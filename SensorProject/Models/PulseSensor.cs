using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorProject.Models
{
    public class PulseSensor : BaseSensor
    {

        public PulseSensor(string name) : base(name) { }

        public override bool Activate(BaseIranianAgent agent)
        {
            if (!IsActive || HasMatched)
            {
                return false;
            }

            if (agent.Weaknesses.Contains(SensorName))
            {
                HasMatched = true;
                return true;
            }

            return false;
        }
    }
}
