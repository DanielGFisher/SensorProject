using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorProject.Models
{
    internal class LightSensor : BaseSensor
    {
        public LightSensor(string name) : base(name) { }

        public string RevealTwoFields(BaseIranianAgent agent)
        {
            if (!agent.Weaknesses.Contains(SensorName))
                return "Reveal failed: Thermal scan ineffective";

            string intel = agent.RevealAgentType();
            return $"The operative type is - {intel}" +
                $"The operative sensor amount is - {agent.SensorAmount}";
        }
    }
}
