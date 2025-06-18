using SensorProject.Interfaces;
using SensorProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorProject.Factories
{
    public class SensorFactory : ISensorFactory
    {
        public BaseSensor CreateSensor(string type)
        {
            type = type.ToUpper();

            if (type == "AUDIO") return new AudioSensor("Audio");
            if (type == "THERMAL") return new ThermalSensor("Thermal");
            if (type == "PULSE") return new PulseSensor("Pulse");
            if (type == "MAGNETIC") return new MagneticSensor("Magnetic");
            if (type == "SIGNAL") return new SignalSensor("Signal");
            if (type == "LIGHT") return new LightSensor("Light");

            else return null!;
        }
    }
}
