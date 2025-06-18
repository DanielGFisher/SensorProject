using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SensorProject.DAL
{
    public class AgentConfig
    {
        public int Level { get; set; }
        public string AgentType { get; set; } = string.Empty;
        public int SensorAmount { get; set; }
        public List<string> Weaknesses { get; set; } = new();
    }

    public class AgentDal
    {
        private readonly string _connectionString;

        public AgentDal(string connectionString)
        {
            _connectionString = connectionString;
        }

        private MySqlConnection CreateConnection()
        {
            MySqlConnection conn = new(_connectionString);
            conn.Open();
            return conn;
        }
    }
}
