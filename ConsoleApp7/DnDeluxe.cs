using ConsoleApp7;
using DnDeluxe;
using System.Collections.Immutable;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using static DnDeluxe.PlayerManager;
static int GetInt(string prompt)
{
    while (true)
    {
        try
        {
            Console.Write(prompt);
            return Int16.Parse(Console.ReadLine() ?? "-1");
        }
        catch
        { }
        Console.WriteLine("No, please try again.");
        Console.WriteLine("-------------");
    }
}




static void Spacer(int length)
{
    Console.WriteLine();
    for (int i = 0; i < length; i++)
    {  
    Console.Write("="); 
    }
    Console.WriteLine();
}

int storeanswer = 0;
const string bot = "Dungeon Bot: ";
StatManager StatManager = new();
var PlayerManager = new PlayerManager();

Console.WriteLine(bot + "Welcome to the Dungeon Suite v4.5e.");
Console.WriteLine(bot + "This tool is meant to HELP you create a character, not explain every feature.");
Console.WriteLine(bot + "For that, I recommend outside research, especially with DnD Wikidot.");

Console.WriteLine("===========");



void GetDecision(string prompt, int OptionNumber, bool oneover)
{ 
    bool gate = false;
    List<Int64>OptionCount = new List<Int64>();

    for (int i = 0; i <  OptionNumber; i++) 
    {
        OptionCount.Add(i + 1);
    }
   
    while (!gate)
    {
        Console.Write(prompt);
        storeanswer = GetInt("");
        Console.WriteLine(storeanswer);
        if (OptionCount.Contains(storeanswer)) //we aren't looking for one extra value
        {
            gate = true;
        }
        else if(OptionCount.Contains(storeanswer) == false && oneover == true) //we are
        {
            if (storeanswer == OptionNumber + 1)
            {
                gate = true;
            }
        }
        else
        {
            Console.WriteLine("No, please try again.");
        }
    }
    return;
}

GetDecision(bot + "Type '1' to enter the Character Creator, or find out how I react to wrong answers. |", 1, false);

if (storeanswer == 1) // 1 = character StatManager | 2 = character viewer
{
    Console.Write(bot + "Great! let's get started making a character.");
}

Spacer(20);

if (storeanswer == 1) // character StatManager in proper
{
    bool ignoreroller = false;
    var Character = new CharacterBuild();
    var NPC = new NPC();
    bool isNPC;
    GetDecision(bot + "Type '1' to make an NPC (Non Player Character), or '2' to make a Level 1 Player Character. |", 2, false);

    if (storeanswer == 1)
    {
        Console.WriteLine(bot + "Alrighty, an NPC. This will be less orderly.");
        isNPC = true;

        Console.WriteLine(bot + "What vocation, social class, or job type would your NPC have?");

        NPC.VocationName = Console.ReadLine(); Console.WriteLine();
        if (NPC.VocationName == "")
        {
            NPC.VocationName = "Commoner";
        }

        Console.WriteLine(bot + "would you like to make any changes to their stats at all? They will be working with commoner stats otherwise.");
        GetDecision("'1' for yes, '2' for no. |", 2, false);

        if (storeanswer == 1)
        {
            Console.WriteLine(bot + "What should their Hit Point Total be? |");

            Character.MaxHp = GetInt("");


            Console.WriteLine(bot + "What should their Armor Class be? |");

            Character.ArmorClass = GetInt("");

            Character.Speed = GetInt("How fast can they move in a turn? |");

        
        }

        Console.WriteLine(bot + "Now, what is this character's driving goal or dream? it's useful to have on hand. |");

        NPC.RunningThread = Console.ReadLine(); Console.WriteLine();
        if (NPC.RunningThread == "")
        {
            NPC.RunningThread = "Opening a cheese store with his pops";
        }


    }
    else //this is the player Manager
    {
        
        Console.WriteLine(bot + "Alrighty, a Player Character.");
        isNPC = false;
        GetDecision(bot + "Type '1' to roll for your stats, or '2' to use a standard array. |", 2, false);

        if (storeanswer == 1)
        {
            Console.WriteLine(bot + "Alrighty, let's roll for stats.");
            ignoreroller = false;
        }
        else
        {
            Console.WriteLine(bot + "Alrighty, standard array it is.");
            ignoreroller = true;
        }

        int[] Array = { 0,0,0,0,0,0 };

        if (ignoreroller)                                   //replaces array with the standard
        {
            Array = new int[] {15, 14, 13, 12, 10, 8};
        }
        else
        {
            Array = StatManager.StatRoller(Character);          //rolls, is highly volatile
        }
        //=====================================================

        Spacer(20);

        Console.WriteLine(bot + "This is what you rolled: ");
        foreach (int i in Array)
        {
            Console.WriteLine(i);
        }

        void StatAssigner(int[] Array)
        {
            Console.WriteLine("===============");

            int[] AlreadyChosen = { -1, -1, -1, -1, -1, -1 }; //if this ever matches storeanswer, forbids the selection



            string[] displaybank = { "", "", "", "", "", "" };


            Character.Strength = 10;
            Character.Dexterity = 10;
            Character.Consitution = 10;
            Character.Wisdom = 10;
            Character.Intelligence = 10;
            Character.Charisma = 10;


            for (int i = 0; i < Array.Length; i++)
            {
                Console.Clear();
                bool gate = false;
                while (!gate)
                {

                    StatManager.StatDisplay(Array, displaybank);

                    GetDecision(bot + "Type '1 - 6' to pick where your '" + Array[i] + "' goes. |", 6, false);

                    if (AlreadyChosen.Contains(storeanswer))
                    {
                        Console.WriteLine(bot + "No, please try again.");
                    }
                    else
                    {
                        StatManager.StatSet(Character, storeanswer, Array[i]);
                        AlreadyChosen[i] = storeanswer;
                        
                        
                        displaybank[storeanswer - 1] = Array[i].ToString();
                        gate = true;
                    }
                }
                Console.WriteLine("================");
            }
            Console.Clear();
            StatManager.StatDisplay(Array, displaybank);
        }

        bool door = false;
        while (!door)
        {
            StatAssigner(Array);
            door = true;
        }

        int plus2bonus = 0;
        int plus1bonus = 0;

        GetDecision(bot + "Type '1' if you would like to configure racial bonuses now, or '2' if you would like to skip it. |", 2, false);

        if(storeanswer == 1) // requires outside input, allows for homebrew as well
        {
            Console.WriteLine("How many '+2' bonuses would your character get?");
            plus2bonus = GetInt(bot + "(Type '0' for none) |");
            Console.WriteLine("===============");
            Console.WriteLine("How many '+1' bonuses would your character get?");
            plus1bonus = GetInt(bot + "(Type '0' for none) |");
        }

        while (plus1bonus > 0) 
        {
            StatManager.StatDisplay(Character);
            GetDecision(bot + "Type '1 - 6' to pick which stat gets the +1 bonus. |", 6, false);
            switch (storeanswer)
            {
                case 1:
                    Character.Strength = Character.Strength + 1; break;

                case 2:
                    Character.Dexterity = Character.Dexterity + 1; break;
                case 3:
                    Character.Consitution = Character.Consitution + 1; break;
                case 4:
                    Character.Wisdom = Character.Wisdom + 1; break;
                case 5:
                    Character.Intelligence = Character.Intelligence + 1; break;
    
                case 6:
                    Character.Charisma = Character.Charisma + 1; break;
            }
            plus1bonus--;
        }
        while (plus2bonus > 0)
        {
            StatManager.StatDisplay(Character);
            GetDecision(bot + "Type '1 - 6' to pick which stat gets the +2 bonus. |", 6, false);
            switch (storeanswer)
            {
                case 1:
                    Character.Strength = Character.Strength + 2; break;

                case 2:
                    Character.Dexterity = Character.Dexterity + 2; break;
                case 3:
                    Character.Consitution = Character.Consitution + 2; break;
                case 4:
                    Character.Wisdom = Character.Wisdom + 2; break;
                case 5:
                    Character.Intelligence = Character.Intelligence + 2; break;
                case 6:
                    Character.Charisma = Character.Charisma + 2; break;
            }
            plus2bonus--;
        }

        StatManager.StatDisplay(Character);

        Console.ReadKey();

        //StatManager.AddRacialBonus()




        //New Section, we are done with stat reading

        

        var deserializer = new DeserializerBuilder()
           .WithNamingConvention(UnderscoredNamingConvention.Instance)
           // see height_in_inches in sample yml 
           .Build();

        var yamlstring = File.ReadAllText(@"C:\Users\eicho\source\repos\ConsoleApp7\Formatting.yaml");

        var config = deserializer.Deserialize<ClassConfig>(yamlstring);

        
        Console.WriteLine(bot + "Ok, now we have to determine your class.");

        door = false;
        while (!door)
        {
            Console.WriteLine("====================");

            PlayerManager.ClassList(config);

            Console.WriteLine("====================");


            GetDecision(bot + "Type '1' - '" + config.Classes.Count + "' to pick a class. |", config.Classes.Count, false);
            storeanswer--;
            int tempclassstore = storeanswer;

            Console.WriteLine(storeanswer);

            if (storeanswer <= config.Classes.Count)
            {
                PlayerManager.ClassDisplay(config, storeanswer);

                GetDecision(bot + "Type '1' to pick this as your starting class, or '2' to go back to the class list. |", 2, false);

                if (storeanswer == 1)
                {
                    door = true;

                    Console.Clear();

                    int choicelimit = config.Classes[tempclassstore].SkillAmount;

                    List<string> alreadypicked = new List<string>(choicelimit);

                    Character.Class = config.Classes[tempclassstore];

                    while (choicelimit > 0)
                    {
                        Console.Clear();
                        PlayerManager.ClassSkillDisplay(config, tempclassstore, choicelimit);
                        GetDecision(bot + "Type '1' - '" + config.Classes[tempclassstore].SkillList.Count + "' to pick the skill you want.", config.Classes[tempclassstore].SkillList.Count, false);
                        storeanswer--;
                        if (!alreadypicked.Contains(config.Classes[tempclassstore].SkillList[storeanswer]))
                        {
                            alreadypicked.Add(config.Classes[tempclassstore].SkillList[storeanswer]);

                            Console.WriteLine(config.Classes[tempclassstore].SkillList[storeanswer]);

                            choicelimit--;
                        }
                        else
                        {
                            Console.WriteLine(bot + "You already picked this one.");
                        }
                    }

                    Character.Class.SkillList.Clear();
                    Character.Class.SkillList = alreadypicked;

                    Console.WriteLine("======================");
                }
                else
                {
                    Console.Clear();
                    door = false;
                }
            }
            
            storeanswer = tempclassstore;
        }
            Console.WriteLine(bot + "Now pick what equipment your character will start with.");
            Character.Class.EquipmentListA = PlayerManager.ClassEquipmentDisplay(config, storeanswer);

            Spacer(20);

            for(int i = 0; i < Character.Class.EquipmentListA.Count; i++)
            {
                Console.WriteLine("- " + Character.Class.EquipmentListA[i]);
            }
        

        Console.Clear();

        Console.WriteLine(bot + "Now let's put it all together:");
        //calculating the finer details
        Console.WriteLine(Character.Class.Name + " 1");
        
        if (Character.Class.SkillList.Contains("Perception"))
        {
            Character.PassivePerception = 10 + StatManager.BonusCalculator(Character.Wisdom) + Character.ProficiencyBonus;
        }
        else
        {
            Character.PassivePerception = 10 + StatManager.BonusCalculator(Character.Wisdom);
        }
        //armor class
        if (Character.Class.EquipmentListA.Contains("Leather armor"))
        {
            Character.ArmorClass = 11 + StatManager.BonusCalculator(Character.Dexterity);
        }
        else if (Character.Class.EquipmentListA.Contains("chain mail"))
        {
            Character.ArmorClass = 16;
        }
        else
        {
            Character.ArmorClass = 10 + StatManager.BonusCalculator(Character.Dexterity);
        }

        

        //Health
        Character.MaxHp = Character.Class.HitDie + StatManager.BonusCalculator(Character.Consitution);

        

        Spacer(20);
    }

    Console.Write(bot + "Now, what is your character's name? |");

    Character.Name = Console.ReadLine(); Console.WriteLine();
    if (Character.Name == "")
    {
        Character.Name = "Darryl";
    }

    Console.Write(bot + "Now, what is your character's Alignment? |");

    Character.Alignment = Console.ReadLine(); Console.WriteLine();
    if (Character.Alignment == "")
    {
        Character.Alignment = "True Neutral";
    }

    Console.WriteLine(bot + "Now, Describe your character's personality in a couple adjectives.");

    Character.Personality = Console.ReadLine(); Console.WriteLine();
    if (Character.Personality == "")
    {
        Character.Personality = "Brash, Dim, Well-Meaning";
    }



    Console.WriteLine(bot + "Now, what does your character value / idealize? I'd recommend keeping it brief.");

    Character.Ideals = Console.ReadLine(); Console.WriteLine();
    if (Character.Ideals == "")
    {
        Character.Ideals = "Getting a good deal";
    }

    Console.WriteLine(bot + "Now, did your character know any one in the past or had previously 'bonded' with? Type 'no-one' if not.");

    Character.Bonds = Console.ReadLine(); Console.WriteLine();
    if (Character.Bonds == "")
    {
        Character.Bonds = "Uncle Jerry";
    }

    Console.WriteLine(bot + "Now, what does your character fall for, or consider a 'flaw' ?.");

    Character.Flaws = Console.ReadLine(); Console.WriteLine();
    if (Character.Flaws == "")
    {
        Character.Flaws = "Is too trusting, despises aliens";
    }

    //the actual character display!
    Console.Clear();
    Console.WriteLine(bot + "This is as follows:");
    Console.WriteLine("=========================");

    Console.Write(Character.Name);

    if (!isNPC)
    {
        Console.WriteLine(", " + Character.Class.Name + " 1");
    }
    else
    {
        Console.WriteLine(", " + NPC.VocationName);
    }
    Console.WriteLine("---------------");
    Console.WriteLine("Traits: " + Character.Personality);
    Console.WriteLine("Ideals: " + Character.Ideals);
    Console.WriteLine("Bonds : " + Character.Bonds);
    Console.WriteLine("Flaws : " + Character.Flaws);

    if (isNPC)
    {
        Console.WriteLine("Their main goal is: " + NPC.RunningThread);
    }


    Console.WriteLine("===============");
    Console.WriteLine("Max Hitpoints: " + Character.MaxHp);
    Console.WriteLine("Armor Class: " + Character.ArmorClass);
    Console.WriteLine("Speed: " + Character.Speed);



    if (!isNPC)
    {
        PlayerManager.ClassDisplay(Character);
    }
    
}





