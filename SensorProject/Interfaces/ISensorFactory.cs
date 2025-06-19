using SensorProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorProject.Interfaces
{
    public interface ISensorFactory
    {
        BaseSensor CreateSensor(string type);
    }
}
