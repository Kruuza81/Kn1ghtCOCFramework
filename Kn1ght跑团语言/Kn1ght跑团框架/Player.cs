using System.Drawing;

namespace Kn1ghtCOCFramework
{

    public enum Results  //骰点结果
    {
        Great,
        Good,
        Normal,
        Wrong,
        Bad,
        Defeat,
    }
    public class Player
    {
        public string Name { get; set; } //名字
        public int San { get; set; } //影响玩家骰点结果,范围-60~60
        public  int HealthLimit { get; set; }
        public  int Health { get; set; } //决定玩家是否能够正常继续游戏
        public  int MP { get; set; } //决定玩家能否使用技能
        public  int MPLimit { get; set; }
        public  int Strength { get; set; } //力量,San小于0时将令其浮动,无法提升到110%以上
        public  int Speed { get; set; } //速度,正常影响但无法提升到102%以上
        public  int IQ { get; set; } //智力,将受到San的双倍影响
        public  int PS { get; set; } //体力,不受到San的影响
        public  int Will { get; set; } //意志力,将受到San的双倍影响
        public  int Talking { get; set; } //话术,正常影响
        public  int Memory { get; set; } //记忆,正常影响
        public  int SkillValue { get; set; } //技巧,正常影响,决定技能效果
        public  int Charm { get; set; } //魅力,不受到San的影响
        public  List<string> Items { get; set; } //物品,决定事件结果,但可能会丢失
        public  List<string> Tags { get; set; } //标签,决定事件结果,且不可能会丢失
        public  List<string> Skills { get; set; } //技能,影响自己或NPC亦或其他玩家
        public  Results Roll(string Arg, int Min)
        {
            Random Rolling = new Random();
            int PlayerAttributeValue = GetPlayerAttributeValue(Arg);
            int Value = 0;
            switch (Arg)
            {
                case "strength":
                    if (this.San >= 0)
                    {
                        Value = (int)Math.Min((int)(1.1*PlayerAttributeValue), PlayerAttributeValue+this.San);
                       
                        break;
                    }
                    else {
                        Value = PlayerAttributeValue + Rolling.Next(this.San,-this.San);
                        break;
                    }
                case "speed":
                    Value = (int)Math.Min((int)(1.02 * PlayerAttributeValue), PlayerAttributeValue + this.San);
                    break;
                case "iq":
                    Value = PlayerAttributeValue + 2 * this.San;
                    break;
                case "will":
                    Value = PlayerAttributeValue + 2 * this.San;
                    break;
                case "ps":
                    Value = PlayerAttributeValue;
                    break;
                case "charm":
                    Value = PlayerAttributeValue;
                    break;
                default:
                    Value = PlayerAttributeValue + this.San;
                    break;

            }
            int Rand = Rolling.Next(1,Value);
            int Diff = 0;
            Diff = Min - Rand;
            if(Diff <0 && Diff >-10) return Results.Normal;
            if (Diff <= -10 && Diff > -20) return Results.Good;
            if(Diff <= -20) return Results.Great;
            if (Diff >= 1 && Diff < 10) return Results.Wrong;
            if(Diff >=10 && Diff<20) return Results.Bad;
            if (Diff >= 20) return Results.Defeat;
           else throw new ArgumentException("Roll点出现错误,骰点与目标的差值异常");
        }

        private  int GetPlayerAttributeValue(string Arg)
        {
            switch (Arg.ToLower())
            {
                case "strength":
                    return this.Strength;
                case "speed":
                    return this.Speed;
                case "iq":
                    return this.IQ;
                case "ps":
                    return this.PS;
                case "will":
                    return this.Will;
                case "talking":
                    return this.Talking;
                case "memory":
                    return this.Memory;
                case "skillvalue":
                    return this.SkillValue;
                case "charm":
                    return this.Charm;
                default:
                    throw new ArgumentException("属性读取请求异常,请检查拼写是否错误");
            }
        }

    }


}