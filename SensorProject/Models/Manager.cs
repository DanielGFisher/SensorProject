using System;
using System.Collections.Generic;

namespace SensorProject.Models
{
    public class Manager
    {
        private List<BaseSensor> sensors = new();
        private BaseIranianAgent agent;
        private List<string> sensorTypes = new List<string> { "Thermal", "Audio", "Pulse" };
        private Dictionary<string, int> matchedWeaknesses = new();

        public void StartGame()
        {
            agent = new BaseIranianAgent(sensorTypes);
            int activeCount = 0;
            bool running = true;

            Console.WriteLine(agent.RevealWeaknesses());

            while (running)
            {
                Console.WriteLine("Available sensors: Audio, Thermal, Pulse");
                Console.Write("Enter sensor to add (or type 'exit' to exit): ");
                string input = Console.ReadLine();

                switch (input.ToUpper())
                {
                    case "EXIT":
                        running = false;
                        break;

                    case "THERMAL":
                        var thermal = new ThermalSensor("Thermal");
                        sensors.Add(thermal);
                        Console.WriteLine("Thermal sensor added");
                        Console.WriteLine(thermal.RevealSensor(agent));
                        break;

                    case "AUDIO":
                        sensors.Add(new AudioSensor("Audio"));
                        Console.WriteLine("Audio sensor added");
                        break;

                    case "PULSE":
                        sensors.Add(new PulseSensor("Pulse"));
                        Console.WriteLine("Pulse sensor added");
                        break;

                    default:
                        Console.WriteLine("Invalid sensor type");
                        continue;
                }

                for (int i = sensors.Count - 1; i >= 0; i--)
                {
                    BaseSensor sensor = sensors[i];
                    sensor.IncreaseUsage();

                    if (sensor is PulseSensor && sensor.Uses == 3 && sensor.HasMatched)
                    {
                        matchedWeaknesses["Pulse"] -= 1;
                        sensors.RemoveAt(i);
                        activeCount--;
                        Console.WriteLine("Pulse sensor broken - 3 uses");
                        continue;
                    }

                    if (!sensor.IsActive || sensor.HasMatched)
                    {
                        continue;
                    }

                    string type = sensor.SensorName;

                    int timesMatched = 0;
                    if (matchedWeaknesses.ContainsKey(type))
                    {
                        timesMatched = matchedWeaknesses[type];
                    }

                    int totalNeeded = 0;

                    foreach (var weakness in agent.Weaknesses)
                    {
                        if (weakness == type)
                        {
                            totalNeeded++;
                        }
                    }

                    if (timesMatched < totalNeeded && sensor.Activate(agent))
                    {
                        matchedWeaknesses[type] = timesMatched + 1;
                        activeCount++;
                        Console.WriteLine(type + " matched a weakness.");
                    }
                }

                Console.WriteLine($"{activeCount}/2 weaknesses matched");

                if (activeCount == 2)
                {
                    Console.WriteLine("Agent exposed! Mission complete");
                    running = false;
                }
            }
        }
    }
}