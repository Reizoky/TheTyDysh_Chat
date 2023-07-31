using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Data;
using System.Timers;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using TwitchLib.Client;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Events;
using TwitchLib.Client.Extensions;
using TwitchLib.Client.Models;
using TwitchLib.Api.V5.Models.Users;

namespace TheTyDysh
{
    class Events
    {
        Random random = new Random();
        public bool isFight = false; //Флаг, если идет бой
        
        /// <summary>
        /// Начало события Бой
        /// </summary>
        /// <param name="name1">Имя 1 бойца</param>
        /// <param name="name2">Имя 2 бойца</param>
        public void StartEventFight(string name1, string name2)
        {
            isFight = true;
            StartNewThread(new Thread(delegate ()
            {
                Fight fight1 = new Fight(name1, name2);
                while (fight1.loserName == string.Empty)
                    Thread.Sleep(2000);
                EndFight(fight1.winnerName, fight1.loserName);
            }));
        }

        /// <summary>
        /// Окончание бой, запуск тамеров
        /// </summary>
        private void EndFight()
        {
            isFight = false;
        }

        /// <summary>
        /// Окончание боя, запуск таймеров, занесение победителя в список турнира
        /// </summary>
        /// <param name="winner">Победитель</param>
        /// <param name="looser">Проигравший</param>
        private void EndFight(string winner, string looser)
        {
            EndFight();
        }

        /// <summary>
        /// Запуск нового фонового потока
        /// </summary>
        /// <param name="th">Поток</param>
        private void StartNewThread(Thread th)
        {
            th.IsBackground = true;
            th.Start();
        }

    }
}