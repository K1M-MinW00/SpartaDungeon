using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    internal class Player
    {
        public int level = 1;
        public string name = "Unknown";
        public string job = "전사";
        public float atk = 10f;
        public float def = 5f;
        public int hp = 100;
        public int gold = 1500;
        public int exp = 0; 
        

        public List<Item> myItems = new List<Item>();

        public Player()
        {

        }
        public Player(string name)
        {
            this.name = name;
        }
        public int Choose_Behaviour(int start, int end)
        {
            Console.WriteLine("\n\n원하시는 행동을 입력해주세요.");
            Console.Write(">> ");

            int input;
            bool isCorrect = int.TryParse(Console.ReadLine(),out input);

            if (isCorrect && input >= start && input <= end)
            {
                return input;
            }

            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("잘못된 입력입니다.\n");
                Console.ResetColor();
                return Choose_Behaviour(start, end);
            }
        }
        public void Show_Stat()
        {
            int added_atk = 0;
            int added_def = 0;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("상태 보기\n");
            Console.ResetColor();
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();

            Console.WriteLine($"Lv. {level.ToString("D2")}");
            Console.WriteLine($"{name} ({job})");

            foreach (Item item in myItems)
            {
                if (item.type == 1 && item.equip) // 착용한 장비가 무기
                    added_atk += item.effect;
                else if (item.type == 2 && item.equip) // 착용한 장비가 방어구
                    added_def += item.effect;
            }

            if (added_atk == 0)
                Console.WriteLine($"공격력 : {atk}");
            else
                Console.WriteLine($"공격력 : {atk} (+{added_atk})");

            if (added_def == 0)
                Console.WriteLine($"방어력 : {def}");
            else
                Console.WriteLine($"방어력 : {def} (+{added_def})");

            Console.WriteLine($"체 력 : {hp}");
            Console.WriteLine($"Gold : {gold} G");

            Console.WriteLine("\n0. 나가기");

            int input = Choose_Behaviour(0, 0);

            return;
        }

        public void Equipment_Manage()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("인벤토리 - 장착 관리\n");
            Console.ResetColor();
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
            Console.WriteLine("[아이템 목록]\n");

            int idx = 1;

            foreach(Item item in myItems)
            {
                if (item.equip)
                {
                    if (item.type == 1) // 무기
                        Console.WriteLine($"-{idx} [E]{item.name,-17} | 공격력 + {item.effect,-3} | {item.info,-20}");
                    else if (item.type == 2) // 방어구
                        Console.WriteLine($"-{idx} [E]{item.name,-17} | 방어력 + {item.effect,-3} | {item.info,-20}");
                }
                else
                {
                    if (item.type == 1) // 무기
                        Console.WriteLine($"-{idx} {item.name,-20} | 공격력 + {item.effect,-3} | {item.info,-20}");
                    else if (item.type == 2) // 방어구
                        Console.WriteLine($"-{idx} {item.name,-20} | 방어력 + {item.effect,-3} | {item.info,-20}");
                }
                ++idx;
            }

            Console.WriteLine("\n\n\n0. 나가기");
            int num = Choose_Behaviour(0, myItems.Count);

            if (num == 0)
                return;

            Item _Item = myItems[num - 1]; // 선택한 아이템

            if (_Item.equip == true) // 이미 장착한 아이템을 선택했을 때 -> 장착 해제
            {
                _Item.equip = false;

                if (_Item.type == 1)
                {
                    atk -= _Item.effect;
                }
                    
                else
                {
                    def -= _Item.effect;
                }
            }

            else if (_Item.equip == false) // 장착하지 않은 아이템을 선택했을 때 -> 장착
            {
                // 장착 중인 장비가 있을 때
                foreach(Item item in myItems)
                {
                    if(item.type == _Item.type && item.equip)
                    {
                        if (item.type == 1)
                            atk-= item.effect;
                        else
                            def-= item.effect;

                        item.equip = false;
                        break;
                    }
                }

                // 장착 중인 장비가 없었을 때
                _Item.equip = true;
                if (_Item.type == 1) // 무기
                {
                    atk += _Item.effect;
                }
                else // 방어구
                {   
                    def += _Item.effect;
                }

            }
            return;
        }


        public void Show_Inven()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("인벤토리\n");
            Console.ResetColor();
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
            Console.WriteLine("[아이템 목록]\n");

            foreach (Item item in myItems)
            {
                if (item.equip)
                {
                    if (item.type == 1) // 무기
                        Console.WriteLine($"- [E]{item.name,-17} | 공격력 + {item.effect,-3} | {item.info,20}");
                    else if (item.type == 2) // 방어구
                        Console.WriteLine($"- [E]{item.name,-17} | 방어력 + {item.effect,-3} | {item.info,-20}");
                }
                else
                {
                    if (item.type == 1) // 무기
                        Console.WriteLine($"- {item.name,-30} | 공격력 + {item.effect,-3} | {item.info, -20}");
                    else if (item.type == 2) // 방어구
                        Console.WriteLine($"- {item.name,-30} | 방어력 + {item.effect,-3} | {item.info, -20}");
                }
            }

            Console.WriteLine("\n\n1. 장착(또는 해제)");
            Console.WriteLine("0. 나가기\n");

            int input = Choose_Behaviour(0, 1);

            if (input == 1)
            {
                Equipment_Manage();
                Show_Inven();
            }
            else if (input == 0)
                return;
            
        }


    }
}