using System.Xml.Serialization;

namespace Castles.gameObjects.level
{
    public class door : IToogable
    {
        [XmlAttribute]
        public bool IsOpen { get; set; }

        public void Toogle()
        {
            IsOpen = !IsOpen;
        }
    }
}
