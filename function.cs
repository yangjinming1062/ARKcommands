using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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

        public static string Transmit(string Code)
        {
            switch (Code)
            {
                case "1": return "孤岛";
                case "2": return "中心岛";
                case "3": return "焦土";
                case "4": return "畸变";
                case "5": return "仙境";
                case "6": return "灭绝";
                case "7": return "瓦尔盖罗";
            }
            return "";
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

        public override string ToString() => string.Format("{0} [{1}] [{2}] [{3}]", Name, Type, function.Transmit(Map), Special).Replace("[]", "");
    }
}
