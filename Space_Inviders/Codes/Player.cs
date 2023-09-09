using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Inviders
{
    internal class Player
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public Player(string name, int score)
        {
            Score = score;
            Name = name;
        }
    }
}
