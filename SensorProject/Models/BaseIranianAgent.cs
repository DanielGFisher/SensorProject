using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorProject.Models
{
    public class BaseIranianAgent
    {
        protected string AgentType;
        protected int SensorAmount { get; set; }
        public List<string> Weaknesses { get; set; }

        public BaseIranianAgent(List<string> allSensorTypes)
        {
            AgentType = "Foot-Soldier";
            SensorAmount = 2;
            Weaknesses = GenerateWeaknesses(allSensorTypes);
        }

        protected  List<string> GenerateWeaknesses(List<string> allSensorTypes)
        {
            var rand = new Random();
            var weaknesses = new List<string>();
            for (int i = 0; i < SensorAmount; i++)
            {
                var sensor = allSensorTypes[rand.Next(allSensorTypes.Count)];
                weaknesses.Add(sensor);
            }
            return weaknesses;
        }

        public string RevealWeaknesses()
        {
            return string.Join(", ", Weaknesses);
        }

        public string RevealAgentType()
        {
            return $"{AgentType}";
        }
        public int RevealSensorAmount()
        {
            return SensorAmount;
        }
    }
}
