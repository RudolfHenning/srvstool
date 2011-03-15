using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace SrvsTool
{
    static class SerializationUtils
    {
        public static string SerializeXML(object classToSerialize)
        {
            XmlSerializer ser = new XmlSerializer(classToSerialize.GetType());
            StringBuilder sb = new StringBuilder();
            using (StringWriter writer = new StringWriter(sb))
            {
                ser.Serialize(writer, classToSerialize);
            }
            return sb.ToString();
        }
        public static void SerializeXMLAndSave(object classToSerialize, string fileName)
        {
            XmlSerializer ser = new XmlSerializer(classToSerialize.GetType());
            StringBuilder sb = new StringBuilder();
            using (StringWriter writer = new StringWriter(sb))
            {
                ser.Serialize(writer, classToSerialize);
            }
            using (StreamWriter output2 = new StreamWriter(fileName, false, Encoding.Unicode))
            {
                output2.Write(sb.ToString());
                output2.Flush();
                output2.Close();
            } 
        }
        public static object DeserializeXMLFile(string filePath, System.Type typeOfObject)
        {
            object result = null;
            if (File.Exists(filePath))
            {
                string fileContents = System.IO.File.ReadAllText(filePath);
                if (fileContents.Length > 0)
                {
                    result = DeserializeXML(fileContents, typeOfObject);
                }
            }
            return result;
        }
        public static object DeserializeXML(string serializedObject, System.Type typeOfObject)
        {
            XmlSerializer ser = new XmlSerializer(typeOfObject);
            object result = null;

            using (MemoryStream input = new MemoryStream(System.Text.Encoding.Unicode.GetBytes(serializedObject)))
            {
                input.Position = 0;
                result = ser.Deserialize(input);
            }
            return result;
        }
    }
}
