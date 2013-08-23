using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Castles.Conf
{
    public class CastlesConfiguration
    {
        public string DataFolder { get; set; }

        public bool PermaLevelReloading {get;set;}
        public bool HighlightPathfinding {get;set;}

        public bool Immortal {get;set;}

        public string ActiveProfile { get; set; }


        public Bonuses Bonuses {get;set;}

        //public int EditingLevel {get;set;}
        //public int EditingScreen {get;set;}

        public EntityDef[] EntityDefs {get;set;}

        public Editor Editor {get;set;}

        public Board[] Levels { get; set; }

        public Graphics Graphics {get;set;}

        public Gfx[] Gfxs { get; set; }

        public EntityDef GetEntityByType(EntityType et)
        {
            foreach (EntityDef e in EntityDefs) 
            {
                if (e.ET == et) return e;
            }

            return new EntityDef();
        }
    }

    public class Editor
    {
        public DefaultPlatform DefaultPlatform {get;set;}
        public DefaultPlatform DefaultPlatformSpecial {get;set;}

        public int DisplayedDepth {get;set;}
        public int Level {get;set;}
        public int Screen {get;set;}
    }

    public class DefaultPlatform
    {
        [XmlAttribute]
        public string Gfx {get;set;}

        [XmlAttribute]
        public PlatformType Type {get;set;}
    }

    public class Bonuses
    {
        public int magichat {get;set;}
        public int cauldron {get;set;}
        public int booobak {get;set;}
        public int booobakUltra { get; set; }

        public int extraLifein {get;set;}
    }

    public class Board
    {
        [XmlAttribute]
        public string Name {get;set;}

        [XmlAttribute]
        public string Path {get;set;}

        [XmlAttribute]
        public string Tile {get;set;}

        [XmlAttribute]
        public int Level {get;set;}        
    }

    public class Graphics
    {
        [XmlAttribute]
        public int ShiftX {get;set;}
        
        [XmlAttribute]
        public int ShiftY {get;set;}
        
        [XmlAttribute]
        public int ShiftY2 {get;set;}
    }

    public class Gfx
    {
        [XmlAttribute]
        public string name {get;set;}
        
        [XmlAttribute]
        public string gfx {get;set;}


        [XmlAttribute]
        public string theme {get;set;}

        [XmlAttribute]
        public bool isBlock {get;set;}    

        [XmlAttribute]
        public bool isPit {get;set;}   

        [XmlAttribute]
        public bool isTeleport {get;set;}   

        [XmlAttribute]
        public bool isPressurePlate {get;set;}

        [XmlAttribute]
        public int probabilty {get;set;}
    }
}
