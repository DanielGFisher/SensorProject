using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorProject.Models
{
    public class Player
    {
        public string Name { get; set; }
        public int Level { get; set; } = 1;

        public Player(string name, int level)
        {
            Name = name;
            Level = level;
        }
    }
}
