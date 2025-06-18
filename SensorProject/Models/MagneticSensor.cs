using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorProject.Models
{
    internal class MagneticSensor : BaseSensor
    {
        public int ProtectionSkips { get; set; }
        public MagneticSensor(string name) : base(name) 
        {
            ProtectionSkips = 2;
        }
    }
}
