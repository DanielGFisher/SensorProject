using MySql.Data.MySqlClient;
using SensorProject.Models;
using System;

namespace SensorProject.DAL
{
    public class PlayerDal
    {
        private readonly ConnectionDal _connectionDal;

        public PlayerDal(string connectionString)
        {
            _connectionDal = new ConnectionDal(connectionString);
        }

        public void InsertPlayer(Player player)
        {
            using var conn = _connectionDal.CreateConnection();
            string query = "INSERT INTO players (username, level) VALUES (@username, @level)";

            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@username", player.Name);
            cmd.Parameters.AddWithValue("@level", player.Level);

            cmd.ExecuteNonQuery();
        }

        public Player GetPlayerByUsername(string username)
        {
            using var conn = _connectionDal.CreateConnection();
            string query = "SELECT username, level FROM players WHERE username = @username LIMIT 1";

            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@username", username);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                string name = reader.GetString("username");
                int level = reader.GetInt32("level");

                return new Player(name, level);
            }

            return null;
        }

        public void UpdatePlayerLevel(string username, int newLevel)
        {
            using var conn = _connectionDal.CreateConnection();
            string query = "UPDATE players SET level = @level WHERE username = @username";

            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@level", newLevel);
            cmd.Parameters.AddWithValue("@username", username);

            cmd.ExecuteNonQuery();
        }
    }
}
