using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
using System.Text.RegularExpressions;

namespace TheTyDysh
{
    class Fight
    {
        #region Необходимые общие объекты
        public string winnerName = string.Empty;
        public string loserName = string.Empty;
        public Character character1;
        public Character character2;
        public DataTable dtEventActions;
        public DataTable dtHit = new DataTable();
        public DataTable dtEvasion = new DataTable();
        public DataTable dtBlock = new DataTable();
        public DataTable dtEvent;
        public List<Character> listTurn;
        System.Timers.Timer timerAction = new System.Timers.Timer();
        string channelName = string.Empty;
        Random r = new Random();

        const string styleFight = "<font size=3 style=\"font-family: ‘Lato’; font-weight: bolder;\"> ";

        int defaultChanceBlEv = 80;
        int actionTime = 5;
        #endregion
       
        /// <summary>
        /// СОздание персонажей, подготовка к бою
        /// </summary>
        /// <param name="name"></param>
        /// <param name="secName"></param>
        public Fight(string name, string secName)
        {

            listTurn = new List<Character>();
            character1 = new Character(name);
            character2 = new Character(secName);

            listTurn.Add(character1);
            listTurn.Add(character2);

            timerAction.Elapsed += NextAction;
            timerAction.Interval = 1000 * actionTime;

            dtHit.Columns.Add("Scream");
            dtEvasion.Columns.Add("Scream");
            dtBlock.Columns.Add("Scream");

            LoadDefaultSettings();

            PrepareToFight();
            StartFight();
            timerAction.Start();
        }

        private void LoadDefaultSettings()
        {
        }

        private void NextAction(object sender, ElapsedEventArgs e)
        {
            timerAction.Stop();
            Action();
        }

        WorkWithMYSQL workWithMYSQL = new WorkWithMYSQL();

        /// <summary>
        /// Получение информации из БД
        /// </summary>
        public void PrepareToFight()
        {
            string sql = string.Empty;
            sql = @"Select `Action`.`Name` as `Action`, `Action_Scream`.`Text` as `Scream`  from `Event`
join `Event_Action` on `Event`.`IdEvent` = `Event_Action`.`IdEvent`
join `Action` on `Event_Action`.`IdAction` = `Action`.`IdAction`
join `Action_Scream` on `Action`.`IdAction` = `Action_Scream`.`IdAction`
where `Event`.`Name` = 'Fight';";
            /*dtEventActions = workWithMYSQL.GetTableFromDB(sql);

            var rows = from row in dtEventActions.AsEnumerable()
                       where row.Field<string>("Action") == "Hit"
                       select row.Field<string>("Scream");
                       */

            dtEventActions = workWithMYSQL.GetTableFromDB(sql);
            foreach (DataRow dr in dtEventActions.Rows)
                if (dr["Action"].ToString() == "Hit")
                    dtHit.Rows.Add(dr["Scream"].ToString());
                else if (dr["Action"].ToString() == "Block")
                    dtBlock.Rows.Add(dr["Scream"].ToString());
                else if (dr["Action"].ToString() == "Evasion")
                    dtEvasion.Rows.Add(dr["Scream"].ToString());
        }

       
        /// <summary>
        /// Начало боя
        /// </summary>
        public void StartFight()
        {
            dtEvent = new DataTable();
            string sql = @"Select `Event_Scream`.`Text` from `Event`
join `Event_Scream` on `Event_Scream`.`IdEvent` = `Event`.`IdEvent`
where `Event`.`Name` = 'Fight' and `Event_Scream`.`AtStart` = 1
order by rand() Limit 1";
            dtEvent = workWithMYSQL.GetTableFromDB(sql);

            WriteMessage(dtEvent.Rows[0]["Text"].ToString(),
                new string[,] { { "{name}", listTurn[0].characterName }, { "{secName}", listTurn[1].characterName } }, true);
        }

        #region Действия
        /// <summary>
        /// Выбор действия
        /// </summary>
        public void Action()
        {
            try
            {
                int damage = Convert.ToInt32(listTurn[0].strength + 4 + Convert.ToInt32(listTurn[0].fortune / 3) + r.Next(5) -
                    listTurn[0].strength * (listTurn[0].fatigue / 150.0));

                int d20 = r.Next(20);
                if (d20 > 16)
                {
                    Hit(damage, true);
                }
                else
                {
                    //обычный урон или половина от вероятности
                    int chanceBlock = Convert.ToInt32(listTurn[1].fortune / 3) + listTurn[1].block + listTurn[1].block -
                        Convert.ToInt32(listTurn[1].fatigue / 2);
                    int chanceEvasion = Convert.ToInt32(listTurn[1].fortune / 3) + listTurn[1].evasion + listTurn[1].evasion -
                        Convert.ToInt32(listTurn[1].fatigue / 2);

                    if (chanceBlock > r.Next(defaultChanceBlEv))
                        Block(Convert.ToInt32(damage));
                    else if (chanceEvasion > r.Next(defaultChanceBlEv))
                        Evasion();
                    else
                        Hit(damage);
                }

                //проверка смерти
                if (listTurn[1].health < 0 || listTurn[0].health < 0)
                {
                    winnerName = listTurn[1].health > listTurn[0].health ? listTurn[1].characterName : listTurn[0].characterName;
                    loserName = listTurn[1].health < listTurn[0].health ? listTurn[1].characterName : listTurn[0].characterName;
                    EndFight();
                    return;
                }
                //уменьшение усталости
                for (int i = 0; i < listTurn.Count; i++)
                    listTurn[i].fatigue -= (listTurn[i].fatigue > 5) ? 5 : listTurn[i].fatigue;

                listTurn.Add(listTurn[0]);
                listTurn.RemoveAt(0);


                timerAction.Start();
            }
            catch (Exception ex)
            {
                WriteError.WriteErrorIntoFile("Character - TargetSite" + ex.TargetSite + Environment.NewLine +
                    "Message -" + ex.Message + Environment.NewLine + "Source - " + ex.Source);
                return;
            }
        }

        public void Hit(int damage, bool crit = false)
        {
            listTurn[1].health -= crit ? Convert.ToInt32(damage * 1.5) : damage;
            try
            {
                WriteMessage(dtHit.Rows[r.Next(dtHit.Rows.Count)]["Scream"].ToString(),
                    new string[,] { { "{name}", listTurn[0].characterName },
                { "{secName}", listTurn[1].characterName },
                { "{damage}", (crit ? "!" : "") + damage.ToString() },
                { "{weapon}", listTurn[0].GetEquipmentByType("Weapon").nameWithPrefix} });
            }
            catch
            {
                WriteMessage(dtHit.Rows[r.Next(dtHit.Rows.Count)]["Scream"].ToString(),
                    new string[,] { { "{name}", listTurn[0].characterName },
                { "{secName}", listTurn[1].characterName },
                { "{damage}", (crit ? "!" : "") + damage.ToString() },
                { "{weapon}", "Палка"} });
            }

        }
        public void Evasion()
        {
            listTurn[1].fatigue += 20;
            WriteMessage(dtEvasion.Rows[r.Next(dtEvasion.Rows.Count)]["Scream"].ToString(),
                new string[,] { { "{name}", listTurn[0].characterName },
                { "{secName}", listTurn[1].characterName },});

        }
        public void Block(int damage)
        {
            listTurn[1].fatigue += 20;
            listTurn[1].health -= Convert.ToInt32(damage * 0.85);
            WriteMessage(dtBlock.Rows[r.Next(dtBlock.Rows.Count)]["Scream"].ToString() + " (" + Convert.ToInt16(damage - damage * 0.85).ToString() + " ед. урона прошло).",
                new string[,] { { "{name}", listTurn[0].characterName },
                { "{secName}", listTurn[1].characterName }});
        }
        #endregion

        /// <summary>
        /// Конец боя
        /// </summary>
        public void EndFight()
        {
            dtEvent = new DataTable();
            string sql = @"Select `Event_Scream`.`Text` from `Event`
join `Event_Scream` on `Event_Scream`.`IdEvent` = `Event`.`IdEvent`
where `Event`.`Name` = 'Fight' and `Event_Scream`.`AtStart` = 0
order by rand() Limit 1";
            dtEvent = workWithMYSQL.GetTableFromDB(sql);

            WriteMessage(dtEvent.Rows[0]["Text"].ToString(),
                new string[,] { { "{name}", listTurn[0].characterName }, { "{secName}", listTurn[1].characterName } }, true);

            Thread.Sleep(100);

            Rewards();
        }

        /// <summary>
        /// Выдача наград
        /// </summary>
        public void Rewards()
        {
            string sql = string.Empty;
            //Возможность получить вещь победителю
            if (r.Next(100) > 80)
            {
                string type = string.Empty;
                switch (r.Next(2))
                {
                    case 0: type = "Weapon"; break;
                    case 1: type = "Helmet"; break;
                    case 2: type = "Arnor"; break;
                }

                sql = "Call AddEquipment ('" + winnerName + "','" + type + "',0);";
                workWithMYSQL.ModificationDataInDB(sql);
                string msgGetEq = "За такую победу не грех и шмотку в лицо кинуть, в инвентарь победителя отправляется '" +
                    (type == "Weapon" ? "Оружие" : (type == "Helmet") ? "Шлём" : "Броня") + "'";
                WriteMessage(msgGetEq, new string[,] { }, true);
                Thread.Sleep(100);
            }
            //Возможность потерять вещь проигравшему
            if (r.Next(100) > 80)
            {
                sql = "Call LoseEquipment ('" + loserName + "')";
                workWithMYSQL.ModificationDataInDB(sql);

                WriteMessage("В пылу битвы одна из вещей " + loserName + " была уничтожена", new string[,] { }, true);
                Thread.Sleep(100);
            }

            //Получение и потеря голды
            sql = "Call  EndFightGetLoseCoins('" + winnerName + "', '" + loserName + "')";
            workWithMYSQL.ModificationDataInDB(sql);

            WriteMessage("<br>",
                 new string[,] { { "{name}", listTurn[0].characterName }, { "{secName}", listTurn[1].characterName } });
        }

        /// <summary>
        /// Отправка сообщений в чат
        /// </summary>
        /// <param name="message"></param>
        /// <param name="patter"></param>
        /// <param name="stramChat"></param>
        public void WriteMessage(string message, string[,] patter, bool streamChat = false)
        {
            for (int i = 0; i < patter.GetLength(0); i++)
                message = message.Replace(patter[i, 0], patter[i, 1]);
            frmMain.frmMainWindow.Send($"#fightMessage|??chat:fight" + (streamChat? ":system" : "" ) + $"|??users:{character1.characterName}:{character2.characterName}|??msg:{message}");
            //Сервер.Server.SendMessageAnyUsers(new string[] { character1.characterName, character2.characterName }, message, "fight");
            if (streamChat) ;
            //    Сервер.Server.SendGlobalMessage(styleFight + message + "</font>");
        }

        private void StartNewThread(Thread th)
        {
            th.IsBackground = true;
            th.Start();
        }
    }
}