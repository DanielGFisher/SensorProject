using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using SensorProject.Models;

namespace SensorProject.DAL
{
    public class IranianAgent_Dal
    {
        private readonly ConnectionDal _connectionDal;

        public IranianAgent_Dal(string connectionString)
        {
            _connectionDal = new ConnectionDal(connectionString);
        }

        public BaseIranianAgent GetAgentByLevel(int level)
        {
            BaseIranianAgent config = null;

            using var conn = _connectionDal.CreateConnection();
            string query = @"
                SELECT a.level, a.agentType, a.sensorAmount, w.weakness
                FROM agents a
                LEFT JOIN weaknesses w ON a.level = w.agentLevel
                WHERE a.level = @level;
            ";

            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@level", level);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (config == null)
                {
                    config = new BaseIranianAgent
                    {
                        AgentType = reader.GetString("agentType"),
                        SensorAmount = reader.GetInt32("sensorAmount"),
                        Weaknesses = new List<string>()
                    };
                }

                if (!reader.IsDBNull(reader.GetOrdinal("weakness")))
                {
                    config.Weaknesses.Add(reader.GetString("weakness"));
                }
            }

            return config;
        }
    }
}
