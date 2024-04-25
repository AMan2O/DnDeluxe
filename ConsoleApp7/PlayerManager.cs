using ConsoleApp7;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet;
using YamlDotNet.Serialization.BufferedDeserialization;
using YamlDotNet.Serialization.NamingConventions;

namespace DnDeluxe
{
    public class PlayerManager
    {

        


        public class ClassConfig
        {
            public List<Class> Classes;

        }
        public class Class
        {
            public string Name;
            public string Description;
            public int HitDie;
            public List<string> Tools;
            public List<string> Weapons;
            public List<string> Armor;
            public List<string> SavingThrows;
            public int SkillAmount;
            public List<string> SkillList;
            public List<string> EquipmentListA;
            public List<string> EquipmentListB;
            public string StartingTechniques;
        }
        void Space(int length)
        {
            for (int i = 0; i < length; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
        }
        void Spacer(int length)
        {
            for (int i = 0; i < length; i++)
            {
                Console.Write("=");
            }
            Console.WriteLine();
        }

        public void ClassList(ClassConfig config)
        {
            for (int i = 0; i < config.Classes.Count; i++)
            {
                Console.WriteLine(config.Classes[i].Name);
            }
        }

        public void ClassDisplay(ClassConfig config, int storenumber)
        {
            

            Console.Clear();

            Spacer(20);

            Console.WriteLine(config.Classes[storenumber].Name);
            Console.WriteLine(config.Classes[storenumber].Description);

            Spacer(20);


            Console.WriteLine("Hit Die:");
            Console.WriteLine("- 1d" + config.Classes[storenumber].HitDie);

            Space(10);

            Console.WriteLine("Starting HP: ");
            Console.WriteLine(config.Classes[storenumber].HitDie + " + Con Bonus");

            Space(10);

            Console.WriteLine("Armor Proficiencies:");
            for (int i = 0; i < config.Classes[storenumber].Armor.Count; i++)  
            {
                Console.Write("- ");
                Console.WriteLine(config.Classes[storenumber].Armor[i]);
            }

            Space(10);

            Console.WriteLine("Weapon Proficiencies:");
            for (int i = 0; i < config.Classes[storenumber].Weapons.Count; i++)  
            {
                Console.Write("- ");
                Console.WriteLine(config.Classes[storenumber].Weapons[i]);
            }

            Space(10);

            Console.WriteLine("Tool Proficiencies:");
            for (int i = 0; i < config.Classes[storenumber].Tools.Count; i++)  
            {
                Console.Write("- ");
                Console.WriteLine(config.Classes[storenumber].Tools[i]);
            }

            Space(10);

            Console.WriteLine("Saving Throw Proficiencies:");
            for (int i = 0; i < config.Classes[storenumber].SavingThrows.Count; i++)  
            {
                Console.Write("- ");
                Console.WriteLine(config.Classes[storenumber].SavingThrows[i]);
            }

            Space(10);

            Console.WriteLine("Skill Proficiencies: ");
            Console.WriteLine(config.Classes[storenumber].SkillAmount + " from this list:");
            for (int i = 0; i < config.Classes[storenumber].SkillList.Count; i++)
            {
                Console.Write("- ");
                Console.WriteLine(config.Classes[storenumber].SkillList[i]);
            }

            Space(10);

            Console.WriteLine("Starting Equipment:");
            for (int i = 0; i < config.Classes[storenumber].EquipmentListA.Count; i++)  
            {
                Console.Write("- ");

                if (config.Classes[storenumber].EquipmentListA[i] == config.Classes[storenumber].EquipmentListB[i])
                {
                    Console.WriteLine(config.Classes[storenumber].EquipmentListA[i]);
                }
                else
                    Console.WriteLine(config.Classes[storenumber].EquipmentListA[i] + " or " + config.Classes[storenumber].EquipmentListB[i]);
            }

            Space(10);
        }

        public void ClassSkillDisplay(ClassConfig config, int storenumber, int choicelimit)
        {

            Console.WriteLine("Now, choose the skills your character will be good at.");
            Console.WriteLine("You get to choose " + choicelimit + " of them.");

            Spacer(20);

            for (int i = 0; i < config.Classes[storenumber].SkillList.Count; i++)
            {
                Console.Write("- ");
                Console.WriteLine(config.Classes[storenumber].SkillList[i] + "  " + (i + 1));
            }
            Spacer(20);
        }

        public List<string> ClassEquipmentDisplay(ClassConfig config, int storenumber)
        {
            List<string> EquipmentChoices = new List<string>();
            int option = 0;
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


            for (int i = 0; i < config.Classes[storenumber].EquipmentListA.Count; i++)
            {
                Console.Write("- ");

                if (config.Classes[storenumber].EquipmentListA[i] == config.Classes[storenumber].EquipmentListB[i])
                {
                    //this would be a non-negotiable option
                    Console.WriteLine("You get a " + config.Classes[storenumber].EquipmentListA[i] +".");
                    EquipmentChoices.Add(config.Classes[storenumber].EquipmentListA[i]);
                }
                else
                {
                    bool door = false;

                    while (!door)
                    {
                        option = GetInt("Type '1' for " + config.Classes[storenumber].EquipmentListA[i] + ", or '2' for " + config.Classes[storenumber].EquipmentListB[i] + ". |");

                        if (option == 1)
                        {
                            EquipmentChoices.Add(config.Classes[storenumber].EquipmentListA[i]);
                            door = true;
                        }
                        else if (option == 2)
                        {
                            EquipmentChoices.Add(config.Classes[storenumber].EquipmentListB[i]);
                            door = true;
                        }
                        else
                        {

                        }
                    }



                }
            }

            return EquipmentChoices;
        }

        public void ClassDisplay(CharacterBuild Character)
        { 

            Spacer(20);


            Console.WriteLine("Hit Die:");
            Console.WriteLine("- 1d" + Character.Class.HitDie);

            Space(10);

            Console.WriteLine("Starting HP: ");
            Console.WriteLine(Character.Class.HitDie + " + Con Bonus");

            Space(10);

            Console.WriteLine("Passive Perception: ");
            Console.WriteLine(Character.PassivePerception);

            Space(10);

            Console.WriteLine("Armor Proficiencies:");
            for (int i = 0; i < Character.Class.Armor.Count; i++)
            {
                Console.Write("- ");
                Console.WriteLine(Character.Class.Armor[i]);
            }

            Space(10);

            Console.WriteLine("Weapon Proficiencies:");
            for (int i = 0; i < Character.Class.Weapons.Count; i++)
            {
                Console.Write("- ");
                Console.WriteLine(Character.Class.Weapons[i]);
            }

            Space(10);

            Console.WriteLine("Tool Proficiencies:");
            for (int i = 0; i < Character.Class.Tools.Count; i++)
            {
                Console.Write("- ");
                Console.WriteLine(Character.Class.Tools[i]);
            }

            Space(10);

            Console.WriteLine("Saving Throw Proficiencies:");
            for (int i = 0; i < Character.Class.SavingThrows.Count; i++)
            {
                Console.Write("- ");
                Console.WriteLine(Character.Class.SavingThrows[i]);
            }

            Space(10);

            Console.WriteLine("Skill Proficiencies: ");
            
            for (int i = 0; i < Character.Class.SkillList.Count; i++)
            {
                Console.Write("- ");
                Console.WriteLine(Character.Class.SkillList[i]);
            }

            Space(10);

            Console.WriteLine("Starting Equipment:");
            for (int i = 0; i < Character.Class.EquipmentListA.Count; i++)
            {
                Console.Write("- ");
                Console.WriteLine(Character.Class.EquipmentListA[i]);

            }
        Space(10);
    }









    public PlayerManager() { }
    }
}
