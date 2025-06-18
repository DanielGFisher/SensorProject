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
                    if (level == 4)
                    {
                        Console.WriteLine("Mission Complete - You Win!");
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
                Console.WriteLine("\nAvailable sensors: Audio, Thermal, Pulse, Magnetic, Signal, Light");
                Console.Write("Enter sensor type (or type 'exit' to quit): ");
                string input = Console.ReadLine().ToUpper();

                if (input == "EXIT")
                {
                    gameRunning = false;
                    break;
                }

                var newSensor = SensorFactory.CreateSensor(input);
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

                foreach (var sensor in sensors)
                {
                    sensor.Uses++;
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
                    if (sensor is ThermalSensor thermal)
                    {
                        Console.WriteLine(thermal.RevealSensor(agent));
                    }
                    if (sensor is SignalSensor signal)
                    {
                        Console.WriteLine(signal.RevealField(agent));
                    }
                    if (sensor is LightSensor light)
                    {
                        Console.WriteLine(light.RevealTwoFields(agent));
                    }
                }
                if (agent is SquadLeader leader)
                {
                    leader.turnCount++;
                }

                matchedCount = RemoveInactiveAndUnmatchedSensors(matchedCount);

                if (agent is SquadLeader squadLeader && squadLeader.turnCount % 3 == 0)
                {
                    int removalAmount = ((agent is OrganisationLeader || agent is SeniorCommander)) ? 2 : 1;

                    foreach (var mag in sensors.OfType<MagneticSensor>())
                    {
                        if (mag.ProtectionSkips > 0) mag.ProtectionSkips--;
                    }

                    List<BaseSensor> removable = sensors.Where(sensor => !(sensor is MagneticSensor magnetic && magnetic.ProtectionSkips > 0)).ToList();
                    Random rand = new Random();
                    for (int i = 0; i < removalAmount && removable.Count > 0; i++)
                    {
                        int pick = rand.Next(removable.Count);
                        BaseSensor toRemove = removable[pick];
                        sensors.Remove(toRemove);

                        if (matchedWeaknesses.TryGetValue(toRemove.SensorName, out var cnt) && cnt > 0)
                        {
                            matchedWeaknesses[toRemove.SensorName] = cnt - 1;
                            matchedCount--;
                        }

                        removable.RemoveAt(pick);
                    }
                }

                Console.WriteLine($"{matchedCount}/{agent.RevealSensorAmount()} weaknesses matched");
                if (matchedCount >= agent.RevealSensorAmount())
                {
                    Console.WriteLine($"{agent.RevealAgentType()} exposed!");
                    gameRunning = false;
                    return true;
                }
            }
            return false;
        }

        public int RemoveInactiveAndUnmatchedSensors(int count)
        {
            for (int i = sensors.Count - 1; i >= 0; i--)
            {
                var sensor = sensors[i];
                string sensorType = sensor.SensorName;

                bool wasMatched = sensor.HasMatched;
                bool isInactive = !sensor.IsActive;

                if (isInactive || !wasMatched || (sensors[i] is PulseSensor && sensors[i].Uses == 3))
                {
                    if (wasMatched &&
                        matchedWeaknesses.ContainsKey(sensorType) &&
                        matchedWeaknesses[sensorType] > 0)
                    {
                        matchedWeaknesses[sensorType] = Math.Max(0, matchedWeaknesses[sensorType] - 1);
                        count--;
                    }

                    Console.WriteLine($"{sensorType} sensor removed!");
                    sensors.RemoveAt(i);
                }
            }
            return count;
        }

    }
}