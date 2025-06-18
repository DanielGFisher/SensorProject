using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorProject.Models
{
    internal class SignalSensor : BaseSensor
    {
        public SignalSensor(string name) : base(name) { }

        public string RevealField(BaseIranianAgent agent)
        {
            if (!agent.Weaknesses.Contains(SensorName))
                return "Reveal failed: Thermal scan ineffective";

            string intel = agent.RevealAgentType();
            return $"The operative type is - {intel}";
        }
    }
}
