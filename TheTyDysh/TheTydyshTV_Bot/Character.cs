using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data;
using System.Text.RegularExpressions;
using System.Text;

namespace TheTyDysh
{
    /// <summary>
    /// Персонаж
    /// </summary>
    public class Character
    {
        public int health = 100;
        public int strength = 5;
        public int evasion = 5;
        public int block = 5;
        public int fortune = 5;
        public int coins = 0;
        public int fatigue = 0; //усталость 
        public string characterName = string.Empty;
        private string characterId = "-1";
        public List<Equipment> inventoryList = new List<Equipment>();
        public List<Equipment> equipmentList = new List<Equipment>();
        public List<string[]> bufsList = new List<string[]>(); //список бафов
        public List<string[]> debufsList = new List<string[]>(); //список дебафов
        public DataTable dtEquipmentAndStats = new DataTable();

        public Character(string _name)
        {
            characterName = _name;
            GetCharacter();
        }

        public void GetCharacter()
        {
            string sqlGetEqquipment =
 @"Select `Character`.`IdCharacter`, `Character`.`Name`, `Character`.`Description`,
`Equipment`.`Health`,
`Equipment`.`Strength`,
`Equipment`.`Evasion`,
`Equipment`.`Block`,
`Equipment`.`Fortune`,
`Equipment`.`Coins`, 
`Equipment`.`NameEquipment`,
`Equipment`.`Sex`,
`Equipment`.`Type`,
`Equipment`.`Description` as `EquipmentDescription`,
`Equipment`.`IdEquipment`,
`Equipment_Prefix`.`Rare`,
`Equipment_Prefix`.`Prefix`,
`Equipment_Prefix`.`InHonor`,
`Character_Equipment`.`Taken`,
`Character_Equipment`.`IdEquipment`,
concat(
if (`Equipment_Prefix`.`Rare` != 'Обычный', concat(`Equipment_Prefix`.`Prefix`, ' '), '')
,  `Equipment`.`NameEquipment`) as 'NameEqPref'
                                                     
from `Character` left join `Character_Equipment` on `Character`.IdCharacter = `Character_Equipment`.IdCharacter
left join `Equipment` on `Character_Equipment`.IdEquipment = `Equipment`.IdEquipment
left join `Equipment_Prefix` on `Equipment_Prefix`.`IdEquipmentPrefix` = `Character_Equipment`.IdEquipmentPrefix
where `Character`.`Name` = '" + characterName + @"'
union
select `Character`.`IdCharacter`, `Character`.`Name`, `Character`.`Description`,
(`Character`.`Health`) as 'Health',
(`Character`.`Strength`) as 'Strength',
(`Character`.`Evasion`) as 'Evasion',
(`Character`.`Block`) as 'Block',
(`Character`.`Fortune`) as 'Fortune',
`Character`.`Coins`, 
null,
null,
null,
null,
null,
null,
null,
null,
null,
null,
null
from `Character`
where `Character`.`Name` = '" + characterName + "';";
            WorkWithMYSQL sqlClient = new WorkWithMYSQL();
            dtEquipmentAndStats = sqlClient.GetTableFromDB(sqlGetEqquipment);
            try
            {
                GetCharacterId();
                //Запись вещей
                GetEquipment();
            }
            catch (Exception ex)
            {
                WriteError.WriteErrorIntoFile("Character - TargetSite" + ex.TargetSite + Environment.NewLine +
                    "Message - " + ex.Message + Environment.NewLine + "Source - " + ex.Source);
                return;
            }
        }

        private void GetCharacterId()
        {
            if (dtEquipmentAndStats.Rows.Count != 0)
                characterId = dtEquipmentAndStats.Rows[0]["IdCharacter"].ToString();
        }

        /// <summary>
        /// Сортировка вещей
        /// </summary>
        public void GetEquipment()
        {
            List<string[]> patternsSex = new List<string[]>(){
                    new string[]{ "ий$", "ая" },
                    new string[] { "ый$", "ая" },
                    new string[] { "ой$", "ая" },
                    new string[] { "ийся$", "аяся" },};
            string eqPrefix = "";

            foreach (DataRow dr in dtEquipmentAndStats.Rows)
                if (dr["NameEquipment"].ToString() != string.Empty)
                {
                    eqPrefix = dr["Prefix"].ToString();
                    if (dr["InHonor"].ToString() == "1")
                    {
                        eqPrefix += "'s";
                    }
                    else if (dr["Sex"].ToString() == "f")
                    {
                        foreach (string[] pattern in patternsSex)
                        {
                            if (Regex.IsMatch(eqPrefix, pattern[0]))
                            {
                                eqPrefix = Regex.Replace(eqPrefix, pattern[0], pattern[1]);
                                break;
                            }
                        }
                    }
                    eqPrefix += (eqPrefix == "" ? "" : " ") + dr["NameEquipment"].ToString();
                    if (dr["Taken"].ToString() == "1")
                    {
                        equipmentList.Add(new Equipment(Convert.ToInt32(dr["IdEquipment"]), dr["Type"].ToString(), dr["NameEquipment"].ToString(), 
                            dr["EquipmentDescription"].ToString(), dr["Rare"].ToString(), eqPrefix, dr["Sex"].ToString(),
                            Convert.ToInt32(dr["Health"]), Convert.ToInt32(dr["Strength"]), Convert.ToInt32(dr["Evasion"]), 
                            Convert.ToInt32(dr["Block"]), Convert.ToInt32(dr["Fortune"]), Convert.ToInt32(dr["Coins"])));
                    }
                    else
                    {
                        inventoryList.Add(new Equipment(Convert.ToInt32(dr["IdEquipment"]), dr["Type"].ToString(), dr["NameEquipment"].ToString(),
                            dr["EquipmentDescription"].ToString(), dr["Rare"].ToString(), eqPrefix, dr["Sex"].ToString(),
                            Convert.ToInt32(dr["Health"]), Convert.ToInt32(dr["Strength"]), Convert.ToInt32(dr["Evasion"]),
                            Convert.ToInt32(dr["Block"]), Convert.ToInt32(dr["Fortune"]), Convert.ToInt32(dr["Coins"])));

                    }
                }
            GetStats();
        }

        /// <summary>
        /// Перерасчет характеристик
        /// </summary>
        public void GetStats()
        {
            if (equipmentList.Count != 0)
            {
                health = 0;
                strength = 0;
                evasion = 0;
                block = 0;
                fortune = 0;
                foreach (Equipment eq in equipmentList)
                {
                    health += eq.health;
                    strength += eq.strength;
                    evasion += eq.evasion;
                    block += eq.block;
                    fortune += eq.fortune;
                    coins = eq.coins;
                }
            }

            foreach (DataRow dr in dtEquipmentAndStats.Rows)
                if (dr["NameEquipment"].ToString() == "")
                {
                    health += Convert.ToInt32(dr["Health"]);
                    strength += Convert.ToInt32(dr["Strength"]);
                    evasion += Convert.ToInt32(dr["Evasion"]);
                    block += Convert.ToInt32(dr["Block"]);
                    fortune += Convert.ToInt32(dr["Fortune"]);
                }
        }

        /// <summary>
        /// Нахождение вещи по типу
        /// </summary>
        /// <param name="type">Тип вещи</param>
        /// <returns></returns>
        public Equipment GetEquipmentByType(string type)
        {
            foreach (Equipment eq in equipmentList)
                if (eq.typeEq == type)
                return eq;
            return new Equipment();
        }
        /// <summary>
        /// Обновление информации в БД
        /// </summary>
        /// <returns>Количество затронутых строк</returns>
        public int UpdateInfo()
        {
            if (characterId == "-1")
                return -1;
            int counter = 0;
            string sql = $@"Update `Character_Equipment` set `Taken` = 0 where `IdCharacter` = {characterId} ;";
            WorkWithMYSQL sqlClient = new WorkWithMYSQL();
            counter += sqlClient.ModificationDataInDB(sql);
            sql = "";
            //Обновление взятых вещей
            foreach (Equipment eq in equipmentList)
            {
                sql += $@" Update `Character_Equipment` set `Taken` = 1 where `IdCharacter` = {characterId} and `IdEquipment` = {eq.idEq} ;";
            }

            counter += sqlClient.ModificationDataInDB(sql);

            return counter;
        }
    }

    public class Equipment
    {
        public int idEq = -1;
        public string typeEq = "Weapon";
        public string name = "Палка";
        public string description = "";
        public string rare = "";
        public string nameWithPrefix = "";
        public string sex = "";
        public int health = 0;
        public int strength = 0;
        public int evasion = 0;
        public int block = 0;
        public int fortune = 0;
        public int coins = 0;

        public Equipment()
        {
            ;
        }

        public Equipment(int _idEq, string _type, string _name, string _description, string _rare, string _nameWithPrefix, string _sex,
            int _health = 0, int _strength = 0, int _evasion = 0, int _block = 0, int _fortune = 0, int _coins = 0)
        {
            idEq = _idEq;
            typeEq = _type;
            name = _name;
            description = _description;
            rare = _rare;
            nameWithPrefix = _nameWithPrefix;
            sex = _sex;
            health = _health;
            strength = _strength;
            evasion = _evasion;
            block = _block;
            fortune = _fortune;
            coins = _coins;

        }
    }
}