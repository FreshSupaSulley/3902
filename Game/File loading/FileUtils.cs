using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Entities;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using Game.Rooms;
using demo.Game.File_loading;
using Game.Tiles;
using System.Reflection.Metadata;
using System.Numerics;

namespace demo.Game
{
    //provides static methods that can be called to load levels, assign character sprites, etc.
    internal class FileUtils
    {
        //specifies Tiles and Entities that will be present in a given room
        public static Room loadRoom(Room rm, String filename)
        {
            XmlSerializer ser = new XmlSerializer(typeof(TileType[]));
            ser.UnknownAttribute += new XmlAttributeEventHandler(attrUnknown);
            ser.UnknownNode += new XmlNodeEventHandler(nodeUnknown);
            FileStream fs = new FileStream(filename + "_tiles.xml", FileMode.Open);
            rm.tiles = (TileType[])ser.Deserialize(fs);
            fs = new FileStream(filename + "_entities.xml", FileMode.Open);
            ser = new XmlSerializer(typeof(String[]));
            ser.UnknownAttribute += new XmlAttributeEventHandler(attrUnknown);
            ser.UnknownNode += new XmlNodeEventHandler(nodeUnknown);
            String[] ents = (String[])ser.Deserialize(fs);
            for(int i = 0; i < ents.Length; i++)
            {
                rm.AddEntity(FileUtils.parse(ents[i]));
                System.Diagnostics.Process.Start("CMD.exe", "/C echo " + i + " " + ents[i]);
            }
            fs.Close();
            return rm;
        }

        public static Entity parse(String s)
        {
            Entity ent = null;
            String type = s.Substring(0, s.IndexOf(" "));
            String pos = s.Substring(s.IndexOf(" "));
            Vector2 position = new Vector2(int.Parse(pos.Substring(pos.IndexOf("X:") + 2, pos.IndexOf(" "))), int.Parse(pos.Substring(pos.IndexOf("Y:") + 1, pos.IndexOf("}"))));
            if(type.IndexOf("Player") > -1)
            {
                ent = new Player();
                ent.Position = position;
            }
            if(type.IndexOf("Dragon") > -1)
            {
                ent = new Dragon(position);
            }
            if(type.IndexOf("Gohma") > -1)
            {
                ent = new Gohma(position);
            }
            if(type.IndexOf("Fireball") > -1)
            {
                ent = new Fireball(position);
            }
            return ent;
        }

        //serializes rm into XML files with the supplied prefix, one for the tiles, another for the entities
        public static void saveRoom(Room rm, String filename)
        {
            TextWriter w = new StreamWriter(filename + "_tiles.xml");
            XmlSerializer ser = new XmlSerializer(typeof(TileType[]));
            ser.Serialize(w, rm.tiles);
            w.Close();
            w = new StreamWriter(filename + "_entities.xml");
            String[] ents = new String[rm.gameObjects.Count];
            for (int i = 0; i < ents.Length; i++) { ents[i] = rm.gameObjects[i].GetType() + " " + rm.gameObjects[i].Position.ToString(); }
            ser = new XmlSerializer(typeof(String[]));
            ser.Serialize(w, ents);
            w.Close();
        }
        //easily switch between player sprite sheets if so desired
        public static Player loadPlayer(String filename)
        {
            Player p = new Player();
            XmlSerializer ser = new XmlSerializer(typeof(Player));
            ser.UnknownNode += new XmlNodeEventHandler(nodeUnknown);
            ser.UnknownAttribute += new XmlAttributeEventHandler(attrUnknown);
            FileStream fs = new FileStream(filename, FileMode.Open);
            p = (Player)ser.Deserialize(fs);
            fs.Close();
            return p;
        }

        private static void nodeUnknown(object sender, XmlNodeEventArgs e)
        {
            System.Diagnostics.Process.Start("CMD.exe", "/C echo unknown node");
        } 

        private static void attrUnknown(object sender, XmlAttributeEventArgs e)
        {
            System.Diagnostics.Process.Start("CMD.exe", "/C echo unknown attribute");
        }
    }
}
