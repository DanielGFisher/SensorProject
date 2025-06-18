using System;
using System.Collections.Generic;

namespace SensorProject.Models
{
    public class SquadLeader : BaseIranianAgent
    {
        public int turnCount = 0;

        public SquadLeader(List<string> types) : base(types)
        {
            AgentType = "Squad-Leader";
            SensorAmount = 4;
            Weaknesses = GenerateWeaknesses(types);
        }

        public int ReturnTurnCount()
        {
            return turnCount;
        }
        public  int RemoveSensor(List<BaseSensor> sensors)
        {
            turnCount++;
            if (turnCount % 3 == 0 && sensors.Count > 0)
            {
                Random rand = new();
                int index = rand.Next(sensors.Count);
                Console.WriteLine($"Sensor Sabotaged: {sensors[index].SensorName}");
                sensors.RemoveAt(index);
                return index;
            }
            return -1;
        }
    }
}