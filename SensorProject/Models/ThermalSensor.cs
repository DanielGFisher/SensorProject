using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorProject.Models
{
    public class ThermalSensor : BaseSensor
    {

        public ThermalSensor(string name) : base(name)
        {
        }

        public string RevealSensor(BaseIranianAgent agent)
        {
            Random rand = new Random();
            if (this.Activate(agent))
            {
                string revealWeakness = agent._Weaknesses[rand.Next(agent._Weaknesses.Count)];
                return $"One of the sensors is: {revealWeakness}";
            }
            else
            {
                return "Invalid operation - activation unsuccessful";
            }
        }
    }
}
