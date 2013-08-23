using System;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Threading;
using System.Xml;

namespace Castles.Tools
{
    /// <summary>
    /// Class for xmlserialization
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Serializer<T>
    {
        public static void Serialize(string fileName, T input, string nmspc, XmlAttributeOverrides xmlao)
        {
            //get rid of header
            XmlWriterSettings writerSettings = new XmlWriterSettings {OmitXmlDeclaration = true};
            writerSettings.Indent = true;

            //Create our own namespaces for the output
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", nmspc);

            //clear default namespace
            XmlSerializer s = null;
            
            //create serializzer with overrides...
            s = xmlao==null ? new XmlSerializer(typeof(T)) : new XmlSerializer(typeof(T),xmlao);

            try
            {
                using (XmlWriter w = XmlWriter.Create(fileName, writerSettings))
                {
                    try
                    {
                        if (w != null) s.Serialize(w, input, ns);
                    }
                    catch (Exception ex)
                    {
						Console.WriteLine("Serialize: "+ex.ToString());
                    }
                }

            }
            catch (Exception ex)
            {
				Console.WriteLine("Serialize: "+ex.ToString());
            }

        }

        public static void Serialize(string fileName, T input)
        {
            Serialize(fileName, input, "", null);
        }

        public static void Serialize(string fileName, T input, XmlAttributeOverrides xmlao)
        {
            Serialize(fileName, input, "", xmlao);
        }

        public static string InnerXml(T input)
        {
            try
            {
                MemoryStream memoryStream = new MemoryStream();

                using (XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.Default))
                {
                    xmlTextWriter.Formatting = Formatting.Indented;
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                    xmlSerializer.Serialize(xmlTextWriter, input);

                    memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
                }

                return ByteArrayToString(Encoding.Default, memoryStream.ToArray());
            }
            catch
            {
                return string.Empty;
            }
            
        }

        private static Byte[] StringToByteArray(Encoding encoding, string xml)
        {
            return encoding.GetBytes(xml);
        }

        private static string ByteArrayToString(Encoding encoding, byte[] byteArray)
        {
            return encoding.GetString(byteArray);
        }

        public static T Deserialize(XmlElement element)
        {
            try
            {
                UTF8Encoding encoding = new UTF8Encoding();
                byte[] bArray = encoding.GetBytes(element.InnerXml);

                XmlSerializer s = new XmlSerializer(typeof(T));
                MemoryStream ms = new MemoryStream(bArray);

                T retval = (T)s.Deserialize(ms);

                ms.Close();

                return retval;
            }
            catch (Exception ex)
            {
				Console.WriteLine("Serialize: "+ex.ToString());
            }

            return default(T);


        }

        public static T Deserialize(object o)
        {
            if (o is string) return Deserialize((string)o);
            if (o is XmlElement) return Deserialize((XmlElement)o);

            return default(T);
        }

        public static T Deserialize(string fileName)
        {
            T retval = default(T);
            XmlSerializer s = new XmlSerializer(typeof (T));

            try
            {

                if (File.Exists(fileName))
                {
                    using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        using (TextReader r = new StreamReader(fs))
                        {
                            object o = s.Deserialize(r);
                            retval = (T) o;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
				Console.WriteLine("Deserialize: "+ex.ToString());
            }

            return retval;
        }

    }
}