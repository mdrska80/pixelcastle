using System.Xml.Serialization;

namespace Castles
{
    public class Door : IToogable
    {
        [XmlAttribute]
        public bool IsOpen { get; set; }

        /// <summary>
        /// Door theme
        /// </summary>
        [XmlAttribute]
        public string Theme { get; set; }

        public void Toogle()
        {
            IsOpen = !IsOpen;
        }
    }
}
