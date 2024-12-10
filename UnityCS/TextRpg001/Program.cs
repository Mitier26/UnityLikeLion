namespace TextRpg001
{
    class Player
    {
        int AT = 10;
        int HP = 50;
        int MAXHP = 100;

        public void StatusRenderer()
        {
            Console.WriteLine("-----------------------------------------");
            Console.Write("공격력 : ");
            Console.WriteLine(AT);
            Console.Write("체력 : ");
            Console.Write(HP);
            Console.Write("/");
            Console.WriteLine(MAXHP);
            Console.WriteLine("-----------------------------------------");
        }

        public int PrintHp()
        {
            Console.WriteLine("");
            Console.Write("현제 플레이어의 HP 는");
            Console.Write(HP);
            Console.WriteLine("입니다.");
            Console.ReadKey();
        }

        public void MaxHeal()
        {
            if(HP>= MAXHP)
            {
                Console.WriteLine("");
                Console.WriteLine("체력이 완벽해 회복할 필요가 없다");
                Console.ReadKey();
            }
            else
            {
                HP = MAXHP;
            }
            PrintHp();
        }
    }

    class Monster
    {

    }

    enum STARTSELECT
    {
        SELECTTOWN, SELECTBATTLE, NONESELECT
    }

    internal class Program
    {
        static STARTSELECT StartSelect()
        {
            Console.Clear();
            
            Console.WriteLine("1. 마을");
            Console.WriteLine("2. 배틀");
            Console.WriteLine("어디로 갈거냐?");

            ConsoleKeyInfo CKI = Console.ReadKey();

            Console.WriteLine("");

            switch (CKI.Key)
            {
                case ConsoleKey.D1:
                    Console.WriteLine("마을로 이동합니다.");
                    Console.ReadKey();
                    return STARTSELECT.SELECTTOWN;
                case ConsoleKey.D2:
                    Console.WriteLine("전장으로 이동합니다.");
                    Console.ReadKey();
                    return STARTSELECT.SELECTBATTLE;
                default:
                    Console.WriteLine("잘못된 선택입니다.");
                    Console.ReadKey();
                    return STARTSELECT.NONESELECT;
            }

        }

        static void Town(Player player)
        {
            while (true)
            {
                Console.Clear();
                player.StatusRenderer();
                Console.WriteLine("마을에서 무슨일을 할거냐");
                Console.WriteLine("1. 체력을 회복한다.");
                Console.WriteLine("2. 무기를 강화한다.");
                Console.WriteLine("3. 마을을 나간다.");

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D1:
                        player.MaxHeal();
                        break;
                    case ConsoleKey.D2:
                        break;
                    case ConsoleKey.D3:
                        return;
                }
            }
        }

        static void Battle()
        {

        }
        static void Main(string[] args)
        {
            // 마을과 싸움터

            //while (true)
            //{
            //    // 객체화 하지 않고도 쓸수 있는 함수 : static 함수
            //    ConsoleKeyInfo keyInfo = Console.ReadKey();
            //}

            Player newPlayer = new Player();

            while (true)
            {
                STARTSELECT SelectCheck =  StartSelect();

                switch (SelectCheck)
                {
                    case STARTSELECT.SELECTTOWN:
                        Town(newPlayer);
                        break;
                    case STARTSELECT.SELECTBATTLE:
                        Battle();
                        break;
                    default:
                        break;
                    
                }
            }

        }
    }
}
