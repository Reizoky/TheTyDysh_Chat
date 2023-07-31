using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using MySql.Data.MySqlClient;

namespace TheTyDysh
{
    class WorkWithMYSQL
    {
        public string strConnect = string.Empty;
        public MySqlDataAdapter daTableForBinding;
        public DataTable dtForBinfing;
        MySqlConnection conn;
        public string StringConnect()
        {
            string serverName = "185.154.54.8"; // Адрес сервера (для локальной базы пишите "localhost")
            string port = "3306"; // Порт для подключения
            string userName = "vh242135_admin"; // Имя пользователя
            string password = "C1g2S9h"; // Пароль для подключения
            string dbName = "vh242135_tydyshtv"; //Имя базы данных

            strConnect = "server=" + serverName +
                ";user=" + userName +
                ";database=" + dbName +
                ";port=" + port +
                ";password=" + password + ";";

            return strConnect;
        }

        /// <summary>
        /// Получение таблицы с данными
        /// </summary>
        /// <param name="sql">Строка запроса</param>
        /// <returns></returns>
        public DataTable GetTableFromDB(string sql)
        {
            try
            {
                using (conn = new MySqlConnection(StringConnect()))
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand(sql, conn);
                    MySqlDataReader reader = command.ExecuteReader();
                    DataTable dtInfo = new DataTable();
                    dtInfo.Load(reader);
                    return dtInfo;
                }
            }
            catch (Exception ex)
            {
                WriteError.WriteErrorIntoFile("WorkWithMYSQL - TargetSite" + ex.TargetSite + Environment.NewLine +
                    "Message - " + ex.Message + Environment.NewLine + "Source - " + ex.Source);
                return new DataTable("Fail");
            }
        }

        /// <summary>
        /// Модификация данных
        /// </summary>
        /// <param name="sql">Строка запроса</param>
        /// <returns></returns>
        public int ModificationDataInDB(string sql)
        {
            try
            {
                using (conn = new MySqlConnection(StringConnect()))
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand(sql, conn);
                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                WriteError.WriteErrorIntoFile("WorkWithMYSQL - TargetSite" + ex.TargetSite + Environment.NewLine +
                    "Message - " + ex.Message + Environment.NewLine + "Source - " + ex.Source);
                return -1;
            }
        }

        public DataTable BindingTable(string sql)
        {
            
            try
            {
                using (conn = new MySqlConnection(StringConnect()))
                {
                    conn.Open();
                    dtForBinfing = new DataTable();
                    daTableForBinding = new MySqlDataAdapter(sql, strConnect);
                    MySqlCommandBuilder cb = new MySqlCommandBuilder(daTableForBinding);

                    daTableForBinding.Fill(dtForBinfing);
                    daTableForBinding.FillSchema(dtForBinfing, SchemaType.Source);

                    return dtForBinfing;
                }
            }
            catch (Exception ex)
            {
                WriteError.WriteErrorIntoFile("WorkWithMYSQL - TargetSite" + ex.TargetSite + Environment.NewLine +
                    "Message - " + ex.Message + Environment.NewLine + "Source - " + ex.Source);
                return new DataTable("Fail");
            }
        }

        public int UpdateBindingTable()
        {
            return daTableForBinding.Update(dtForBinfing);
        }
    }
}