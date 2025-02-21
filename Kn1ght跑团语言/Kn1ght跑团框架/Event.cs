using Kn1ghtCOCFramework;

public static class Event
{
    public static Dictionary<string,string> EventChooses;
    public static void Init()
    {
        while (true)
        {
            Console.WriteLine("\n初始事件触发，有a,b两个选项" + "\n输入?或？查看指令列表");
            string command = Console.ReadLine();
            switch (command.Split(" ")[0])
            {
                case "?":
                case "？":
                case "help":
                    Console.WriteLine("l/ls 查看当前事件每个角色的选择,要求选择时才生效");
                    Console.WriteLine("c/cg [角色名] [选项名] 修改或决定某个角色在当前事件的选择,要求选择时才生效;角色生命值归零无法进行这个操作");
                    Console.WriteLine("end 结束事件选择,进行结算");
                    Console.WriteLine("v/vi/view [角色名] 查看角色信息");
                    Console.WriteLine("s/st/start 正式开始跑团");
                    Console.WriteLine("--角色的默认选择为'a'--");
                    break;
                case "v":
                case "vi":
                case "view":
                    try
                    {
                        string[] parts = command.Split(" ");
                        if (parts.Length < 2)
                        {
                            Console.WriteLine("指令错误: 缺少角色名");
                            break;
                        }

                        int playerIndex = fc(parts[1]);
                        if (playerIndex == -1)
                        {
                            Console.WriteLine("角色未找到");
                            break;
                        }

                        var player = Game.Players[playerIndex];
                        Console.WriteLine(player.Name);
                        Console.WriteLine("血量:" + player.Health + "/" + player.HealthLimit);
                        Console.WriteLine("蓝量:" + player.MP + "/" + player.MPLimit);
                        Console.WriteLine("理智:" + player.San);
                        Console.WriteLine("力量:" + player.Strength);
                        Console.WriteLine("速度:" + player.Speed);
                        Console.WriteLine("智力:" + player.IQ);
                        Console.WriteLine("体力:" + player.PS);
                        Console.WriteLine("意志力:" + player.Will);
                        Console.WriteLine("话术:" + player.Talking);
                        Console.WriteLine("魅力:" + player.Charm);
                        Console.WriteLine("技巧:" + player.SkillValue);

                        if (player.Tags != null)
                        {
                            foreach (string tag in player.Tags) { Console.WriteLine(tag); }
                        }

                        if (player.Items != null)
                        {
                            Console.WriteLine("物品:");
                            foreach (string item in player.Items) { Console.WriteLine(item); }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("指令错误: " + ex.Message);
                    }
                    break;
                case "s":
                case "st":
                case "start":
                    Init2();
                    break;
                default:
                    Console.WriteLine("错误的指令");
                    break;
            }
        }
    }

    public static void Init2()
    {
            Console.WriteLine("已正式开始,现在开始选择:");
            Int();
        
    }
  
    public static void Int()
    {
        foreach (Player player in Game.Players)
        {
            EventChooses.Add(player.Name, "a");
        }
        //初始化一波事件选择hh
        
        while (true)
        {
            string Input = Console.ReadLine();
            string[] Parts = Input.Split(" ");
            switch (Parts[0]) {
                case "l":
                case "ls":
                    foreach (Player player in Game.Players)
                    {
                        Console.WriteLine(player.Name + ":" + EventChooses[player.Name]);
                    }
                    break;
                case "c":
                case "cg":
                    if (EventChooses.ContainsKey(Parts[1]))
                    {
                        EventChooses[Parts[1]] = Parts[2];
                        Console.WriteLine("修改完成");
                    }
                    else Console.WriteLine("这个人是棍母");
                    break;
                case "end":
                    return;
                default:
                    Console.WriteLine("你的指令是棍母");
                    break;
            }
        }
    }

    public static int fc(string name) => Game.Players.FindIndex(item => item.Name == name);
}



