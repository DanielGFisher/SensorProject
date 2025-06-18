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
        public string SensorName { get; }
        public bool IsActive { get; set; }
        public bool HasMatched { get; set; }
        public int Uses { get; set; }

        public BaseSensor(string name)
        {
            SensorName = name;
            IsActive = true;
            HasMatched = false;
            Uses = 0;
        }

        public virtual bool Activate(BaseIranianAgent agent)
        {
            if (!IsActive || HasMatched) return false;
            if (agent.Weaknesses.Contains(SensorName))
            {
                HasMatched = true;
                return true;
            }
            return false;
        }
    }
}
