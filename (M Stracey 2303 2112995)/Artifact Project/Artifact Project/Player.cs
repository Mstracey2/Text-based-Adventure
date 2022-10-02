using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;




namespace Artifact_Project
{
    public class Player
    {
        public Room currentRoom; // the players current room
        public Inventory playerInv = new Inventory();// players inventory
        
        


        public Player(Room startRoom)// contructer, holds the location of start room.
        {
            currentRoom = startRoom;// puts the current room as the start room
        }


        #region Move
        public void Move()
        {
            bool move = true;
            while (move == true)
            {

                Console.WriteLine("go where? (please respond with N/S/E/W)");

                currentRoom.DescribeExits();// function which shows visible exits to the player

                string direction = Console.ReadLine();

                if (direction == "N")
                {
                    if (currentRoom.north != null && currentRoom.northVisible == true) // checks if there is a north hallway and checks if its visible
                    {
                        currentRoom = currentRoom.north; // changes current room to the room equal to north
                        move = false;
                    }
                    else
                    {
                        Console.WriteLine("there is no North");
                    }
                }
                else if (direction == "S")
                {
                    if (currentRoom.south != null && currentRoom.southVisible == true)
                    {
                        currentRoom = currentRoom.south;
                        move = false;
                    }
                    else
                    {
                        Console.WriteLine("there is no South");
                    }
                }
                else if (direction == "E")
                {
                    if (currentRoom.east != null && currentRoom.eastVisible == true)
                    {
                        currentRoom = currentRoom.east;
                        move = false;
                    }
                    else
                    {
                        Console.WriteLine("there is no East");
                    }
                }
                else if (direction == "W")
                {
                    if (currentRoom.west != null && currentRoom.westVisible == true)
                    {
                        currentRoom = currentRoom.west;
                        move = false;
                    }
                    else
                    {
                        Console.WriteLine("there is no West");
                    }
                }
                else
                {
                    Console.WriteLine("wrong input");
                }
                
            }

        }
        #endregion

        #region Look around 
        public void LookAroundRoom()
        {
            currentRoom.DescribeRoom();// function that describes the room, go through Player to be able to access the current room.
        }
        #endregion

        #region display help
        public void DisplayHelp()// displays all the controls for the player
        {
            Console.WriteLine("help");
            Console.WriteLine("\n'view' to view whats inside the room.");
            Console.WriteLine("'move' allows you to move room (you can move by entering N/S/E/W, whether you can go in that direction depends on the room).");
            Console.WriteLine("'examine [object]' to examine an object(remember: some objects that you examine can lead to finding new objects).");
            Console.WriteLine("'pickup [object]' to pick up an object.");
            Console.WriteLine("'inv' to view your inventory.");
            Console.WriteLine("'use [inventory object] with [static object]' to use objects with other objects.");
            Console.WriteLine("'save' to save your progress");
            Console.WriteLine("'load' to load your latest save");
        }
        #endregion

        #region pickup objects
        public void Pickup(string objName)
        {
            RoomObjects roomObject = currentRoom.GetRoomObject(objName);// calls the function that identifies the object in the current room that the player has asked for.

            if (roomObject != null)// if the function can't find the object, it remains null.
            {
                if (roomObject.is_pickable && roomObject.is_Hidden == false)// checks if the object in question is pickable and not hidden.
                {
                    playerInv.AddItem(roomObject);// adds the item to the players inventory through the AddItem function.
                    currentRoom.RemoveRoomObject(roomObject);// function that removes the object from the room.
                    Console.WriteLine("You Picked up the " + objName + ", it is now in your inventory");
                }
                else
                {
                    Console.WriteLine("you are unable to pickup the " + roomObject.name);
                }
            }
            else
            {
                Console.WriteLine("can't find " + objName + " in this room");
            }
        }
        #endregion

        #region Look at inventory
        public void LookInInventory()
        {
            playerInv.Show();// function that shows the players inventory, has to go through Player to have access to the inventory
        }
        #endregion

        #region examine
        public void Examine(string objName, GameMap creater)
        {
            RoomObjects roomObject = currentRoom.GetRoomObject(objName);// calls the same GetRoomObject function to identify the object in the room.
            if (roomObject != null)
            {
                roomObject.ExamineCheck(roomObject, creater); // function which shows the objects description and more depending on if its a derived class object.
            }
            else
            {
                Console.WriteLine("Can't find an item of your description");
            }
        }
        #endregion

        #region Use object
        public void UseObjects(string firstObjName, string withObjName, GameMap creater)
        {
            RoomObjects withRoomObject = currentRoom.GetRoomObject(withObjName);// same function to check to see if the object exists inside the room.
            if (withRoomObject != null)
            {
                RoomObjects inventoryItem = playerInv.FindInventoryItem(firstObjName);// this function does the same check but for the inventory instead
                if (inventoryItem != null)
                {
                    inventoryItem.UseObject(withRoomObject, creater);//function that holds the process of the use objects command(outcome changes depending on derived classes).
                }
                else
                {
                    Console.WriteLine("You are not carrying a " + firstObjName);
                }
            }
            else
            {
                Console.WriteLine("Can't find an item called " + withObjName + " to use in this room");
            }
        }
        #endregion
    }
}
