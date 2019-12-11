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
            if (File.Exists("DATA.xml"))
            {
                XmlS(oData);
            }
        }

        public static List<ARKCommand> DatUnS()
        {
            try
            {
                List<ARKCommand> ls = new List<ARKCommand>();
                if (File.Exists("DATA.dat"))
                {
                    FileStream stream = new FileStream(@"DATA.dat", FileMode.Open);
                    BinaryFormatter bFormat = new BinaryFormatter();
                    ls = (List<ARKCommand>)bFormat.Deserialize(stream);
                    stream.Close();
                }
                else
                {
                    ls = XmlUnS();
                }
                return ls;
            }
            catch { return new List<ARKCommand>(); }
        }

        private static void XmlS(List<ARKCommand> oData)
        {
            using (FileStream stream = new FileStream(@"DATA.xml", FileMode.Create))
            {
                XmlSerializer xmlserilize = new XmlSerializer(typeof(List<ARKCommand>));
                xmlserilize.Serialize(stream, oData);
            }
        }

        private static List<ARKCommand> XmlUnS()
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

        public static string Transmit(string num) => ((MapEnum)int.Parse(num)).ToString();

        public static string UnTransmit(string name) => ((int)Enum.Parse(typeof(MapEnum), name)).ToString();
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

    public enum MapEnum
    {
        通用,
        孤岛,
        中心岛,
        焦土,
        畸变,
        仙境,
        灭绝,
        瓦尔盖罗
    }

    class ARKCompare : IComparer<ARKCommand>
    {
        public int Compare(ARKCommand x, ARKCommand y) => string.Compare(x.Name, y.Name);
    }
}
