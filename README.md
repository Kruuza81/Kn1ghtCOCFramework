# Kn1ghtCOCFramework
*My English Isn't Good,So I Use The Deepseek To Translate,It's Might Has Some Wrongs.
## Introduction
KnightCOCFramework is a framework designed to assist creators in developing their own tabletop role-playing games.
Therefore, using KnightCOCFramework requires some programming knowledge, but don't worry, this document will provide a zero-based tutorial to help you complete your project smoothly.
Since this project is primarily aimed at the general public, it will explain some commonly used terms in the IT industry.
## Environment Requirements
> Environment: Refers to the configuration of your device, including the software installed.
If you want to develop and run the project yourself, we recommend that your device meets the following requirements:
1. Use Windows 10/11 or Linux operating system
2. Install Visual Studio or Visual Studio Code (recommended)
3. Install the .NET runtime
4. RAM ≥ 12GiB
5. Free storage ('memory') ≥ 15GiB
If all the above conditions are met, you can easily use this framework to create your game.
## What if the device does not have a PC (personal computer) operating system?
If your device does not have a PC operating system, the development process will be somewhat challenging.
In this case, I suggest you either write the code directly using a note-taking app on your phone and let Kn1ght debug and run it for free, or pay Kn1ght 16.8 yuan(2 doller) to write it for you hhh.(Chinese Only)
However, buying a computer is not an insurmountable challenge. Generally, you can assemble a barely usable PC for around 200~1000 yuan（25 ~ 125 doller）. You can search for tutorials on Bilibili, Douyin, or Tieba (Tualatin Bar). Using AI or Baidu is not recommended.
## Development Tutorial
### 1. Understanding the Player Class
> Class: A programming term, roughly meaning a collection of many properties and functions, which can be understood as a "class" in games. Using "classes" can easily implement various functions.
In the Player class, you can find the following code:
```csharp
using System.Drawing;

namespace Kn1ghtCOCFramework
{

    public enum Results  //Dice roll results
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
        public string Name { get; set; } //Name
        public int San { get; set; } //Affects player's dice roll results, range -60~60
        public  int HealthLimit { get; set; }
        public  int Health { get; set; } //Determines if the player can continue the game normally
        public  int MP { get; set; } //Determines if the player can use skills
        public  int MPLimit { get; set; }
        public  int Strength { get; set; } //Strength, will fluctuate if San is less than 0, cannot exceed 110%
        public  int Speed { get; set; } //Speed, normally affected but cannot exceed 102%
        public  int IQ { get; set; } //Intelligence, affected doubly by San
        public  int PS { get; set; } //Physical strength, not affected by San
        public  int Will { get; set; } //Willpower, affected doubly by San
        public  int Talking { get; set; } //Speech, normally affected
        public  int Memory { get; set; } //Memory, normally affected
        public  int SkillValue { get; set; } //Skill, normally affected, determines skill effects
        public  int Charm { get; set; } //Charm, not affected by San
        public  List<string> Items { get; set; } //Items, determine event outcomes, but may be lost
        public  List<string> Tags { get; set; } //Tags, determine event outcomes, and cannot be lost
        public  List<string> Skills { get; set; } //Skills, affect self, NPCs, or other players
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
           else throw new ArgumentException("Roll error, abnormal difference between dice roll and target");
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
                    throw new ArgumentException("Attribute read request error, please check spelling");
            }
        }

    }


}
```
Each property is followed by a comment (the gray text after //).
Experienced tabletop RPG players should easily understand them.
You can add, delete, or modify some properties. Here is the related tutorial.
#### Adding New Attributes
Suppose we want to add a Gun attribute, we can modify the original code as follows (lines 32, 62, 110):
```csharp
using System.Drawing;

namespace Kn1ghtCOCFramework
{

    public enum Results  //Dice roll results
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
        public string Name { get; set; } //Name
        public int San { get; set; } //Affects player's dice roll results, recommended range -45~45
        public  int HealthLimit { get; set; }
        public  int Health { get; set; } //Determines if the player can continue the game normally
        public  int MP { get; set; } //Determines if the player can use skills
        public  int MPLimit { get; set; }
        public  int Strength { get; set; } //Strength, will fluctuate if San is less than 0, cannot exceed 110%
        public  int Speed { get; set; } //Speed, normally affected but cannot exceed 102%
        public  int IQ { get; set; } //Intelligence, affected doubly by San
        public  int PS { get; set; } //Physical strength, not affected by San
        public  int Will { get; set; } //Willpower, affected doubly by San
        public  int Talking { get; set; } //Speech, normally affected
        public  int Memory { get; set; } //Memory, normally affected
        public  int SkillValue { get; set; } //Skill, normally affected, determines skill effects
        public  int Charm { get; set; } //Charm, not affected by San
        public  int Gun { get; set; } //Gun, affected doubly by San
        public  List<string> Items { get; set; } //Items, determine event outcomes, but may be lost
        public  List<string> Tags { get; set; } //Tags, determine event outcomes, and cannot be lost
        public  List<string> Skills { get; set; } //Skills, affect self, NPCs, or other players
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
                case "gun":
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
           else throw new ArgumentException("Roll error, abnormal difference between dice roll and target");
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
                case "gun"
	                return this.Gun
                default:
                    throw new ArgumentException("Attribute read request error, please check spelling");
            }
        }

    }


}
```
It's easy to see that when an attribute is affected by San, it is directly added to San. This is actually a basic mathematical principle: adding a negative number is equivalent to subtracting that number.
The three lines mentioned above each contain a piece of code about the gun, which are:
```csharp
public  int Gun { get; set; } //Gun, affected doubly by San
```
This code is necessary, it defines the Gun attribute.
```csharp
case "gun":
                    Value = PlayerAttributeValue + 2 * this.San;
                    break;
```
This code declares the special mechanism by which Gun is affected by San.
If your custom attribute is normally affected by San, you can omit this code.
```csharp
                case "gun"
	                return this.Gun
```
This code is used in the GetPlayerAttributeValue function to return the value of the attribute based on the string. If your attribute needs to be rolled, then you must add this code, otherwise you will not be able to use the Roll function to roll it.
> String: Some arbitrary text.
During development, I originally wanted to directly read the string and then use delegates, mappings, enumerations, etc. to directly access those attributes, reducing the amount of code. But after researching for a long time, I found that it didn't seem to work, so I let Deepseek do it, and it directly used a switch.
This function is related to the underlying logic of dice rolling.
#### Deleting and Modifying Attributes
After reading the section on adding new attributes, you should also know how to delete and modify attributes, that is, delete or modify the three pieces of code mentioned above.
#### Dice Rolling
The most important part is here - dice rolling.
The main code associated with dice rolling is as follows:
```csharp
public enum Results  //Dice roll results
    {
        Great,
        Good,
        Normal,
        Wrong,
        Bad,
        Defeat,
    }

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
                case "gun":
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
           else throw new ArgumentException("Roll error, abnormal difference between dice roll and target");
        }
```
First, the enum type above contains 6 dice roll results: Great success, Good success, Success, Failure, Bad failure, Defeat.
Their roles are:
1. Conveniently represent dice roll results in code.
2. Conveniently perform unified judgments (i.e., when the dice roll result is within a specified range, uniformly return a certain result).
> Return: A programming term, a function gives a value to the place where it is called and ends execution.
    Can be compared to: The teacher asks you to do homework, you go to do homework, and after finishing, you hand in the homework to the teacher, stopping the process of doing homework.
It should be noted that the value of Great success Great is 0, Good is 1, and so on.
Moreover, when comparing, you need to add an (int) in front, its role is to convert a certain value to an integer type.
##### Performing Actions Based on Dice Roll Results
Next, let's learn how to perform actions based on dice roll results. First, let's sort out the ideas:
First, you must call the dice roll function.
Second, judge the dice roll result.
Finally, add corresponding actions under each dice roll result branch.
Then, we can write the following code:
```csharp
Result result = Roll("gun",50) //Perform gun dice roll
var player =new Player {Name="Neon",Gun=100,San=0}; //Here, because it's just an example, this is ok, in fact, a Player has more attributes
switch(result)
{
case Result.Great:
	Console.WriteLine("Excellent marksmanship, "+player.Name+" received praise from all onlookers.");
	player.San += 15;
	break;
case Result.Good:
	Console.WriteLine("Good marksmanship,"+player.Name+" received praise from most onlookers, some just nodded.");
	player.San += 10;
	break;
case Result.Normal:
	Console.WriteLine("Average marksmanship"+player.Name+" received some praise from novices.");
	player.San += 5;
	break;
case Result.Wrong:
	Console.WriteLine(player.Name+" is not in good condition, let TA go home.");
	break;
case Result.Bad:
case Result.Defeat:
	Console.WriteLine("Go home and farm, "+player.Name);
	player.San -= 10;
	break;

}
```
It can be noticed that lines 20 and 21 have cases connected together, the meaning of this is to set a unified result for these two results. Other parts can also do this.
break; is used to exit the switch, if there is no code below, then the program may end directly.
Console.WriteLine(); is used to output a piece of text in the terminal/console, if you delete Line, it will not wrap.
However, adding a \n in front of Console.Write(); can also achieve the effect of wrapping, if we want to output English double quotes, we can use \".
#### Items and Tags - Understanding Generic Class List<T> - List<Type>
We can notice that there are three lines of code like this:
```csharp
public  List<string> Items { get; set; } //Items, determine event outcomes, but may be lost
        public  List<string> Tags { get; set; } //Tags, determine event outcomes, and cannot be lost
        public  List<string> Skills { get; set; } //Skills, affect self, NPCs, or other players
```
Skills are skills, not done yet, the focus is on items and tags.
Actually, tags and items can be together, but for convenience of management, we separated them.
We can write more event branches by judging whether the player has certain items or tags.
Here are the four basic operations
