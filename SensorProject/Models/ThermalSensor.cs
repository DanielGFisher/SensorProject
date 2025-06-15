using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorProject.Models
{
    internal class ThermalSensor : BaseSensor
    {
        public new string SensorName { get; set; }

        public ThermalSensor(string name) : base(name)
        {
            SensorName = name;
        }

        public string RevealSensor(List<string> names)
        {

        }
    }
}
