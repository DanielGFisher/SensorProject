using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SensorProject.Models
{
    public class BaseSensor
    {
        public string SensorName { get; set; }
        
        public BaseSensor(string name)
        {
            SensorName = name;
        }

        public bool Activate(BaseIranianAgent agent)
        {
            return agent._Weaknesses.Contains(SensorName);
        }
    }
}
