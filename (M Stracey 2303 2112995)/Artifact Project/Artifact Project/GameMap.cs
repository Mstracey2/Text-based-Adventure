using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Artifact_Project
{
    public class GameMap
    {
        public bool controlRoomActive = false;// boolean which tells the player they have powered the control room.
        public bool end = false; // boolean which decides that the player has ended the game

        #region initializing rooms and room objects
        public RoomObjects launchButton, escapePodDoor, storageDoor, sGen, nGen, securityGuard, cafeteriaEmergancyDoor, centralComputer, valve, cafeteriaWaterPipe, southGenDoor, janitor, storageKeys, engineer, saw, genBelt, shelves, broom, ladder, creatureTerminal, oilcan, table, picture;
        public Room forkHall, cafeteria, waterPressureRoom, backUpGenControls, controlRoom, escapePod, eastWindow, westWindow, storage, janitorsCloset, northBackupRoom, observationDeck, southBackupRoom;
        #endregion

        #region finding room objects for deserializing
        public RoomObjects findRoomObject(string roomObjectName)// while the rooms are deserializing, this function will find the current objects for them to be overidden by the saved objects.
        {
            RoomObjects retval = null;
            switch (roomObjectName)
            {
                case "Launch_pod_button":
                 retval = launchButton;
                    break;

                case "Oil_can":
                    retval = oilcan;
                    break;

                case "Valve":
                    retval = valve;
                    break;

                case "Cafeteria_pressure_pipe":
                    retval = cafeteriaWaterPipe;
                    break;

                case "South_generator_door":
                    retval = southGenDoor;
                    break;

                case "Engineers_corpse":
                    retval = engineer;
                    break;

                case "Saw":
                    retval = saw;
                    break;

                case "Generator_belt":
                    retval = genBelt;
                    break;

                case "High-handled_broom":
                    retval = broom;
                    break;

                case "Storage_keys":
                    retval = storageKeys;
                    break;

                case "Janitor_corpse":
                    retval = janitor;
                    break;

                case "Ladder":
                    retval = ladder;
                    break;

                case "Table":
                    retval = table;
                    break;

                case "Research_terminal":
                    retval = creatureTerminal;
                    break;

                case "Team_picture":
                    retval = picture;
                    break;

                case "Security_guard_corpse":
                    retval = securityGuard;
                    break;

                case "Central_computer":
                    retval = centralComputer;
                    break;

                case "Shelves":
                    retval = shelves;
                    break;

                case "Cafeteria_emergancy_door":
                    retval = cafeteriaEmergancyDoor;
                    break;

                case "North_generator":
                    retval = nGen;
                    break;

                case "South_generator":
                    retval = sGen;
                    break;

                case "Storage_door":
                    retval = storageDoor;
                    break;

                case "Escape_Pod_Door":
                    retval = escapePodDoor;
                    break;


            }
            return retval;
        }
        #endregion

        #region creating the map
        public void CreateMap()
        {
            // these are the rooms for the game, containing the name, description and the directions to other rooms
            #region Creating Rooms with directions
            forkHall = new Room("Fork Hall", "the fork hall is in bad shape, sea water was leaking from the roof of the base. whatever happened here was devastating to the integraty. The hall is filled with paintings and photos in frames hung on the wall, each one showing some kind of significance to the marine research inc. The hall is meant to look fancy with the wooden floors and hung lights, but with the water damage, it now looks the most devastating compared to all the rooms.", null, null, null, null);
            cafeteria = new Room("Cafeteria", "from what you can see, the cafeteria is in shambles. Plates and trays are all over the place, tables and chairs barricading corners. usually, the food you can get in the cafeteria is quite delicous. But now, the smell of stale seafood mixed with dead bodies is giving you a sickening taste at the back of your throat.", null, null, null, null);
            waterPressureRoom = new Room("Water pressure room", "your not the the person who mans the water pressure, but you can tell that its not in good condition. red warning lights are going off control panel but you can not figure out what they mean, it would seem the base is in a very fragile state. the bright side is, there is not much in the way of damage here, seems like you wont need to figure out any complex repairs.", null, null, null, null);
            backUpGenControls = new Room("Back up generator control room", "this room is possibly the most unkept of all the rooms, nobody really moniters the backup generators too much. Some people call this the secondary storage, or the bin. crates fill the corners of the room, most of them empty exept for the occasional packaging. ", null, null, null, null);
            controlRoom = new Room("Control Room", "the control room is very dark, the only light your recieving is flashing red lights on the wall and a couple of monitor screens. if there was a ways to turn the lights on, you may be able to see what your constantly bumping into while walking around. The only thing you can make out, is that you are standing in a cold puddle of sea water.", forkHall, null, waterPressureRoom, backUpGenControls);
            escapePod = new Room("Escape pod", "the escape pod, from the outside it looks more like a submarine. It's only used for the most dire situations.", null, forkHall, null, null);
            eastWindow = new Room("East window", "the east window, a big window however theres not much to see except open blue ocean. With that said though, some people have said to have seen some crazy wildlife, including sharks bigger than the window itself.", null, waterPressureRoom, null, forkHall);
            westWindow = new Room("West window", "the west window seems like its in poor condition, cracked and smashed. Any force and it would surely break, flooding the entire base. So it would not be the best idea to lean anything on the window, just saying.", backUpGenControls, null, null, null);
            storage = new Room("Storage", "the storage room is filled with shelves, this is where they keep all the spare parts", null, null, null, null);
            janitorsCloset = new Room("Janitors cuboard", "not much here, just the janitor's cleaning tools.", null, null, forkHall, null);
            northBackupRoom = new Room("North backup room", "an empty room with a generator, its always been a bit dingy in these rooms", null, storage, null, null);
            observationDeck = new Room("Observation deck", "the observation deck is a massive glass dome, deep blue ocean surrounding the place. In the centre, seems to be some kind of sptic tank with and underdeveloped creature. Your not entirely sure that what your looking at is very ethical, but you dont care at this point as its not your top priority.", null, null, backUpGenControls, null);
            southBackupRoom = new Room("South Backup room", "an empty room with a generator, its always been a bit dingy in these rooms", cafeteria, null, null, null);

            // these are more direction assigning
            forkHall.north = escapePod;
            forkHall.northVisible = false; // these booleans are to reveal hidden pathways when the player unlocks them
            westWindow.east = cafeteria;
            westWindow.eastVisible = false;
            controlRoom.south = cafeteria;
            controlRoom.southVisible = false;
            forkHall.south = controlRoom;
            forkHall.east = eastWindow;
            forkHall.west = janitorsCloset;
            cafeteria.north = controlRoom;
            cafeteria.west = westWindow;
            cafeteria.south = southBackupRoom;
            cafeteria.southVisible = false;
            waterPressureRoom.north = eastWindow;
            waterPressureRoom.west = controlRoom;
            backUpGenControls.north = storage;
            backUpGenControls.northVisible = false;
            backUpGenControls.south = westWindow;
            backUpGenControls.west = observationDeck;
            backUpGenControls.east = controlRoom;
            storage.south = backUpGenControls;
            storage.north = northBackupRoom;
            #endregion

            #region Adding objects to rooms
            // adds the objects to the rooms.
            controlRoom.AddRoomObject(securityGuard);
            controlRoom.AddRoomObject(centralComputer);
            controlRoom.AddRoomObject(cafeteriaEmergancyDoor);
            waterPressureRoom.AddRoomObject(cafeteriaWaterPipe);
            janitorsCloset.AddRoomObject(valve);
            cafeteria.AddRoomObject(southGenDoor);
            cafeteria.AddRoomObject(janitor);
            cafeteria.AddRoomObject(storageKeys);
            backUpGenControls.AddRoomObject(engineer);
            backUpGenControls.AddRoomObject(saw);
            backUpGenControls.AddRoomObject(storageDoor);
            storage.AddRoomObject(genBelt);
            storage.AddRoomObject(shelves);
            northBackupRoom.AddRoomObject(nGen);
            westWindow.AddRoomObject(ladder);
            westWindow.AddRoomObject(cafeteriaEmergancyDoor);
            observationDeck.AddRoomObject(creatureTerminal);
            forkHall.AddRoomObject(oilcan);
            forkHall.AddRoomObject(table);
            forkHall.AddRoomObject(picture);
            southBackupRoom.AddRoomObject(sGen);
            southBackupRoom.AddRoomObject(broom);
            escapePod.AddRoomObject(launchButton);
            #endregion
        }
        #endregion

        #region creating the objects
        public void CreateObjects()
        {
            #region Create Objects
            // these are the objects that the player can interact with, they contain a name, description, whether they are pickable and if they are hidden from site.
            oilcan = new OilCanObject("Oil_can", "the oil can is very rusted and clearly has not been used in ages. you can just make out that the original colour of the can was red.", false, true);
            valve = new ValveObject("Valve", "a bright red valve, seems to be wedged onto a pipe. some lubricant should loosen it.", false, false);
            cafeteriaWaterPipe = new RoomObjects("Cafeteria_pressure_pipe", "the cafeteria's water pressure pipes, a valve can be placed on it.", false, false);
            southGenDoor = new RoomObjects("South_generator_door", "the door to the south generator, the door appears to be wedged with a pipe.", false, false);
            engineer = new EngineerObject("Engineers_corpse", "the sea base's engineer, poor guy seamed like he got torn up badly.", false, false);
            saw = new SawObject("Saw", "a saw from the engineer, pointy!", false, true);
            genBelt = new GenBeltObject("Generator_belt", "an elastic generator belt, stuck far beyond your reach. Looks like you are going to need something long and thin to hook it with.", false, true);
            broom = new BroomObject("High-handled_broom", "a wooden broom, has a high handle", true, false);
            storageKeys = new StorageKeysObject("Storage_keys", "Storage keys, tag hangs from them saying 'storage'", false, true);
            janitor = new RoomObjects("Janitor_corpse", "janitor's corpes, his head has been torn off and is tangled in electrical roof wires. looks like your going to need leverage to get to him", false, false);
            ladder = new LadderObject("Ladder", "a step ladder, useful!", true, false);
            table = new TableObject("Table", "a table with some draws", false, false);
            creatureTerminal = new RoomObjects("Research_terminal", "the research terminal, all the information we have on these creatures", false, false);
            picture = new RoomObjects("Team_picture", "a picture of the sector C team, you all looked so happy then.", false, false);
            securityGuard = new RoomObjects("Security_guard_corpse", "the security guard, said to keep us safe and yet he is in his own pool of blood", false, false);
            centralComputer = new CentralComputerObject("Central_computer", "The Central computer is not in the best shape with cracks on the monitor and missing keys on the keyboard. You try switching it on but its no use, the power is out. The backup generators are your only option to access the computer.", false, false);
            shelves = new ShelfObject("Shelves", "storage shelves, holds all sorts of spare parts", false, false);
            cafeteriaEmergancyDoor = new RoomObjects("Cafeteria_emergancy_door", "the cafeteria seems to be blocked off by these emergancy doors, this only happens when a room gets unstable from water pressure. maybe theres a way of stabalizing it.", false, false);
            nGen = new NorthGenObject("North_generator", "the North generator, its in good shape.", false, false);
            sGen = new RoomObjects("South_generator", "seems like something has tampered with the generator, the generator belt that is required for it to run has been cut. looks like your going to need a new belt in order for the generator to work", false, false);
            storageDoor = new RoomObjects("Storage_door", "the door to storage, seems like its locked. there must be a key around somewhere.", false, false);
            escapePodDoor = new RoomObjects("Escape_Pod_Door", "the escape pod, the only way to get out of here. You need to find a way to open it.", false, false);
            launchButton = new ButtonObject("Launch_pod_button", "the launch button, pressing this will launch the pod to the surface.", false, false);

            #endregion
        }
        #endregion

        // these are multiple functions designed to change room and object variables depending on the actions of the player. there are also check functions to check current object states and returns them to room objects class.
        #region changes from player actions and object state checks
        public void RevealSouthBackup()
        {
            cafeteria.southVisible = true;
            southGenDoor.description = "the door is open revealing a new hallway";
        }

        public bool CheckSouthGenDoor()
        {
            if (cafeteria.southVisible == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void RevealSaw()
        {
            saw.is_Hidden = false;
            saw.is_pickable = true;
        }

        public bool CheckSaw()
        {
            if (saw.is_Hidden == true)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public void RevealOilCan()
        {
            oilcan.is_Hidden = false;
            oilcan.is_pickable = true;
        }

        public bool CheckOilCan()
        {
            if (oilcan.is_Hidden == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ValvePickable()
        {
            valve.is_pickable = true;
            valve.description = "a bright red valve";
        }

        public bool CheckValvePickable()
        {
            if (valve.is_pickable == false)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void CafeteriaOpened()
        {
            cafeteriaEmergancyDoor.description = "the door has been lifted and is no longer in the way";
            controlRoom.southVisible = true;
            westWindow.eastVisible = true;
            
        }

        public bool CheckCafeteria()
        {
            if (controlRoom.southVisible == true)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void RevealGenBelt()
        {
            genBelt.is_Hidden = false;
        }

        public bool GenBeltHidCheck()
        {
            if(genBelt.is_Hidden == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void GenBeltPickable()
        {
            genBelt.is_pickable = true;
            genBelt.description = "an elastic generator belt, a spare part for the generators.";
        }

        public bool CheckBeltPickable()
        {
            if (genBelt.is_pickable == true)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void SouthGenPowered()
        {
            controlRoom.description = "the control room is alive with blinking lights and monitors that cover every wall, desks with little knickknacks are laid in a orderd fashion.";
            sGen.description = "the generator is active";
            if (nGen.description == "the generator is active")
            {
                controlRoomActive = true;
            }
        }

        public bool SouthGenCheck()
        {
            if (sGen.description != "the generator is active")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void KeyPickable()
        {
            storageKeys.is_Hidden = false;
            storageKeys.is_pickable = true;
            janitor.description = "the janitor is flat on the floor, possibly even more bruised from when it fell to the ground";
        }

        public bool CheckKey()
        {
            if (storageKeys.is_Hidden == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void NorthGenPowerd()
        {
            nGen.description = "the generator is active";
            if (sGen.description == "the generator is active")
            {
                controlRoomActive = true;
            }
        }

        public bool nGenCheck()
        {
            if (nGen.description != "the generator is active")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void OpenStorage()
        {
            
            storageDoor.description = "the door is open";
            
            backUpGenControls.northVisible = true;

        }
        
        public bool CheckStorageOpen()
        {
            if (backUpGenControls.northVisible == true)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void OpenEscape()
        {
            Console.WriteLine("Intercom:'escape pod doors open, initiating escape pod.");
            escapePodDoor.description = "the door to the escape pod is now open.";
            forkHall.northVisible = true;
        }
        
        public bool CheckEscapePod()
        {
            if( forkHall.northVisible == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void EndGame()
        {
            Console.WriteLine("You feel a sense of guilt run through you, what if there is somebody still alive down here? there were so many people on this team, some of which you would call family.\n");
            System.Threading.Thread.Sleep(6000);
            Console.WriteLine("while considering your options, you are quickly by the threats that live down here. in the distance, at the end of the hallway, a creature staring back at you. It notices your presence and starts twitching violantly towards you.\n");
            System.Threading.Thread.Sleep(6000);
            Console.WriteLine("without hesitation this time, you slam your fist directly onto the button, the pod decouples from the base and you are shot straight to the surface.\n");
            System.Threading.Thread.Sleep(6000);
            Console.WriteLine("Opening the hatch, you squeeze your eyes like lemons at the rays of sunshine. It's a bitter sweet feeling but you are thankful you made it out with your life.\n");
            System.Threading.Thread.Sleep(6000);
            Console.WriteLine("Your mission is still not over, people need to be warned about whatever those things were down there. That's a goal only you can achieve, but as of right now? probably best to find a way back home\n");
            System.Threading.Thread.Sleep(6000);
            Console.WriteLine("congratulations!!!\n\nYou beat Too Deep!\n\nTHANK YOU FOR PLAYING!\n\n");
            System.Threading.Thread.Sleep(6000);
            Console.WriteLine("RETURNING TO MAIN MENU");
            System.Threading.Thread.Sleep(6000);
            end = true;
            
        }
        #endregion
    }
}
