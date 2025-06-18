using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SensorProject.DAL
{
    public class SensorConfig
    {
        public string SensorName { get; }
        public bool IsActive { get; set; }
        public bool HasMatched { get; set; }
        public int Uses { get; set; }

        public class SensorDal
    {
        private readonly string _connectionString;

        public SensorDal(string connectionString)
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
