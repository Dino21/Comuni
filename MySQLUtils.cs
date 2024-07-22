using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using MySqlConnector;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace MySQLUtils
{
    public class SQLUtils
    {
        
        string connectionString;
        MySqlConnection myDBConnection;
        MySqlCommand myDBCommand;
        MySqlDataReader myDBReader;
        string mySqlString;
        public SQLUtils(string server, string database, string userId = "root", string password = "C3AU3NXX", string sslMode = "none", int port = 3306)
        {
            connectionString = "server=" + server + ";port=" + port + ";database=" + database +";user=" + userId + ";password=" + password + ";SslMode=" + sslMode + ";"; //
            myDBConnection = new MySqlConnection(connectionString);
        }

        public int ExecuteSqlCommand(string sqlString)
        {
            int result = -1;
            try
            {
                SetDBCommand(sqlString);
                result = myDBCommand.ExecuteNonQuery();
            }
            catch { }
            return result;
        }

        public int ExecuteCounting (string table, string conditions)
        {
            int result = -1;
            SetDBCommand("SELECT COUNT(*) FROM " + table + " WHERE " + conditions);
            result = Convert.ToInt32(myDBCommand.ExecuteScalar());
            return result;
        }
        void SetDBCommand (string sqlString)
        {
            myDBCommand = new MySqlCommand(sqlString, myDBConnection);
        }
        public DataTable GetDataTable (string sqlStringCommand)
        {
            DataTable dataTable = new DataTable();
            if (Open() != true)
                return null;
            SetDBCommand(sqlStringCommand);
            using (myDBReader = myDBCommand.ExecuteReader())
                dataTable.Load(myDBReader);
            return dataTable;
        }

        public List<string> GetSingleRowList(string sqlStringCommand)
        {
            List<string> list = new List<string>();
            if (Open() != true)
                return null;
            SetDBCommand(sqlStringCommand);
            using (myDBReader = myDBCommand.ExecuteReader(CommandBehavior.SequentialAccess))
            {
                while (myDBReader.Read())
                    for (int i = 0; i < myDBReader.FieldCount; i++)
                    {
                        list.Add(myDBReader[i].ToString());
                    }
                    
            }
            return list;
        }
        public string GetStringValue(string sqlStringCommand, string fieldName)
        {
            string value = "";
            if (Open() != true)
                return null;
            SetDBCommand(sqlStringCommand);
            using (myDBReader = myDBCommand.ExecuteReader(CommandBehavior.SequentialAccess))
            {
                while (myDBReader.Read())
                    value = myDBReader[fieldName].ToString();
            }
                
            return value;
        }

        public bool Open()
        {
            try
            {
                if (myDBConnection.State == ConnectionState.Closed)
                    myDBConnection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }
        public bool Close()
        {
            try
            {
                if (myDBConnection.State == ConnectionState.Open)
                    myDBConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }
    }
}
