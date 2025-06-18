using MySql.Data.MySqlClient;
using Mysqlx.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorProject.DAL
{

    public class MatchedWeaknessDal
    {
        private readonly string _connectionString;

        public MatchedWeaknessDal(string connectionString)
        {
            _connectionString = connectionString;
        }

        private MySqlConnection CreateConnection()
        {
            MySqlConnection conn = new MySqlConnection(_connectionString);
            conn.Open();
            return conn;
        }
    }
}