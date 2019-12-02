using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ARKcommands
{
    public static class function
    {
        public static void DatS(List<ARKCommand> oData)
        {
            using (FileStream stream = new FileStream(@"DATA.dat", FileMode.Create))
            {
                BinaryFormatter bFormat = new BinaryFormatter();
                bFormat.Serialize(stream, oData);
            }
        }
        public static List<ARKCommand> DatUnS()
        {
            try
            {
                FileStream stream = new FileStream(@"DATA.dat", FileMode.Open);
                BinaryFormatter bFormat = new BinaryFormatter();
                List<ARKCommand> ls = (List<ARKCommand>)bFormat.Deserialize(stream);
                stream.Close();
                return ls;
            }
            catch { return new List<ARKCommand>(); }
        }

        public static void XmlS(List<ARKCommand> oData)
        {
            using (FileStream stream = new FileStream(@"DATA.xml", FileMode.Create))
            {
                XmlSerializer xmlserilize = new XmlSerializer(typeof(List<ARKCommand>));
                xmlserilize.Serialize(stream, oData);
            }
        }
        public static List<ARKCommand> XmlUnS()
        {
            try
            {
                FileStream stream = new FileStream(@"DATA.xml", FileMode.Open);
                XmlSerializer xmlserilize = new XmlSerializer(typeof(List<ARKCommand>));
                List<ARKCommand> ls = (List<ARKCommand>)xmlserilize.Deserialize(stream);
                stream.Close();
                return ls;
            }
            catch { return new List<ARKCommand>(); }
        }
    }

    [Serializable]
    [XmlRoot("ARKCommand")]
    public class ARKCommand
    {
        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlElement("Command")]
        public string Command { get; set; }
        [XmlElement("Type")]
        public string Type { get; set; }
        [XmlElement("Map")]
        public string Map { get; set; }
        [XmlElement("Special")]
        public string Special { get; set; }
    }
}
