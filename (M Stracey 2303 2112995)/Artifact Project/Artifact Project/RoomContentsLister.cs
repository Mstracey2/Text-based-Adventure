using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Artifact_Project
{
    public class RoomContentsLister
    {
        public List<RoomObjects> contentsList = new List<RoomObjects>();// list that holds all the objects of one room.

        public RoomContentsLister()
        {
            
        }

        #region Add Item
        public void AddItem(RoomObjects obj)// fuction that adds objects to the contents list
        {
            contentsList.Add(obj);
        }
        #endregion

        #region remove item
        public bool RemoveItem(RoomObjects obj)// function to remove items from the list/room.
        {
            return contentsList.Remove(obj);
        }
        #endregion

        #region find item
        public RoomObjects findItem(string objName)// this function is used to find the object in the room that the player has inputted.
        {
            foreach (RoomObjects roomObj in contentsList)// goes through the list of objects in the room.
            {
                if (roomObj.name == objName)// if object is found (by name), returns that object.
                {
                    return roomObj;
                }
            }
            return null;//returns null if object cant be found.
        }
        #endregion

        #region show room objects
        public void Show()//function to show the items in the room
        {
            int count = 0;

            Console.WriteLine("\nthe room contains:");

            foreach (RoomObjects roomObj in contentsList)// goes through all the objects in the list in the current room
            {
                if (roomObj.is_Hidden == false)
                {
                    Console.WriteLine(roomObj.name);
                    count++;
                }
            }

            if (count == 0)// if there is nothin in this room, print this.
            {
                Console.WriteLine("there is nothing you can see in this room");
            }
        }
        #endregion

        #region clear
        public void ClearList()// this clears the list, it is used when deserializing the room so there are no duplicates.
        {
            contentsList.Clear();
        }
        #endregion
    }
}
