using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace YUI.WPF.YUtil
{
    /// <summary>
    /// xml辅助类
    /// </summary>
    public static class YXmlHelper
    {
        /// <summary>
        /// 将对象序列化为xml
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string SerializeObject<T>(T o)
        {
            return SerializeObject(o, null, null, false, Encoding.UTF8, true);
        }

        /// <summary>
        /// 将对象序列化为xml
        /// </summary>
        /// <param name="o"></param>
        /// <param name="indent"></param>
        /// <param name="hasHeader"></param>
        /// <returns></returns>
        public static string SerializeObject<T>(T o, bool indent, bool hasHeader = true)
        {
            return SerializeObject(o, null, null, indent, Encoding.UTF8, hasHeader);
        }

        /// <summary>
        /// 将对象序列化为xml
        /// </summary>
        /// <param name="o"></param>
        /// <param name="indent"></param>
        /// <param name="encoding"></param>
        /// <param name="hasHeader"></param>
        /// <returns></returns>
        public static string SerializeObject<T>(T o, bool indent, Encoding encoding, bool hasHeader = true)
        {
            return SerializeObject(o, null, null, indent, encoding, hasHeader);
        }

        private sealed class StringWriterWithEncoding : StringWriter
        {
            public override Encoding Encoding { get; }

            public StringWriterWithEncoding(Encoding encoding)
            {
                Encoding = encoding;
            }
        }

        /// <summary>
        /// 将对象序列化为xml
        /// </summary>
        /// <param name="o"></param>
        /// <param name="namespaces"></param>
        /// <param name="xmlRootName"></param>
        /// <param name="indent"></param>
        /// <param name="encoding"></param>
        /// <param name="hasHeader"></param>
        /// <returns></returns>
        public static string SerializeObject<T>(T o, XmlSerializerNamespaces namespaces, string xmlRootName = null, bool indent = false, Encoding encoding = null, bool hasHeader = false)
        {
            var settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = !hasHeader,
                Indent = indent,
                IndentChars = "    ",
                Encoding = encoding
            };

            var sb = new StringWriterWithEncoding(encoding);

            using (var writer = XmlWriter.Create(sb, settings))
            {
                var xmlSerializer = string.IsNullOrWhiteSpace(xmlRootName)
                    ? new XmlSerializer(typeof(T))
                    : new XmlSerializer(typeof(T), new XmlRootAttribute(xmlRootName));

                xmlSerializer.Serialize(writer, o, namespaces);
            }

            return sb.ToString();
        }

        /// <summary>
        /// 将xml反序列化为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T DeserializeToObject<T>(string xml) where T : class
        {
            return DeserializeToObject<T>(xml, null);
        }

        /// <summary>
        /// 将xml反序列化为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <param name="defaultNamespace"></param>
        /// <returns></returns>
        public static T DeserializeToObject<T>(string xml, string defaultNamespace) where T : class
        {
            object result;
            using (var reader = new StringReader(xml))
            {
                var xmlSerializer = new XmlSerializer(typeof(T), defaultNamespace);
                result = xmlSerializer.Deserialize(reader);
            }

            return result as T;
        }

        /// <summary>
        /// 从文件读取并反序列化为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static T LoadFromFileToObject<T>(string filePath, Encoding encoding) where T : class
        {
            return LoadFromFileToObject<T>(filePath, null, encoding);
        }

        /// <summary>
        /// 从文件读取并反序列化为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="defaultNamespace"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static T LoadFromFileToObject<T>(string filePath, string defaultNamespace, Encoding encoding) where T : class
        {
            var data = File.ReadAllText(filePath, encoding);
            return DeserializeToObject<T>(data, defaultNamespace);
        }

        /// <summary>
        /// 从文件读取并反序列化为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static T LoadFromFileToObject<T>(string filePath) where T : class
        {
            return LoadFromFileToObject<T>(filePath, string.Empty);
        }

        /// <summary>
        /// 从文件读取并反序列化为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="defaultNamespace"></param>
        /// <returns></returns>
        public static T LoadFromFileToObject<T>(string filePath, string defaultNamespace) where T : class
        {
            var data = File.ReadAllText(filePath);
            return DeserializeToObject<T>(data, defaultNamespace);
        }
    }
}
