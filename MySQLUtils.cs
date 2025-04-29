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
using System.Net.Configuration;
using Newtonsoft.Json.Linq;

namespace MySQLUtils
{
    public class SQLUtils
    {
        // aggiungere al progetto il pacchetto nuget 'MySqlConnector' 2.4.0 o superiore
        string connectionString;
        MySqlConnection myDBConnection;
        MySqlCommand myDBCommand;
        MySqlDataReader myDBReader;
        string mySqlString;
        public SQLUtils(string server, string database, string userId = "root", string password = "C3AU3NXX", string sslMode = "none", string tlsVersion = "Tls12", int port = 3306)
        {
            connectionString = "server=" + server + ";port=" + port + ";user=" + userId + ";password=" + password + ";SslMode=" + sslMode + ";"; //
            if (database!="")
                connectionString = "server=" + server + ";port=" + port + ";database=" + database + ";user=" + userId + ";password=" + password + ";SslMode=" + sslMode + ";"; //
            if (sslMode != "none")
                connectionString += "TlsVersion=" + tlsVersion + ";"; //
            
            myDBConnection = new MySqlConnection(connectionString);
        }
        

        public string[] GetDatabasesList()
        {
            List<string> result = new List<string>();
            try
            {
                Open();
                string query = "SHOW DATABASES";
                using (MySqlCommand cmd = new MySqlCommand(query, myDBConnection))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader.GetString(0).ToString() != "information_schema" &&
                            reader.GetString(0).ToString() != "mydb" &&
                            reader.GetString(0).ToString() != "mysql" &&
                            reader.GetString(0).ToString() != "information_schema" &&
                            reader.GetString(0).ToString() != "performance_schema" &&
                            reader.GetString(0).ToString() != "sys")
                            result.Add(reader.GetString(0).ToString());
                    }
                }
                Close();
            }
            catch
            {

            }
            return result.ToArray();
        }

        public string[] GetTablesList()
        {
            List<string> result = new List<string>();
            try
            {
                Open();
                string query = "SHOW TABLES";
                using (MySqlCommand cmd = new MySqlCommand(query, myDBConnection))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        result.Add(reader.GetString(0).ToString());
                }
                Close();
            }
            catch
            {

            }
            return result.ToArray();
        }

        public int GetLastID(string tableName, string idFieldName)
        {
            int result = -1;
            try
            {
                Open();
                result = Convert.ToInt16(GetStringValue("SELECT* FROM " + tableName + " ORDER BY " + idFieldName +" DESC LIMIT 1", idFieldName));
                
            }
            catch (Exception)
            {

            }
            return result;
        }

        public int AddRow(string tableName, string fieldNames, string fieldValues)
        {
            int result = -1;
            try
            {
                Open();
                mySqlString = "INSERT INTO " + tableName + " (" + fieldNames + ") VALUES (" + fieldValues + ")";
                SetDBCommand(mySqlString);
                result = myDBCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            return result;
        }
        public int AddRow(string tableName, string fieldNames, params object[] fieldValues)
        {
            int result = -1;
            string values;
            try
            {
                Open();
                values = "";
                foreach (var item in fieldValues)
                {
                    Type type = item.GetType();
                    if (item is string)
                        values += "'" + item + "', ";
                    else
                        values += item + ", ";
                }
                if (fieldNames.Length > 1)
                    values = values.Substring(0, values.Length - 2);
                mySqlString = "INSERT INTO " + tableName + " (" + fieldNames + ") VALUES (" + values + ")";
                SetDBCommand(mySqlString);
                result = myDBCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            return result;
        }
        public int UpdateTable(string tableName, string whereClause, params object[] fields)
        {
            int result = -1;
            if (fields.Length % 2 != 0)
                return result;
            try
            {
                Open();
                mySqlString = "UPDATE " + tableName + " SET ";
                for (int i = 0; i < fields.Length; i += 2)
                {
                    if (fields[i + 1] is string)
                        mySqlString += fields[i] + " = '" + fields[i + 1] + "'";
                    else
                        mySqlString += fields[i] + " = " + fields[i + 1];

                    if (i < fields.Length - 2)
                        mySqlString += ", "; 
                }
                mySqlString += " WHERE " + whereClause;
                SetDBCommand(mySqlString);
                result = myDBCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
            return result;
        }

        public int ExecuteSqlCommand(string sqlString)
        {
            int result = -1;
            try
            {
                Open();
                SetDBCommand(sqlString);
                result = myDBCommand.ExecuteNonQuery();
            }
            catch { }
            return result;
        }

        public int ExecuteCounting (string table, string conditions)
        {
            Open();
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

        public int GetIntValue(string sqlStringCommand, string fieldName)
        {
            int value = -1;
            if (Open() != true)
                return value;
            value = Convert.ToInt16(GetStringValue(sqlStringCommand, fieldName));
            
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
