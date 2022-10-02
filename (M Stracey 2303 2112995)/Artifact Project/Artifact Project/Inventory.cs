using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace Artifact_Project
{
    public class Inventory
    {
        public List<RoomObjects> inventoryList = new List<RoomObjects>();// list where all the objects the player picks up are held.

        #region add Item
        public void AddItem(RoomObjects obj)
        {
            inventoryList.Add(obj);// adds the object to the inventory list.
        }
        #endregion

        #region find inventory item
        public RoomObjects FindInventoryItem(string objName)
        {
            foreach (RoomObjects roomObj in inventoryList)// goes through each object in the inventory untill the name of the object matches the players input
            {
                if (roomObj.name == objName)
                {
                    return roomObj;// returns the object called.
                }
            }
            return null;// if the players input does not match a name object, it returns null.
        }
        #endregion

        #region show inventory
        public void Show()
        {
            int count = 0;

            Console.WriteLine("your inventory:");

            foreach (RoomObjects roomObj in inventoryList)// for each object in the inventory, it prints the name of the object.
            {
                Console.WriteLine(roomObj.name);
                count++;// counts how many objects are in the inventory
            }

            if (count == 0)
            {
                Console.WriteLine("you do not have anything in your inventory");// if the player has 0 objects in their inventory, it returns this line.
            }
        }
        #endregion

        #region Serialize Inventory
        public void SerializeInv()
        {
            var path ="playerinventory.xml";
            FileStream stream = File.Create(path);//creates a xml file and calls it by the path name(playerinventory.xml).

            XmlSerializer x = new XmlSerializer(this.GetType());//creates a XmlSerializer.

            x.Serialize(stream, this);//serializes the inventory

            stream.Close();//closes the file
        }
        #endregion

        #region DeSerialize Inventory
        public void DeSerializeInv()
        {
            var path = "playerinventory.xml";
            FileStream stream = File.OpenRead(path);//opens the inventory file.

            XmlSerializer x = new XmlSerializer(this.GetType());// creates a new XmlSerializer

            Inventory loadedInventory;
            loadedInventory = (Inventory)x.Deserialize(stream); // creates a new inventory and Deserializes the saved content into that new loadedInventory.



            inventoryList.Clear();// clears the current inventory.

            foreach (RoomObjects roomObj in loadedInventory.inventoryList)// for each object in loadedinventory, it will add that object to the regular inventory.
            {
                inventoryList.Add(roomObj);
            }
            stream.Close();// closes the file.
        }
        #endregion
    }
}
