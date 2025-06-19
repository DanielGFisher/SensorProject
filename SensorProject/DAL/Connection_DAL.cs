using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SensorProject.DAL
{
    public class ConnectionDal
    {
        private readonly string _connectionString;

        public ConnectionDal(string connectionString)
        {
            _connectionString = connectionString;
        }

        public MySqlConnection CreateConnection()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(_connectionString);
                conn.Open();
                return conn;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Database connection failed: " + ex.Message);
                throw;
            }
        }
    }
}

