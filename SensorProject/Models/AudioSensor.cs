using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorProject.Models
{
    internal class AudioSensor : BaseSensor
    {
        public new string SensorName { get; set; }

        AudioSensor(string name) : base(name)
        {
            SensorName = name;
        }
    }
}
