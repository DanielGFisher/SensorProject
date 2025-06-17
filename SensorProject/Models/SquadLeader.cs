using System;
using System.Collections.Generic;

namespace SensorProject.Models
{
    internal class SquadLeader : BaseIranianAgent
    {
        private int turnCount = 0;

        public SquadLeader(List<string> allSensorTypes) : base(allSensorTypes)
        {
            AgentType = "Squad-Leader";
            SensorAmount = 4;
        }

        public override List<string> GenerateWeaknesses(List<string> allSensorTypes)
        {
            var rand = new Random();
            var weaknesses = new List<string>();
            for (int i = 0; i < 4; i++)
            {
                var sensor = allSensorTypes[rand.Next(allSensorTypes.Count)];
                weaknesses.Add(sensor);
            }
            return weaknesses;
        }

        public int RemoveSensor(List<BaseSensor> sensors)
        {
            turnCount++;

            if (turnCount % 3 == 0 && sensors.Count > 0)
            {
                Random rand = new Random();
                int index = rand.Next(sensors.Count);
                Console.WriteLine($"Sensor Sabotaged: {sensors[index].SensorName}");
                sensors.RemoveAt(index);
                return index;
            }
            return -1;
        }
    }
}