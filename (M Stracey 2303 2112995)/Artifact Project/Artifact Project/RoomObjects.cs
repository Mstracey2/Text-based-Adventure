using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Artifact_Project
{
    #region xml include derived room objects
    // for derived room objects to be saved, this needs to be here.
    [XmlInclude(typeof(TableObject))]
    [XmlInclude(typeof(EngineerObject))]
    [XmlInclude(typeof(CentralComputerObject))]
    [XmlInclude(typeof(ShelfObject))]
    [XmlInclude(typeof(NorthGenObject))]
    [XmlInclude(typeof(ButtonObject))]
    [XmlInclude(typeof(SawObject))]
    [XmlInclude(typeof(OilCanObject))]
    [XmlInclude(typeof(ValveObject))]
    [XmlInclude(typeof(BroomObject))]
    [XmlInclude(typeof(GenBeltObject))]
    [XmlInclude(typeof(LadderObject))]
    [XmlInclude(typeof(StorageKeysObject))]
    #endregion

    #region default room object class
    public class RoomObjects
    {

        public string name;
        public string description;
        public bool is_pickable;
        public bool is_Hidden;


        public RoomObjects()
        {

        }

        public RoomObjects(string name, string description, bool is_Pickable, bool is_Hidden)
        {
            this.name = name;
            this.description = description;
            this.is_pickable = is_Pickable;
            this.is_Hidden = is_Hidden;
        }

        public virtual void UseObject(RoomObjects useWithObj, GameMap creater)// this is the normal outcome for use with command if none of the derived classes are present.
        {
            Console.WriteLine("Nothing happens when I tried using " + this.name + " with " + useWithObj.name);
        }

        public virtual void ExamineCheck(RoomObjects roomObject, GameMap creater)// this is the normal outcome for examine command if none of the derived classes are present
        {
            Console.WriteLine(roomObject.description);
        }
    }
    #endregion

    #region examine derived classes
    // these are some processes that occur if the player examines certain objects. If the player examines one of these derived objects, then it reveals a new object. comments are in the EngineerObject class, the other classes follow the same or similar processes.
    public class EngineerObject : RoomObjects
    {
        public EngineerObject()// default constructer
        {

        }
        public EngineerObject(string name, string description, bool is_pickable, bool is_Hidden) : base(name, description, is_pickable, is_Hidden)// overide constructer
        {

        }

        public override void ExamineCheck(RoomObjects roomObject, GameMap creator)// function to check whether to reveal a new object from the players input.
        {
            bool check = creator.CheckSaw();// function that checks to see if the saw is hidden, this is to stop the game repeating itself if the player types the same thing.

            if (roomObject.name == "Engineers_corpse" && check == true)
            {
                Console.WriteLine(roomObject.description + "It would seem that the engineer has a saw in his hand.");
                creator.RevealSaw();// reveals the saw, making it pickable and not hidden
            }
            else
            {
                Console.WriteLine(roomObject.description);
            }
        }
    }

    public class TableObject : RoomObjects
    {
        public TableObject()
        {

        }

        public TableObject(string name, string description, bool is_pickable, bool is_Hidden) : base(name, description, is_pickable, is_Hidden)
        {

        }

        public override void ExamineCheck(RoomObjects roomObject, GameMap creater)
        {
            bool check = creater.CheckOilCan();

            if (roomObject.name == "Table" && check == true)
            {
                Console.WriteLine(description + ", you open the drawer to find a rusty looking oil can.");
                creater.RevealOilCan();
            }
            else
            {
                Console.WriteLine(roomObject.description);
            }
        }


    }

    public class ShelfObject : RoomObjects
    {
        public ShelfObject()
        {

        }

        public ShelfObject(string name, string description, bool is_pickable, bool is_Hidden) : base(name, description, is_pickable, is_Hidden)
        {

        }

        public override void ExamineCheck(RoomObjects roomObject, GameMap creater)
        {
            bool check = creater.GenBeltHidCheck();
            if (roomObject.name == "Shelves" && check == true)
            {
                Console.WriteLine(description + ". As you rummaged mountains of spare parts, you accedently knocked off a box containing a singular generator belt.\nIf you knew that before then you would have been more careful, as the belt slid under the shelf as the box smashed the ground. ");
                creater.RevealGenBelt();
            }
            else
            {
                Console.WriteLine(roomObject.description);
            }
        }


    }

    public class NorthGenObject : RoomObjects
    {
        public NorthGenObject()
        {

        }

        public NorthGenObject(string name, string description, bool is_pickable, bool is_Hidden) : base(name, description, is_pickable, is_Hidden)
        {

        }

        public override void ExamineCheck(RoomObjects roomObject, GameMap creater)
        {
            bool check = creater.nGenCheck();
            if (roomObject.name == "North_generator" && check == true)
            {
                Console.WriteLine(description + " would you like you switch it on? (respond with 'y' for yes or 'n' for no)");
                bool conf = true;
                string userInput = "";
                while (conf == true)
                {
                    userInput = Console.ReadLine();
                    if (userInput == "y")
                    {
                        Console.WriteLine("you pull the rope start on the generator and the machine roars to life");
                        conf = false;
                        creater.NorthGenPowerd();

                    }
                    else if (userInput == "n")
                    {
                        conf = false;
                    }
                    else
                    {
                        Console.WriteLine("Wrong input.");
                    }
                }

            }
            else
            {
                Console.WriteLine(roomObject.description);
            }
        }


    }

    public class CentralComputerObject : RoomObjects
    {
        public CentralComputerObject()
        {

        }

        public CentralComputerObject(string name, string description, bool is_pickable, bool is_Hidden) : base(name, description, is_pickable, is_Hidden)
        {

        }

        public override void ExamineCheck(RoomObjects roomObject, GameMap creater)
        {
            bool check = creater.CheckEscapePod();
            if (roomObject.description == "the central computer is finally back online." && check == false)
            {
                Console.WriteLine(description + ", you desperately look throught the computer to find anything that could help you get out of this place.");
                Console.WriteLine("Just when you were about to give up, you found it, open escape pod. Do you wish to open the escape pod? y/n?");
                bool conf = true;
                while (conf == true)
                {
                    string playerInput = Console.ReadLine();
                    
                    if (playerInput == "y")
                    {
                        creater.OpenEscape();
                        conf = false;
                    }
                    else if (playerInput == "n")
                    {
                        conf = false;
                    }
                    else
                    {
                        Console.WriteLine("wrong input");
                    }
                }


            }
            else
            {
                Console.WriteLine(roomObject.description);
            }
        }


    }

    public class ButtonObject : RoomObjects
    {
        public ButtonObject()
        {

        }
        public ButtonObject(string name, string description, bool is_pickable, bool is_Hidden) : base(name, description, is_pickable, is_Hidden)
        {

        }

        public override void ExamineCheck(RoomObjects roomObject, GameMap creater)
        {
            if (roomObject.name == "Launch_pod_button")
            {
                Console.WriteLine(description + " do you wish to escape? y/n?");
                bool conf = true;
                string playerInput;
                while (conf == true)
                {
                    playerInput = Console.ReadLine();
                    if (playerInput == "y")
                    {
                        creater.EndGame();
                        conf = false;
                    }
                    else if (playerInput == "n")
                    {
                        conf = false;
                    }
                    else
                    {
                        Console.WriteLine("Wrong input.");
                    }
                }

            }
        }


    }
    #endregion

    #region use with classes
    // these are processes that occur when the player uses an inventory object with a static one. If the derived class object is used with the correct static object, then something changes for the player to progress. comments are in SawObject class, other derived classes follow the same or similar process.
    public class SawObject : RoomObjects
    {
        public SawObject()// default constructer
        {

        }

        public SawObject(string name, string description, bool is_pickable, bool is_Hidden) : base(name, description, is_pickable, is_Hidden)// overide constructer
        {

        }

        public override void UseObject(RoomObjects useWithObj, GameMap creater)
        {
            bool check = creater.CheckSouthGenDoor();// a check to see if this process has already occured, stops the game from repeating itself.
            if (useWithObj.name == "South_generator_door" && check == false)// checks to see if the players input for the static object matched South_generator_door.
            {
                Console.WriteLine("you start to saw on the metal pipe wedged on in the door handle, with one last scrape the metal pipe breaks in two and makes a loud clang on the hard floor. ");
                Console.WriteLine("The Door swings open violently to reveal a new hallway");
                creater.RevealSouthBackup();// reveals a new pathway
            }
            else
            {
                Console.WriteLine("nothing happens.");
            }
        }


    }

    public class OilCanObject : RoomObjects
    {
        public OilCanObject()
        {

        }

        public OilCanObject(string name, string description, bool is_pickable, bool is_Hidden) : base(name, description, is_pickable, is_Hidden)
        {

        }

        public override void UseObject(RoomObjects useWithObj, GameMap creater)
        {
            bool check = creater.CheckValvePickable();
            if (useWithObj.name == "Valve" && check == true)
            {
                Console.WriteLine("with the remaining oil in the can, you coat the valve in oil.");
                Console.WriteLine("The valve slips off the pipe and onto the floor");
                creater.ValvePickable();
            }
            else
            {
                Console.WriteLine("nothing happens.");
            }
        }


    }

    public class ValveObject : RoomObjects
    {
        public ValveObject()
        {

        }

        public ValveObject(string name, string description, bool is_pickable, bool is_Hidden) : base(name, description, is_pickable, is_Hidden)
        {

        }

        public override void UseObject(RoomObjects useWithObj, GameMap creater)
        {
            bool check = creater.CheckCafeteria();
            if (useWithObj.name == "Cafeteria_pressure_pipe" && check == true)
            {
                Console.WriteLine("As you slide the valve onto the notch of the pipe, you hear a faint click.");
                Console.WriteLine("You turn the valve until you hear an intercom echo through the base\n\nIntercom: 'cafeteria presure stabilizing, emergancy doors are now open.' ");
                creater.CafeteriaOpened();
            }
            else
            {
                Console.WriteLine("nothing happens.");
            }
        }


    }

    public class BroomObject : RoomObjects
    {
        public BroomObject()
        {

        }

        public BroomObject(string name, string description, bool is_pickable, bool is_Hidden) : base(name, description, is_pickable, is_Hidden)
        {

        }

        public override void UseObject(RoomObjects useWithObj, GameMap creater)
        {
            bool check = creater.CheckBeltPickable();
            if (useWithObj.name == "Generator_belt" && useWithObj.is_Hidden == false && check == true)
            {
                Console.WriteLine("You get on your hands and knees to try and get the generator belt out from underneath the shelf.");
                Console.WriteLine("with all your concentration, you managed to hook the gen belt around the broom.");
                Console.WriteLine("the gen belt is now in a reachable place for you to pickup.");
                creater.GenBeltPickable();
            }
            else
            {
                Console.WriteLine("nothing happens.");
            }
        }


    }

    public class GenBeltObject : RoomObjects
    {
        public GenBeltObject()
        {

        }

        public GenBeltObject(string name, string description, bool is_pickable, bool is_Hidden) : base(name, description, is_pickable, is_Hidden)
        {

        }

        public override void UseObject(RoomObjects useWithObj, GameMap creater)
        {
            bool check = creater.SouthGenCheck();
            if (useWithObj.name == "South_generator" && check == true)
            {
                Console.WriteLine("You replaced the cut generator belt with the spare one, the generator roars to life.");
                Console.WriteLine("Intercom: 'south generator online, central control room lights restored.'");
                creater.SouthGenPowered();
            }
            else
            {
                Console.WriteLine("Nothing happens.");
            }
        }


    }

    public class LadderObject : RoomObjects
    {
        public LadderObject()
        {

        }

        public LadderObject(string name, string description, bool is_pickable, bool is_Hidden) : base(name, description, is_pickable, is_Hidden)
        {

        }

        public override void UseObject(RoomObjects useWithObj, GameMap creater)
        {
            bool check = creater.CheckKey();
            if (useWithObj.name == "Janitor_corpse" && check == true)
            {
                Console.WriteLine("you unfolded the ladder and placed it up right under the janitor.");
                Console.WriteLine("while climbing the ladder you bumped into the janitor's shin. As you watched the lifeless body twist and twirl in the cobweb of wires, you saw key with a keytag drop from the trouser right side pocket. ");
                creater.KeyPickable();
            }
            else
            {
                Console.WriteLine("Nothing happens.");
            }
        }


    }

    public class StorageKeysObject : RoomObjects
    {
        public StorageKeysObject()
        {

        }

        public StorageKeysObject(string name, string description, bool is_pickable, bool is_Hidden) : base(name, description, is_pickable, is_Hidden)
        {

        }

        public override void UseObject(RoomObjects useWithObj, GameMap creater)
        {
            bool check = creater.CheckStorageOpen();
            if (useWithObj.name == "Storage_door" && check == true)
            {
               
                Console.WriteLine("You insert the keys into the door.");
                Console.WriteLine("The Door swings open gently to reveal storage.");
                creater.OpenStorage();
            }
            else
            {
                Console.WriteLine("Nothing happens.");
            }

        }


    }
    #endregion
}
