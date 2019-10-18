using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoblinProject
{
    class Goblin
    {
        public string Name { get; set; }
        public int Priority { get; set; }

        public Goblin(string name, int priority)
        {
            Name = name;
            Priority = priority;
        }
    }
}
