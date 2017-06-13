using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace YUI.YUtil
{
    /// <summary>
    /// Json辅助类（此类依赖Newtonsoft.Json库）
    /// </summary>
    public static class YJsonHelper
    {
        private static readonly MethodInfo SerializeObjectMethodInfo;
        private static readonly MethodInfo DeserializeObjectMethodInfo;
        private static readonly MethodInfo JsonParseMethodInfo;
        private static readonly MethodInfo JsonTostringMethodInfo;
        private static readonly Type IsoDateTimeConverterType;
        private static readonly Type JsonConverterType;

        static YJsonHelper()
        {
            var assembly = Assembly.LoadFrom("Newtonsoft.Json.dll");
            if (assembly == null) return;
            var type = assembly.GetType("Newtonsoft.Json.JsonConvert");
            if (type == null) return;
            JsonConverterType = assembly.GetType("Newtonsoft.Json.JsonConverter");
            var jsonFormattingType = assembly.GetType("Newtonsoft.Json.Formatting");
            SerializeObjectMethodInfo = type.GetMethod("SerializeObject", new[] {typeof(object), JsonConverterType.MakeArrayType()});
            IsoDateTimeConverterType = assembly.GetType("Newtonsoft.Json.Converters.IsoDateTimeConverter");

            DeserializeObjectMethodInfo = type.GetMethod("DeserializeObject", new[] {typeof(string), JsonConverterType.MakeArrayType()});

            type = assembly.GetType("Newtonsoft.Json.Linq.JToken");
            if (type == null) return;
            JsonParseMethodInfo = type.GetMethod("Parse", new[] {typeof(string)});
            JsonTostringMethodInfo = type.GetMethod("ToString", new[] {jsonFormattingType, JsonConverterType.MakeArrayType()});
        }
        
        /// <summary>
        /// 将对象序列化为JSON格式
        /// </summary>
        /// <param name="o">对象</param>
        /// <param name="dateTimeFormat">时间格式</param>
        /// <returns>json字符串</returns>
        public static string SerializeObject(object o, string dateTimeFormat = "yyyy-MM-dd HH:mm:ss")
        {
            return SerializeObjectMethodInfo.Invoke(null, new[] {o, GetIsoDateTimeConverterArray(dateTimeFormat)}) as string;
        }

        /// <summary>
        /// 反序列化为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json字符串(eg.{"ID":"112","Name":"石子儿"})</param>
        /// <param name="dateTimeFormat">时间格式</param>
        /// <returns>对象实体</returns>
        public static T DeserializeToObject<T>(string json, string dateTimeFormat = "yyyy-MM-dd HH:mm:ss") where T : class
        {
            return DeserializeObjectMethodInfo.MakeGenericMethod(typeof(T))
                .Invoke(null, new object[] {json, GetIsoDateTimeConverterArray(dateTimeFormat)}) as T;
        }

        /// <summary>
        /// 从文件读取并反序列化为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="dateTimeFormat"></param>
        /// <returns></returns>
        public static T LoadFromFileToObject<T>(string filePath, string dateTimeFormat = "yyyy-MM-dd HH:mm:ss") where T : class
        {
            var data = File.ReadAllText(filePath);
            return DeserializeToObject<T>(data, dateTimeFormat);
        }

        /// <summary>
        /// 反序列化为对象集合
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json数组字符串(eg.[{"ID":"112","Name":"石子儿"}])</param>
        /// <param name="dateTimeFormat">时间格式</param>
        /// <returns>对象实体集合</returns>
        public static List<T> DeserializeToList<T>(string json, string dateTimeFormat = "yyyy-MM-dd HH:mm:ss") where T : class
        {
            return DeserializeObjectMethodInfo.MakeGenericMethod(typeof(List<T>)).Invoke(null, new object[] { json, GetIsoDateTimeConverterArray(dateTimeFormat) }) as List<T>;
        }

        /// <summary>
        /// 从文件读取并反序列化为对象集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="dateTimeFormat"></param>
        /// <returns></returns>
        public static List<T> LoadFromFileToList<T>(string filePath, string dateTimeFormat = "yyyy-MM-dd HH:mm:ss") where T : class
        {
            var data = File.ReadAllText(filePath);
            return DeserializeToList<T>(data, dateTimeFormat);
        }

        /// <summary>
        /// 格式化Json
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static string FormatJson(string json)
        {
            var jt = JsonParseMethodInfo.Invoke(null, new object[] {json});
            return JsonTostringMethodInfo.Invoke(jt, new object[] {1, null}) as string;
        }

        /// <summary>
        /// 压缩Json
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static string UnFormatJson(string json)
        {
            var jt = JsonParseMethodInfo.Invoke(null, new object[] { json });
            return JsonTostringMethodInfo.Invoke(jt, new object[] { 0, null }) as string;
        }

        private static Array GetIsoDateTimeConverterArray(string dateTimeFormat)
        {
            if (JsonConverterType == null)
                throw new Exception("未加载【Newtonsoft.Json.dll】,请确保添加了该动态库");

            dynamic instance = Activator.CreateInstance(IsoDateTimeConverterType);
            instance.DateTimeFormat = dateTimeFormat;

            var convertArray = Array.CreateInstance(JsonConverterType, 1);
            convertArray.SetValue(instance, 0);
            return convertArray;
        }
    }
}
