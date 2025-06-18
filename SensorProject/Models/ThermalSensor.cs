using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorProject.Models
{
    public class ThermalSensor : BaseSensor
    {
        private static readonly Random rand = new Random();

        public ThermalSensor(string name) : base(name) { }

        public string RevealSensor(BaseIranianAgent agent)
        {
            if (!agent.Weaknesses.Contains(SensorName))
                return "Reveal failed: Thermal scan ineffective";

            string intel = agent.Weaknesses[rand.Next(agent.Weaknesses.Count)];
            return $"Intel gained: One of the weaknesses is '{intel}'.";
        }
    }
}
