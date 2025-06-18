using SensorProject.Interfaces;
using SensorProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorProject.Factories
{
    public class IranianAgentFactory : IIranianFactory
    {
        private List<string> weaknesses = new() { "Thermal", "Audio", "Pulse", "Magnetic", "Signal", "Light" };

        public BaseIranianAgent CreateAgent(int level)
        {
            if (level == 1) return new BaseIranianAgent(weaknesses);
            if (level == 2) return new SquadLeader(weaknesses);
            if (level == 3) return new SeniorCommander(weaknesses);
            if (level == 4) return new OrganisationLeader(weaknesses);

            else return new BaseIranianAgent(weaknesses);
        }
    }
}
