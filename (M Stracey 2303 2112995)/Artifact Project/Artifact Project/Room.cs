using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace Artifact_Project
{
    public class Room
    {
        // for the xml serializing to work, it has to ignore the directions for the rooms as it. the reason for this is because it gets stuck in a loop serializing rooms through the referencing of the rooms from that room.
        [XmlIgnore]
        public Room north;
        [XmlIgnore]
        public Room south;
        [XmlIgnore]
        public Room east;
        [XmlIgnore]
        public Room west;
        

        public string roomName;
        public string description;

        public RoomContentsLister contentslister;// class holding the room objects in this room.

        //booleans for switching pathways to visible
        public bool northVisible;
        public bool southVisible;
        public bool eastVisible;
        public bool westVisible;

        #region default constructer
        public Room()//default constructer
        {
            northVisible = false;
            southVisible = false;
            eastVisible = false;
            westVisible = false;

            north = null;
            south = null;
            east = null;
            west = null;
            roomName = "This room is unnamed!";
            description = "no description";

            contentslister = new RoomContentsLister();


        }
        #endregion

        #region overide constructer
        public Room(string name, string description, Room north, Room south, Room east, Room west, bool isNorthVis = true, bool isSouthVis = true, bool isWestVis = true, bool isEastVis = true)
        {
            this.roomName = name;
            this.description = description;

            // these will equal the directions to new rooms
            this.north = north;
            this.south = south;
            this.east = east;
            this.west = west;

            contentslister = new RoomContentsLister();// list of objects in the room.

            northVisible = isNorthVis;
            southVisible = isSouthVis;
            eastVisible = isEastVis;
            westVisible = isWestVis;


        }
        #endregion

        #region add room object
        public void AddRoomObject(RoomObjects objName)// passes through the room class to the contents lister to add a object to the room.
        {
            contentslister.AddItem(objName);
        }
        #endregion

        #region remove room object
        public bool RemoveRoomObject(RoomObjects objName)//function to remove an item from the room through the contentslister.
        {
            return contentslister.RemoveItem(objName);
        }
        #endregion

        #region describe room
        public void DescribeRoom()// describes the room
        {
            Console.WriteLine("You are in the " + roomName + "\n");

            Console.WriteLine(description);//prints the description of the room seen in GameMap.

            contentslister.Show();// shows the objects in the  room.
        }
        #endregion

        #region describe exits
        public void DescribeExits()//used for when the player moves, shows which locations they can go.
        {


            if (north != null && northVisible == true)//checks if there is a existing north room and whether its visible.
            {
                Console.WriteLine("\nNorth: " + north.roomName);// prints the name of the north room.
            }
            if (south != null && southVisible == true)
            {
                Console.WriteLine("South: " + south.roomName);
            }
            if (east != null && eastVisible == true)
            {
                Console.WriteLine("East: " + east.roomName);
            }
            if (west != null && westVisible == true)
            {
                Console.WriteLine("West: " + west.roomName);
            }
        }
        #endregion

        #region get room object
        public RoomObjects GetRoomObject(string objName)
        {
            RoomObjects roomObj = contentslister.findItem(objName);// goes through room to the contents lister to get the room object from the contents list.

            return roomObj;
        }
        #endregion

        #region serialize room
        public void Serialize()
        {
            var path = this.roomName + ".xml";
            FileStream stream = File.Create(path);//creates a file for the room and calls it by the name of the room.

            XmlSerializer x = new XmlSerializer(this.GetType());//creates a XmlSerializer

            x.Serialize(stream, this);//serializes the entire room exept for the directions for new rooms.

            stream.Close();// closes the file
        }
        #endregion
        
        #region deserialize room
        public void Deserialize(GameMap gameMap)
        {
            var path = this.roomName + ".xml";
            FileStream stream = File.OpenRead(path);//finds the room file

            XmlSerializer x = new XmlSerializer(this.GetType());//creates serializer

            Room loadedRoom;
            loadedRoom = (Room)x.Deserialize(stream);//deserializes into a new room called loaded room.

            roomName = loadedRoom.roomName;// makes this room equal all the traits of the loaded room.
            description = loadedRoom.description;
            northVisible = loadedRoom.northVisible;
            southVisible = loadedRoom.southVisible;
            eastVisible = loadedRoom.eastVisible;
            westVisible = loadedRoom.westVisible;

            contentslister.ClearList();// clears the room of objects to avoid duplicates

            foreach (RoomObjects roomObj in loadedRoom.contentslister.contentsList)//adds the saved objects from the loaded room to the regular room.
            {
                RoomObjects realroomObj;
                realroomObj = gameMap.findRoomObject(roomObj.name);//finds the regular object.
                realroomObj.is_Hidden = roomObj.is_Hidden;
                realroomObj.is_pickable = roomObj.is_pickable;
                realroomObj.name = roomObj.name;
                realroomObj.description = roomObj.description;
                //gives all the real regular objects the states of the loaded objects.


                contentslister.AddItem(realroomObj);// adds the item to the contents of the room.
            }
            stream.Close();//closes the file.
        }
        #endregion

        #region serialize and deserialize the current room
        //this follows the same process for the serializing above except this is for the saved room/current room.
        public void SerializeCurrentRoom()
        {
            var path = "currentRoom.xml";
            FileStream stream = File.Create(path);

            XmlSerializer x = new XmlSerializer(this.GetType());

            x.Serialize(stream, this);

            stream.Close();
        }

        public void DeserializeCurrentRoom(GameMap gameMap)
        {
            var path = "currentRoom.xml";
            FileStream stream = File.OpenRead(path);

            XmlSerializer x = new XmlSerializer(this.GetType());

            Room loadedRoom;
            loadedRoom = (Room)x.Deserialize(stream);

            roomName = loadedRoom.roomName;
            description = loadedRoom.description;
            northVisible = loadedRoom.northVisible;
            southVisible = loadedRoom.southVisible;
            eastVisible = loadedRoom.eastVisible;
            westVisible = loadedRoom.westVisible;

            contentslister.ClearList();

            foreach (RoomObjects roomObj in loadedRoom.contentslister.contentsList)
            {
                RoomObjects realroomObj;
                realroomObj = gameMap.findRoomObject(roomObj.name);
                realroomObj.is_Hidden = roomObj.is_Hidden;
                realroomObj.is_pickable = roomObj.is_pickable;
                realroomObj.name = roomObj.name;
                realroomObj.description = roomObj.description;


                contentslister.AddItem(realroomObj);
            }
            stream.Close();
        }
        #endregion

    }
}
