using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorProject.Models
{
    class BaseIranianAgent
    {
        protected string _TerroristType;
        public List<string> _Weaknesses { get; set; }

        public BaseIranianAgent(List<string> weaknesses)
        {
            _Weaknesses = weaknesses;
        }

        public static List<string> CreateWeaknesses(List<string> allSensorTypes)
        {
            Random rand = new Random();
            List<string> newWeakness = new();

            for (int i = 0; i < 2; i++)
            {
               newWeakness.Add(allSensorTypes[rand.Next(allSensorTypes.Count)]);
            }

            return newWeakness;
        }

        public int ParseSensors(List<BaseSensor> sensors)
        {
            int confirmedSensors = 0;

            foreach (var sensor in sensors)
            {
                if (_Weaknesses.Contains(sensor.SensorName)) confirmedSensors++;
            }
            return confirmedSensors;
        }
    }
}
