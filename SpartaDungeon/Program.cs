using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography.X509Certificates;

namespace SpartaDungeon
{
    internal class Program
    {
        static Player player;
        static Item[] shop;
        static Dungeon[] dungeons;

        static int Choose_Behaviour(int start, int end)
        {
            Console.WriteLine("\n\n원하시는 행동을 입력해주세요.");
            Console.Write(">> ");

            int input;
            bool isCorrect = int.TryParse(Console.ReadLine(), out input);

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

        static void BuyItem()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("상점 - 아이템 구매\n");
            Console.ResetColor();
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
            Console.WriteLine("[보유 골드]\n");
            Console.WriteLine($"{player.gold} G\n");
            Console.WriteLine("[아이템 목록]\n");

            int idx = 1;
            foreach (Item item in shop)
            {
                if (item.type == 1) //  무기 장비
                {
                    if (!item.purchased) // 구매하지 않은
                        Console.WriteLine($"- {idx}. {item.name} | 공격력 +{item.effect,3} | {item.info} | {item.price,-5} G");
                    else // 구매한
                        Console.WriteLine($"- {idx}. {item.name} | 공격력 +{item.effect,3} | {item.info} | 구매 완료");
                }
                else // 방어구 장비
                {
                    if (!item.purchased) // 구매하지 않은
                        Console.WriteLine($"- {idx}. {item.name} | 방어력 +{item.effect,3} | {item.info} | {item.price,-5} G");
                    else // 구매한
                        Console.WriteLine($"- {idx}. {item.name} | 방어력 +{item.effect,3} | {item.info} | 구매 완료");
                }
                ++idx;
            }

            Console.WriteLine("\n0. 나가기\n");

            int option = Choose_Behaviour(0, shop.Length);

            if (option >= 1 && option <= shop.Length)
            {
                if (shop[option - 1].purchased == true) // 이미 구매한 아이템
                {
                    Console.WriteLine("이미 구매한 아이템입니다.\n");

                    Console.WriteLine("아무 키나 누르세요.");
                    Console.ReadLine();

                }
                else // 아직 구매하지 않은 아이템
                {
                    if (player.gold >= shop[option - 1].price) // 구매 가능
                    {
                        shop[option - 1].purchased = true;
                        player.gold -= shop[option - 1].price;
                        player.myItems.Add(shop[option - 1]);
                        Console.WriteLine($"{shop[option - 1].name} 구매를 완료했습니다.\n");

                        Console.WriteLine("아무 키나 누르세요.");
                        Console.ReadKey();

                    }
                    else // 재화 부족
                    {
                        Console.WriteLine("Gold 가 부족합니다.\n");

                        Console.WriteLine("아무 키나 누르세요.");
                        Console.ReadKey();
                    }
                }
            }

            else if (option == 0)
                return;
        }

        static void SellItem()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("상점 - 아이템 판매\n");
            Console.ResetColor();
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
            Console.WriteLine("[보유 골드]\n");
            Console.WriteLine($"{player.gold} G\n");
            Console.WriteLine("[아이템 목록]\n");

            foreach (Item item in player.myItems)
            {
                if (item.type == 1) //  무기 장비
                {
                    Console.WriteLine($"- {item.name} | 공격력 +{item.effect} | {item.info,3} | 판매가 {item.price * 0.85,-4} G");
                }
                else // 방어구 장비
                {
                    Console.WriteLine($"- {item.name} | 방어력 +{item.effect} | {item.info,3} | 판매가 {item.price * 0.85,-4} G");
                }
            }

            Console.WriteLine("\n0. 나가기");

            int input = Choose_Behaviour(0, player.myItems.Count);

            if (input == 0)
                return;

            Item sell_item = player.myItems[input - 1];

            if (sell_item.type == 1 && sell_item.equip) //  무기 장비
            {

                player.atk -= sell_item.effect;

            }
            else if (sell_item.type == 2 && sell_item.equip)// 방어구 장비
            {

                player.def -= sell_item.effect;

            }
            player.myItems.Remove(sell_item);
            sell_item.purchased = false;
            sell_item.equip = false;
            player.gold += (int)((float)(sell_item.price) * 0.85f);

            Console.WriteLine($"{sell_item.name} 판매를 완료했습니다.\n");
            Console.WriteLine("아무 키나 누르세요.");
            Console.ReadKey();

        }
        static void Go_Shop()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("상점\n");
            Console.ResetColor();
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
            Console.WriteLine("[보유 골드]\n");
            Console.WriteLine($"{player.gold} G\n");
            Console.WriteLine("[아이템 목록]\n");

            foreach (Item item in shop)
            {
                string item_name = item.name;
                if (item.type == 1) //  무기 장비
                {
                    if (!item.purchased) // 구매하지 않은
                        Console.WriteLine($"- {item.name} | 공격력 +{item.effect,3} | {item.info} | {item.price,-4} G");
                    else // 구매한
                        Console.WriteLine($"- {item.name} | 공격력 +{item.effect,3} | {item.info} | 구매 완료");
                }
                else // 방어구 장비
                {
                    if (!item.purchased) // 구매하지 않은
                        Console.WriteLine($"- {item.name} | 방어력 +{item.effect,3} | {item.info} | {item.price,-4} G");
                    else // 구매한
                        Console.WriteLine($"- {item.name} | 방어력 +{item.effect,3} | {item.info} | 구매 완료");
                }

            }

            Console.WriteLine("\n1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("0. 나가기");

            int option = Choose_Behaviour(0, 2);

            if (option == 1)
            {
                BuyItem();
                Go_Shop();
            }

            else if (option == 2)
            {
                SellItem();
                Go_Shop();
            }

            else if (option == 0)
                return;
        }

        static void LevelCheck()
        {
            if (player.exp >= player.level)
            {
                player.level += 1;
                player.atk += 0.5f;
                player.def += 1f;
                Console.WriteLine($"레벨업! Lv. {player.level.ToString("D2")}, 공격력 : {player.atk} (+0.5) , 방어력 : {player.def} (+1.0)");
                player.exp = 0;
            }

        }
        static void Clear_Dungeon(Dungeon d)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("던전 클리어\n");
            Console.ResetColor();
            Console.WriteLine("축하합니다!!");
            Console.WriteLine($"{d.dungeonName} 을 클리어 하였습니다.\n");

            Console.WriteLine("[탐험 결과]\n");

            Random random = new Random();
            int damage = random.Next(20, 36) - (int)(player.def - d.recommendDef);
            int before_hp = player.hp;
            player.hp -= damage;
            player.exp += 1;
           
            Console.WriteLine($"체력 : {before_hp} -> {player.hp}\n");

            int before_gold = player.gold;
            player.gold += d.reward + (int)((float)d.reward * (float)random.Next((int)player.atk, (int)player.atk * 2) * 0.01f);
            Console.WriteLine($"Gold : {before_gold} G -> {player.gold} G\n");

            LevelCheck();

            Console.WriteLine("\n0.나가기\n");

            int input = Choose_Behaviour(0, 0);

            if (input == 0)
                return;
        }

        static void Fail_Dungeon(Dungeon d)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("던전 클리어 실패\n");
            Console.ResetColor();
            Console.WriteLine($"{d.dungeonName} 을 클리어하지 못하였습니다.\n");

            Console.WriteLine("[탐험 결과]\n");

            int before_hp = player.hp;
            player.hp /= 2;

            Console.WriteLine($"체력 : {before_hp} -> {player.hp}\n");
            Console.WriteLine($"Gold : {player.gold} G -> {player.gold} G\n");

            Console.WriteLine("\n0.나가기\n");

            int input = Choose_Behaviour(0, 0);

            if (input == 0)
                return;
        }
        static void Entry_Dungeon()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("던전 입장\n");
            Console.ResetColor();
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");

            Console.WriteLine("[던전 목록]\n");
            int idx = 1;
            foreach (Dungeon dungeon in dungeons)
            {
                Console.WriteLine($"{idx}. {dungeon.dungeonName} | 방어력 {dungeon.recommendDef} 이상 권장");
                ++idx;
            }
            Console.WriteLine("\n0. 나가기");

            int input = Choose_Behaviour(0, dungeons.Length);

            if (input == 0)
                return;

            else
            {
                float def = player.def;
                float recommend_def = dungeons[input - 1].recommendDef;

                if (def < recommend_def)
                {
                    Random random = new Random();
                    int percent = random.Next(0, 101);
                    if (percent <= 40)
                    {
                        Fail_Dungeon(dungeons[input - 1]);
                        //Entry_Dungeon();
                    }
                    else
                        Clear_Dungeon(dungeons[input - 1]);

                }
                else
                    Clear_Dungeon(dungeons[input - 1]);
            }


            return;
        }

        static void SaveData()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("데이터 저장 중...\n");
            Console.ResetColor();
            Thread.Sleep(300);

            string playerDataName = "playerStatData.json";
            string itemDataName = "itemData.json";
            // 데이터 경로 저장. (C드라이브, Documents)
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string playerDataPath = Path.Combine(path, playerDataName);
            string itemDataPath = Path.Combine(path, itemDataName);

            string playerJson = JsonConvert.SerializeObject(player, Formatting.Indented);
            string itemJson = JsonConvert.SerializeObject(player.myItems, Formatting.Indented);
            File.WriteAllText(playerDataPath, playerJson);
            File.WriteAllText(itemDataPath, itemJson);

            Console.ForegroundColor= ConsoleColor.Cyan;
            Console.WriteLine("저장이 완료되었습니다.");
            Console.ResetColor();
        }
        static void BreakTime()
        {
            Console.Clear();
            Console.WriteLine("휴식하기");
            Console.WriteLine($"500 G 를 내면 체력을 회복할 수 있습니다. (보유 골드 : {player.gold} G)\n");

            Console.WriteLine("1. 휴식하기");
            Console.WriteLine("0. 나가기\n");

            int input = Choose_Behaviour(0, 1);

            if (input == 0) return;

            if (player.hp == 100)
            {
                Console.WriteLine("\n더 이상 회복할 체력이 없습니다.\n");
                SaveData();
                Console.WriteLine("\n0.나가기");
                input = Choose_Behaviour(0, 0);
                if (input == 0)
                    return;
            }

            if (player.gold < 500)
            {
                Console.WriteLine("\nGold 가 부족합니다.");
                Console.WriteLine("\n0. 나가기");
                input = Choose_Behaviour(0, 0);

                if (input == 0) return;
            }


            Console.WriteLine("\n[휴 식]");
            Console.WriteLine($"체 력 {player.hp} -> 100 \n");
            player.hp = 100;
            player.gold -= 500;

            SaveData();
            Console.WriteLine("\n0. 나가기");
            input = Choose_Behaviour(0, 0);

            if (input == 0) return;
        }
        static void Menu()
        {
            int option;
            do
            {
                Console.Clear();

                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n");

                Console.WriteLine("1. 상태보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.WriteLine("4. 던전 입장");
                Console.WriteLine("5. 휴식하기");
                Console.WriteLine("0. 나가기\n\n");

                //Console.WriteLine("원하시는 행동을 입력해주세요.");
                //Console.Write(">> ");

                option = Choose_Behaviour(0, 5);

                switch (option)
                {
                    case 0: break;
                    case 1: player.Show_Stat(); break;
                    case 2: player.Show_Inven(); break;
                    case 3: Go_Shop(); break;
                    case 4: Entry_Dungeon(); break;
                    case 5: BreakTime(); break;
                }
            } while (option != 0);

            return;
        }

        static void LoadData()
        {
            string playerDataName = "playerStatData.json";
            string itemDataName = "itemData.json";

            // C 드라이브 - MyDocuments 폴더
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            string playerDataPath = Path.Combine(path, playerDataName);

            if(File.Exists(playerDataPath)) // 데이터 존재
            {
                string playerJson = File.ReadAllText(playerDataPath);
                player = JsonConvert.DeserializeObject<Player>(playerJson);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("플레이어 데이터를 성공적으로 불러왔습니다!");
                Console.ResetColor();
                Thread.Sleep(300);
            }
            else
            {
                Console.WriteLine("저장된 플레이어 데이터가 없습니다.");
                player = new Player("Kim");
                Thread.Sleep(300);
            }

            string itemDataPath = Path.Combine(path, itemDataName);
            if (File.Exists(itemDataPath)) // 데이터 존재
            {
                string itemJson = File.ReadAllText(itemDataPath);
                player.myItems = JsonConvert.DeserializeObject<List<Item>>(itemJson);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("아이템 데이터를 성공적으로 불러왔습니다!");
                Console.ResetColor();
                Console.WriteLine("\n아무 키나 눌러 게임을 시작..");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("저장된 아이템 데이터가 없습니다.");
                Console.WriteLine("\n아무 키나 눌러 게임을 시작..");
                Console.ReadKey();
            }
        }

        static void Main(string[] args)
        {

            // 데이터 불러오기
            LoadData();

            // 아이템 생성
            Item noviceArmor = new Item("수련자 갑옷    ", 2, 5, "수련에 도움을 주는 갑옷입니다.                   ", 1000);
            Item ironArmor = new Item("무쇠 갑옷      ", 2, 9, "무쇠로 만들어져 튼튼한 갑옷입니다.               ", 1800);
            Item spartanArmor = new Item("스파르타의 갑옷", 2, 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500);
            Item oldSword = new Item("낡은 검        ", 1, 2, "쉽게 볼 수 있는 낡은 검입니다.                   ", 600);
            Item bronzeAx = new Item("청동 도끼      ", 1, 5, "어디선가 사용됐던 거 같은 도끼입니다.            ", 1500);
            Item spartanSpear = new Item("스파르타의 창  ", 1, 7, "스파르타의 전사들이 사용했다는 전설의 창입니다.  ", 2700);
            Item infinityEdge = new Item("무한의 대검    ", 1, 65, "협곡에서 빌려온 전설의 무기입니다.               ", 3300);

            // 상점에 아이템 추가
            shop = new Item[] { noviceArmor, ironArmor, spartanArmor, oldSword, bronzeAx, spartanSpear, infinityEdge };

            // 던전 추가
            Dungeon easyDungeon = new Dungeon("쉬운 던전", 5, 1000);
            Dungeon normalDungeon = new Dungeon("일반 던전", 11, 1700);
            Dungeon hardDungeon = new Dungeon("어려운 던전", 17, 2500);

            dungeons = new Dungeon[] { easyDungeon, normalDungeon, hardDungeon };

            // 게임 시작 
            Menu();


        }
    }
}
