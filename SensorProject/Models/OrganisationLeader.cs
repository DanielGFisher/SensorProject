using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorProject.Models
{
    internal class OrganisationLeader : BaseIranianAgent
    {
        private int turnCount = 0;

        public OrganisationLeader(List<string> allSensorTypes) : base(allSensorTypes)
        {
            AgentType = "Squad-Leader";
            SensorAmount = 8;
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

