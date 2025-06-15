using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorProject.Models
{
    public class Manager
    {
        private List<BaseSensor> sensors = new List<BaseSensor> { };
        private BaseIranianAgent agent;
        private List<string> sensorTypes = new List<string> { "Thermal", "Audio" };

        public void StartGame()
        {
            agent = new BaseIranianAgent(sensorTypes);

            bool running = true;
            while (running)
            {
                Console.WriteLine("\nAvailable sensors: Audio, Thermal");
                Console.Write("Enter sensor to add (or type exit to exit): ");
                string input = Console.ReadLine();

                switch (input.ToUpper())
                {
                    case "EXIT":
                        running = false;
                        break;

                    case "THERMAL":
                        ThermalSensor thermal = new ThermalSensor("Thermal");
                        sensors.Add(thermal);
                        Console.WriteLine(thermal.RevealSensor(agent));
                        agent._Weaknesses.Remove("Thermal");
                        break;

                    case "AUDIO":
                        AudioSensor audio = new AudioSensor("Audio");
                        sensors.Add(audio);
                        Console.WriteLine($"Audio sensor activated");
                        agent._Weaknesses.Remove("Audio");
                        break;

                    default:
                        Console.WriteLine("Invalid sensor type");
                        continue;
                }

                int matchCount = agent.ParseSensors(sensors);
                Console.WriteLine($"{matchCount}/2 sensors matched the agent's weaknesses");

                if (matchCount == 2)
                {
                    Console.WriteLine("Agent exposed! Mission complete.");
                    break;
                }
            }
        }
    }
}
