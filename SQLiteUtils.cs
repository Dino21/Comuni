using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using DatabaseProvider;

namespace MySQLiteUtils
{
    public class SqliteDb : IDisposable
    {
        private readonly string _connectionString;
        private SQLiteConnection _connection;

        public SqliteDb(string dbPath)
        {
            _connectionString = $"Data Source={dbPath};Version=3;";
            _connection = new SQLiteConnection(_connectionString);
            _connection.Open();
        }

        public int ExecuteNonQuery(string sql, params SQLiteParameter[] parameters)
        {

            using (var cmd = new SQLiteCommand(sql, _connection))
            {
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                return cmd.ExecuteNonQuery();
            }

        }

        public object ExecuteScalar(string sql, params SQLiteParameter[] parameters)
        {
            using (var cmd = new SQLiteCommand(sql, _connection))
            {
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                return cmd.ExecuteScalar();
            }
        }

        public DataTable ExecuteQuery(string sql, params SQLiteParameter[] parameters)
        {
            using (var cmd = new SQLiteCommand(sql, _connection))
            {
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                using (var adapter = new SQLiteDataAdapter(cmd))
                {
                    var dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }
        public int UpdateTable(string tableName, string whereClause, params object[] fields)
        {
            int result = -1;
            if (fields.Length % 2 != 0)
                return result;
            try
            {
                string mySqlString = "UPDATE " + tableName + " SET ";
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
                result = ExecuteNonQuery(mySqlString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
            return result;
        }
        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Close();
                _connection.Dispose();
                _connection = null;
            }
        }
    }

    /*public class SqliteProvider : IDatabaseProvider
    {
        private readonly string _dbPath;

        public SqliteProvider(string dbPath)
        {
            _dbPath = dbPath;
        }

        public bool IsAvailable()
        {
            return File.Exists(_dbPath);
        }

        public DataTable ExecuteQuery(string sql, Dictionary<string, object> parameters = null)
        {
            using (var conn = new SQLiteConnection($"Data Source={_dbPath};"))
            using (var cmd = new SQLiteCommand(sql, conn))
            {
                if (parameters != null)
                    foreach (var p in parameters)
                        cmd.Parameters.AddWithValue(p.Key, p.Value);

                conn.Open();
                var dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
        }

        public int ExecuteNonQuery(string sql, Dictionary<string, object> parameters = null)
        {
            using (var conn = new SQLiteConnection($"Data Source={_dbPath};"))
            using (var cmd = new SQLiteCommand(sql, conn))
            {
                if (parameters != null)
                    foreach (var p in parameters)
                        cmd.Parameters.AddWithValue(p.Key, p.Value);

                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }
    }*/

}
