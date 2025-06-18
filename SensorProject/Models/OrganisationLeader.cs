using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorProject.Models
{
    public class OrganisationLeader : SquadLeader
    {
        public OrganisationLeader(List<string> types) : base(types)
        {
            AgentType = "Organisation-Leader";
            SensorAmount = 8;
            Weaknesses = GenerateWeaknesses(types);
        }
    }
}


