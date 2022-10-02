using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Artifact_Project
{
    class Program
    {



        static void Main(string[] args)
        {




            #region creating new classes and the world.
            Random rndCombatChance = new Random(); // a random class, it is used to decide whether the player goes into combat when they move rooms

            GameMap creator = new GameMap(); // this class will create all the rooms and room objects, along with methods that changes the objects states.

            IntroTutorial intro = new IntroTutorial(); // this class contains a tutorial if they select 'new game'.

            Room savedRoom = new Room(); // this is a room class that will store the current room of the player when they save the game.

            Combat combat = new Combat(); // this class contains the combat process.

            creator.CreateObjects(); // creates the room objects

            creator.CreateMap(); // creates the rooms and the directions they contain to go to new rooms

            Player myRoom = new Player(creator.controlRoom);// this is the player class, holds information for the player such as the inventory and their current room. their current room is set as control room at the start.
            #endregion

            bool gameActivate = false;// boolean to activate the game

            bool avoidComFirstTime = true;// boolean to avoid getting into combat on first time load up.
            bool splashScreen = true;
            while (splashScreen == true)
            {
                #region Menu
                Console.WriteLine("***************************************");
                Console.WriteLine("*             Too Deep !!!            *");
                Console.WriteLine("***************************************");
                Console.WriteLine();
                Console.WriteLine("1.new game\n\n2.load game\n\n3.controls\n\n4.quit");

                bool menu = true;
                while (menu == true)
                {
                    Console.WriteLine("\nplease type the number of which the selection appears.");
                    string input = Console.ReadLine();
                    if (input == "1")
                    {
                        intro.Intro();
                        gameActivate = true; // game activater
                        menu = false;// quits menu loop
                    }
                    else if (input == "2")
                    {
                        gameActivate = true;
                        menu = false;
                        LoadGame(creator); //function for loading a saved game
                    }
                    else if (input == "3")
                    {
                        myRoom.DisplayHelp();// function to display the help section
                    }
                    else if (input == "4") // will close the game by exiting all while loops
                    {
                        quit();
                        gameActivate = false;
                        menu = false;
                        splashScreen = false;
                    }
                }

                #endregion

                #region Active game and UI
                while (gameActivate == true)
                {


                    bool UI = true;

                    int chanceDice;// this dice decides whether the player enters combat when moving rooms by random chance. 

                    myRoom.LookAroundRoom();// this function describes the room the player is in.

                    while (UI == true)
                    {
                        if (creator.end == true) // this is for when the player reaches the end of the game, returning them to the main menu.
                        {
                            UI = false;
                            gameActivate = false;
                            menu = true;
                        }
                        else
                        {
                            avoidComFirstTime = false;// sets to false so they may now enter combat.

                            if (creator.controlRoomActive == true) // this is for when the player turns on both generators in the game
                            {
                                Console.WriteLine("Intercom: backup generators North and South online, Centre Control Room fully restored and back online");
                                creator.centralComputer.description = "the central computer is finally back online.";
                                creator.controlRoomActive = false;
                            }



                            Console.WriteLine("\nwhat do you wish to do? (enter 'help' for help)");
                            string userInput = Console.ReadLine();
                            Console.Clear();
                            string[] words;
                            string objectName;
                            string secondObjectName;
                            int wordsLength;

                            words = userInput.Split(' '); // splits the words from the users input into an array of strings
                            wordsLength = words.Length;

                            // these two if statements make it so the first and third word that the users inputs makes the first letters capitals.
                            if (wordsLength > 1)
                            {
                                objectName = words[1];
                                words[1] = (char.ToUpper(objectName[0]) + objectName.Substring(1));
                            }

                            if (wordsLength > 3)
                            {
                                secondObjectName = words[3];
                                words[3] = (char.ToUpper(secondObjectName[0]) + secondObjectName.Substring(1));
                            }




                            string commandword = words[0]; // this is the command word, it will decide what the user is trying to do

                            if (commandword == "help")
                            {
                                myRoom.DisplayHelp();// displays help
                            }
                            else if (commandword == "view")
                            {
                                myRoom.LookAroundRoom();// describes the current room
                            }
                            else if (commandword == "move")
                            {
                                myRoom.Move();// goes to a function which asks the player which direction they wish to move
                                chanceDice = rndCombatChance.Next(0, 10);// rolls the chance dice of entering combat
                                if (chanceDice == 4 && avoidComFirstTime == false)
                                {
                                    combat.CombatStart();// the combat process
                                    Console.Clear();
                                    if (combat.playerDefeat == true)// this is a boolean variable which decides whether the user won the battle 
                                    {
                                        Console.WriteLine("GAME OVER");
                                        Console.WriteLine("1.load");
                                        Console.WriteLine("2.quit");
                                        string overInput = Console.ReadLine();
                                        if (overInput == "1")
                                        {
                                            LoadGame(creator); // loads the latest saved game
                                        }
                                        else if (overInput == "2")
                                        {
                                            UI = false;
                                            gameActivate = false;
                                            menu = true; // sends the user back to the main menu
                                        }
                                        else
                                        {
                                            Console.WriteLine("wrong input, please respond with the number of your choice.");
                                        }
                                    }
                                }
                                myRoom.LookAroundRoom();
                            }
                            else if (commandword == "pickup")
                            {
                                if (wordsLength > 1)
                                {
                                    myRoom.Pickup(words[1]); // function for picking up objects
                                }

                            }
                            else if (commandword == "inv")
                            {
                                myRoom.LookInInventory(); // function to look in the players inventory
                            }
                            else if (commandword == "examine")
                            {
                                if (wordsLength > 1)
                                {
                                    myRoom.Examine(words[1], creator);// function to examine the object, this is done by matching the name of the object with the players second word they inputted


                                }

                            }
                            else if (commandword == "use")// function for using inventory items with static ones, checks to see if the command word is 'use', and the third word is with. It then matches the second and fourth word to the objects
                            {

                                if (wordsLength == 4 && words[2] == "with")
                                {
                                    myRoom.UseObjects(words[1], words[3], creator);

                                }
                            }
                            else if (commandword == "save")
                            {
                                SaveGame();// save game function
                            }
                            else if (commandword == "load")
                            {
                                LoadGame(creator); // load game funtion
                                myRoom.LookInInventory();// once loaded shows the players inventory and current room description
                                myRoom.LookAroundRoom();
                            }
                            else
                            {
                                Console.WriteLine("Wrong input, view the help for controls.");
                                System.Threading.Thread.Sleep(2000);
                            }
                        }


                    }
                }
                #endregion

                #region save game
                void SaveGame()
                {
                    creator.controlRoom.Serialize(); // serializes and saves all the states of the rooms and the room objects inside them
                    creator.forkHall.Serialize();
                    creator.escapePod.Serialize();
                    creator.eastWindow.Serialize();
                    creator.waterPressureRoom.Serialize();
                    creator.cafeteria.Serialize();
                    creator.southBackupRoom.Serialize();
                    creator.westWindow.Serialize();
                    creator.backUpGenControls.Serialize();
                    creator.observationDeck.Serialize();
                    creator.janitorsCloset.Serialize();
                    creator.storage.Serialize();
                    creator.northBackupRoom.Serialize();
                    myRoom.playerInv.SerializeInv(); // saves the players inventory

                    savedRoom = myRoom.currentRoom; // creates an extra room for saving the players current room
                    savedRoom.SerializeCurrentRoom();// serializes and saves that saved room

                    Console.WriteLine("Game Saved !");
                }
                #endregion

                #region load game
                void LoadGame(GameMap gameMap)
                {

                    myRoom.currentRoom = null; // sets the current room to null. this stops the room the player is currently in being overidden by the saved room.
                    creator.controlRoom.Deserialize(gameMap);// process of deserializing all the rooms and their objects and loading the previous states
                    creator.forkHall.Deserialize(gameMap);
                    creator.escapePod.Deserialize(gameMap);
                    creator.eastWindow.Deserialize(gameMap);
                    creator.waterPressureRoom.Deserialize(gameMap);
                    creator.cafeteria.Deserialize(gameMap);
                    creator.southBackupRoom.Deserialize(gameMap);
                    creator.westWindow.Deserialize(gameMap);
                    creator.backUpGenControls.Deserialize(gameMap);
                    creator.observationDeck.Deserialize(gameMap);
                    creator.janitorsCloset.Deserialize(gameMap);
                    creator.storage.Deserialize(gameMap);
                    creator.northBackupRoom.Deserialize(gameMap);
                    myRoom.playerInv.DeSerializeInv();// returns the users saved objects
                    savedRoom.DeserializeCurrentRoom(gameMap); // deserializes the saved room.

                    GoToCurrentRoom(); // this function is a process where the code looks for the saved rooms name, and then puts the players current room in that room.
                    Console.WriteLine("Load saved game!");
                }
                #endregion

                void quit()
                {
                    Console.WriteLine("thanks for playing! Goodbye!");
                    System.Threading.Thread.Sleep(2000);
                }

                #region find current room
                void GoToCurrentRoom() // checks the room name until it matches one of the names, will then assign the current room to that room.
                {
                    if (savedRoom.roomName == "Fork Hall")
                    {
                        myRoom.currentRoom = creator.forkHall;
                    }
                    else if (savedRoom.roomName == "Control Room")
                    {
                        myRoom.currentRoom = creator.controlRoom;
                    }
                    else if (savedRoom.roomName == "Cafeteria")
                    {
                        myRoom.currentRoom = creator.cafeteria;
                    }
                    else if (savedRoom.roomName == "Water pressure room")
                    {
                        myRoom.currentRoom = creator.waterPressureRoom;
                    }
                    else if (savedRoom.roomName == "Back up generator control room")
                    {
                        myRoom.currentRoom = creator.backUpGenControls;
                    }
                    else if (savedRoom.roomName == "Escape pod")
                    {
                        myRoom.currentRoom = creator.escapePod;
                    }
                    else if (savedRoom.roomName == "East window")
                    {
                        myRoom.currentRoom = creator.eastWindow;
                    }
                    else if (savedRoom.roomName == "West window")
                    {
                        myRoom.currentRoom = creator.westWindow;
                    }
                    else if (savedRoom.roomName == "Storage")
                    {
                        myRoom.currentRoom = creator.storage;
                    }
                    else if (savedRoom.roomName == "Janitors cuboard")
                    {
                        myRoom.currentRoom = creator.janitorsCloset;
                    }
                    else if (savedRoom.roomName == "North backup room")
                    {
                        myRoom.currentRoom = creator.northBackupRoom;
                    }
                    else if (savedRoom.roomName == "Observation deck")
                    {
                        myRoom.currentRoom = creator.observationDeck;
                    }
                    else if (savedRoom.roomName == "South Backup room")
                    {
                        myRoom.currentRoom = creator.southBackupRoom;
                    }
                }
                #endregion
            }
        }
    }
}