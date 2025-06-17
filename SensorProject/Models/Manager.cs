using System;
using System.Collections.Generic;

namespace SensorProject.Models
{
    public class Manager
    {
        private List<BaseSensor> sensors = new List<BaseSensor>();
        private BaseIranianAgent agent;
        private Dictionary<string, int> matchedWeaknesses = new Dictionary<string, int>();
        private List<string> weaknesses = new() { "Thermal", "Audio", "Pulse" };

        public void StartGame()
        {
            List<BaseIranianAgent> agents = new List<BaseIranianAgent>
            {
                new BaseIranianAgent(weaknesses),
                new SquadLeader(weaknesses)
            };

            Random rand = new Random();
            agent = agents[rand.Next(agents.Count)];

            Console.WriteLine($"You are facing: {agent.RevealAgentType()}");
            Console.WriteLine($"Agent weaknesses: {agent.RevealWeaknesses()}");

            int matchedCount = 0;
            bool gameRunning = true;

            while (gameRunning)
            {
                Console.WriteLine("\nAvailable sensors: Audio, Thermal, Pulse");
                Console.Write("Enter sensor type (or type 'exit' to quit): ");
                string input = Console.ReadLine().ToUpper();

                if (input == "EXIT")
                {
                    gameRunning = false;
                    break;
                }

                BaseSensor newSensor = CreateSensor(input);
                if (newSensor != null)
                {
                    sensors.Add(newSensor);
                    Console.WriteLine($"{newSensor.SensorName} sensor added");
                }
                else
                {
                    Console.WriteLine("Invalid sensor type - Try again");
                    continue;
                }

                foreach (BaseSensor sensor in sensors)
                {
                    if (!sensor.IsActive || sensor.HasMatched) continue;

                    string type = sensor.SensorName;
                    int timesMatched = matchedWeaknesses.ContainsKey(type) ? matchedWeaknesses[type] : 0;
                    int totalNeeded = agent.Weaknesses.FindAll(w => w == type).Count;

                    if (timesMatched < totalNeeded && sensor.Activate(agent))
                    {
                        matchedWeaknesses[type] = timesMatched + 1;
                        matchedCount++;
                        Console.WriteLine($"{type} matched a weakness!");
                    }
                }

                RemoveInactiveSensors();

                if (agent is SquadLeader squadLeader)
                {
                    int index = squadLeader.RemoveSensor(sensors);
                    if (index != -1 && index < sensors.Count)
                    {
                        matchedWeaknesses[sensors[index].SensorName]--;
                    }
                }

                Console.WriteLine($"{matchedCount}/{agent.SensorAmount} weaknesses matched");
                if (matchedCount >= agent.SensorAmount)
                {
                    Console.WriteLine("Agent exposed! Mission complete!");
                    gameRunning = false;
                }
            }
        }

        private BaseSensor CreateSensor(string type)
        {
            type = type.ToUpper();

            if (type == "AUDIO") return new AudioSensor("Audio");
            if (type == "THERMAL") return new ThermalSensor("Thermal");
            if (type == "PULSE") return new PulseSensor("Pulse");

            else return null;
        }


        private void RemoveInactiveSensors()
        {
            for (int i = sensors.Count - 1; i >= 0; i--)
            {
                if (!sensors[i].IsActive)
                {
                    if (matchedWeaknesses.ContainsKey(sensors[i].SensorName))
                    {
                        matchedWeaknesses[sensors[i].SensorName]--;
                    }

                    Console.WriteLine($"{sensors[i].SensorName} sensor removed");
                    sensors.RemoveAt(i);
                }
            }
        }
    }
}