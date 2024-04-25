using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DnDeluxe.PlayerManager;

namespace ConsoleApp7
{
    public class CharacterBuild //the defaults will be 
    {
        public string Name;
        public string Alignment;
        public string Personality;
        public string Ideals;
        public string Bonds;
        public string Flaws;
        public int ProficiencyBonus = 2;

        public int Strength = 10;               //strength to char are numbered 1-6 
        public int Dexterity = 10;
        public int Consitution = 10;
        public int Wisdom = 10;
        public int Intelligence = 10;
        public int Charisma = 10;


        public int MaxHp = 4;
        public int PassivePerception = 10;
        public int ArmorClass = 10;
        public int Speed = 30;

        public int StartingLanguage = 1;

        public Class Class;
        public CharacterBuild() //defaults to commoner statblock
        {

        }
    }

    public class NPC //much simpler, literally all strings
    {
        public string VocationName;
        public string RunningThread;
    }

    public class StatSpread
    {
        public int Strength = 10;               //strength to char are numbered 1-6 
        public int Dexterity = 10;
        public int Consitution = 10;
        public int Wisdom = 10;
        public int Intelligence = 10;
        public int Charisma = 10;
    }




    public class StatManager
    {
        public int[] StatRoller(CharacterBuild character)
        {
            int[] statarray = {0, 0, 0, 0, 0, 0};
            int[] temparray = { 0, 0, 0, 0 };
            int smallestroll = 0;
            Random random = new();

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    temparray[j] = random.Next(1, 7);
                }
                smallestroll = temparray.Min();

                statarray[i] = temparray.Sum() - smallestroll;
            }
            return statarray;
        }


        public int StatSet(CharacterBuild character, int statchanged, int amount)
        {
            switch(statchanged)
            {
                case 1:
                    character.Strength = amount; break;
                case 2:
                    character.Dexterity = amount; break;
                case 3:
                    character.Consitution = amount; break;
                case 4:
                    character.Wisdom = amount; break;
                case 5:
                    character.Intelligence = amount; break;
                case 6:
                    character.Charisma = amount; break;
            }
            return amount;
        }

        public void StatDisplay(int[] Array, string[] displaybank) //for the creation process
        {
            string[] statlabels = { "Strength:      ", "Dexterity:     ", "Constitution:  ",
                "Wisdom:        ", "Intelligence:  ", "Charisma:      "};

            Console.Write("This is what you had to work with: |");

            for (int j = 0; j < Array.Length; j++)
            {
                if (j != 5)
                {
                    Console.Write(Array[j] + ", ");
                }
                else
                {
                    Console.Write(Array[j]);
                }

                
            }
            Console.WriteLine();
            Console.WriteLine("===========");

            for (int d = 0; d < statlabels.Length; d++)  //allows the user to see their stats in real time
            {
                Console.Write(statlabels[d] + " " + displaybank[d] + "          ");

                if (displaybank[d] == "")
                {
                    Console.WriteLine((d + 1));
                }
                else
                {
                    Console.WriteLine();
                }
            }
            Console.WriteLine("==================");

        }

        public void StatDisplay(CharacterBuild character) //for already completed characters
        {
            string[] statlabels = { "Strength:      ", "Dexterity:     ", "Constitution:  ",
                "Wisdom:        ", "Intelligence:  ", "Charisma:      "};
            Console.Clear();
            Console.WriteLine("This is what you had to work with: |");
            Console.WriteLine("===========");

            Console.WriteLine(statlabels[0] + character.Strength);
            Console.WriteLine(statlabels[1] + character.Dexterity);
            Console.WriteLine(statlabels[2] + character.Consitution);
            Console.WriteLine(statlabels[3] + character.Wisdom);
            Console.WriteLine(statlabels[4] + character.Intelligence);
            Console.WriteLine(statlabels[5] + character.Charisma);

            Console.WriteLine("===========");
        }

        public int BonusCalculator(int stattocalc)
        {
            double bonus = stattocalc - 10;

            bonus = bonus / 2;

            Math.Floor(bonus);

            return (int)bonus;
        }

        //public void AddRacialBonus(CharacterBuild Character, int plus1bonus, int plus2bonus)
        //{

        //    StatDisplay(Character);

        //    int answer = 0;



        //    while (plus1bonus > 0)
        //    {

        //        StatSet(Character, answer, Character.Charisma + 1); //example of +1 bonus
        //    }

        //    while (plus2bonus > 0)
        //    {
        //        StatSet(Character, answer, Character.Charisma + 2); //example of +1 bonus
        //    }


        //}


        public StatManager() 
        { 
        
        }
    }
}

