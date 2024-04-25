using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    internal class Dungeon
    {
        public string dungeonName;
        public float recommendDef;
        public int reward;

        public Dungeon(string dungeonName, float recommendDef, int reward)
        {
            this.dungeonName = dungeonName;
            this.recommendDef = recommendDef;
            this.reward = reward;
        }

        
    }
}