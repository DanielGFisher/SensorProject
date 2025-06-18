using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorProject.Models
{
    public class SeniorCommander : SquadLeader
    {
        public SeniorCommander(List<string> types) : base(types)
        {
            AgentType = "Senior-Commander";
            SensorAmount = 6;
            Weaknesses = GenerateWeaknesses(types);
        }
    }
}

