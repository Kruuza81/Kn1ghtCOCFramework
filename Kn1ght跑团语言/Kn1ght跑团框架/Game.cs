using System;
using System.Collections.Generic;

namespace Kn1ghtCOCFramework
{
    public class Game
    {
        public static int PointsLimit { get; set; } = 1100; // 定义初始加点值，建议1100点
        public static List<string> GameItem { get; set; } = new List<string>(); // 游戏流程物品
        public static List<Player> Players { get; set; } = new List<Player>();

        static void Main()
        {
            while (true)
            {
                Console.WriteLine("创建角色阶段，输入你的选择.(1,创建角色 2,开始游戏);错误的输入将默认为选择开始游戏");
                ConsoleKeyInfo Choose = Console.ReadKey();
                if (Choose.Key == ConsoleKey.NumPad1 || Choose.Key == ConsoleKey.D1)
                {
                    NewPlayer();
                }
                else
                {
                    Event.Init();
                    break; // 开始游戏后退出循环
                }
            }
        }

        static void NewPlayer()
        {
            Console.Write("输入角色名称:");
            string Name = Console.ReadLine();
            string[] Args = { "血量", "技能值(MP)", "力量", "速度", "智力", "体力", "意志力", "话术", "记忆", "技巧(影响技能效果)", "魅力" };
            Console.WriteLine(Name + ",明白了。");
            int Points = PointsLimit;
            int[] ArgValues = new int[11];

            for (int i = 0; i < Args.Length; i++)
            {
                while (true)
                {
                    Console.WriteLine("你还有" + Points + "点加点值,分配多少" + Args[i] + "呢？");
                    if (int.TryParse(Console.ReadLine(), out int inputValue))
                    {
                        ArgValues[i] = inputValue;
                        Points = Points - ArgValues[i];
                        if (Points < 0)
                        {
                            Console.WriteLine("没点数了,加点失败");
                            Points = Points + ArgValues[i];
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("输入无效，请输入一个整数。");
                    }
                }
            }

            var TheNewPlayer = new Player()
            {
                Name = Name,
                HealthLimit = ArgValues[0],
                MPLimit = ArgValues[1],
                Strength = ArgValues[2],
                Speed = ArgValues[3],
                IQ = ArgValues[4],
                PS = ArgValues[5],
                Will = ArgValues[6],
                Talking = ArgValues[7],
                Memory = ArgValues[8],
                SkillValue = ArgValues[9],
                Charm = ArgValues[10],
                San = 0,
                Health = ArgValues[0],
                MP = ArgValues[1],
                Tags = new List<string>() { "玩家" },
                Items = new List<string>() { "大脑" },
                Skills = new List<string>() { "思考" },
            };

            Console.WriteLine("角色" + TheNewPlayer.Name + "创建完成。");
            Players.Add(TheNewPlayer);
        }
    }
}