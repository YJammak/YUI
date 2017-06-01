using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace YUI.YUtil
{
    /// <summary>
    /// xml辅助类
    /// </summary>
    public static class YXmlHelper
    {
        private static readonly XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();

        static YXmlHelper()
        {
            namespaces.Add(string.Empty, string.Empty);
        }

        /// <summary>
        /// 将对象序列化为xml
        /// </summary>
        /// <param name="o"></param>
        /// <param name="xmlRootName"></param>
        /// <param name="indent"></param>
        /// <param name="encoding"></param>
        /// <param name="hasHeader"></param>
        /// <returns></returns>
        public static string SerializeObject(object o, string xmlRootName = null, bool indent = false, Encoding encoding = null, bool hasHeader = false)
        {
            var type = o.GetType();

            var settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = !hasHeader,
                Indent = indent,
                Encoding = encoding ?? Encoding.Default
            };

            var sb = new StringBuilder();

            using (var writer = XmlWriter.Create(sb, settings))
            {
                var xmlSerializer = string.IsNullOrWhiteSpace(xmlRootName)
                    ? new XmlSerializer(type)
                    : new XmlSerializer(type, new XmlRootAttribute(xmlRootName));

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
        public static T DeserializeXmlToObject<T>(string xml) where T : class
        {
            object result;
            using (var reader = new StringReader(xml))
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                result = xmlSerializer.Deserialize(reader);
            }

            return result as T;
        }
    }
}
