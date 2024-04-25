using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    internal class Item
    {
        public string name;
        public int type; // 1 : 무기 , 2 : 방어구
        public int effect;
        public string info;
        public int price;
        public bool purchased;
        public bool equip;

        public Item(string name, int type, int effect, string info, int price)
        {
            this.name = name;
            this.type = type;
            this.effect = effect;
            this.info = info;
            this.price = price;
            purchased = false;
            equip = false;
        }
    }
}