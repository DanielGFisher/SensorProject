using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorProject.Models
{
    internal class SquadLeader : BaseIranianAgent
    {

        public SquadLeader(List<string> allSensorTypes) : base(allSensorTypes)
        {
            AgentType = "Squad-Leader";
            SensorAmount = 4;
        }

        public void RemoveSensor(List<BaseSensor> sensors)
        {
            Random rand = new();
            sensors.Remove(sensors[rand.Next(0, sensors.Count())]);
        }
    }
}
