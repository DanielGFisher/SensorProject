using SensorProject.Factories;
using System;
using System.Collections.Generic;

namespace SensorProject.Models
{
    public class Manager
    {
        private List<BaseSensor> sensors = new List<BaseSensor>();
        private BaseIranianAgent agent;
        private Dictionary<string, int> matchedWeaknesses = new Dictionary<string, int>();
        private IranianAgentFactory AgentFactory = new();
        private SensorFactory SensorFactory = new();
        private int level = 1;

        public void StartGame()
        {
            Console.WriteLine("- Game Starting -\n");

            bool gameRunning = true;
            while (gameRunning)
            {
                Console.WriteLine($"Level - {level}");
                bool levelComplete = PlayLevel();

                if (levelComplete)
                {
                    if (level == 3)
                    {
                        gameRunning = false;
                    }
                    Console.WriteLine($"Level {level} Complete! Advancing to the next level\n");
                    level++;
                }
                else
                {
                    Console.WriteLine("Game Over! Try again");
                    break;
                }
            }
        }
        
        public bool PlayLevel()
        {
            matchedWeaknesses.Clear();
            sensors.Clear();
            agent = AgentFactory.CreateAgent(level);

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

                BaseSensor newSensor = SensorFactory.CreateSensor(input);
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
                        string removedSensorType = sensors[index].SensorName;
                        sensors.RemoveAt(index);

                        if (matchedWeaknesses.ContainsKey(removedSensorType) && matchedWeaknesses[removedSensorType] > 0)
                        {
                            matchedWeaknesses[removedSensorType]--;
                            matchedCount--;
                        }
                    }
                }

                Console.WriteLine($"{matchedCount}/{agent.SensorAmount} weaknesses matched");
                if (matchedCount >= agent.SensorAmount)
                {
                    Console.WriteLine($"Agent exposed! Level {level} complete!");
                    gameRunning = false;
                    return true;
                }
            }
            return false;
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