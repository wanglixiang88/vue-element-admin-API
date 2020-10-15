using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace ToolLibrary.Helper
{
    /// <summary>
    /// Json处理类
    /// </summary>
    public static class JsonHelper
    {
        private static JsonSerializerSettings _jsonSettings;

        /// <summary>
        /// 格式化数据
        /// </summary>
        static JsonHelper()
        {
            IsoDateTimeConverter datetimeConverter = new IsoDateTimeConverterContent();
            datetimeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

            _jsonSettings = new JsonSerializerSettings();
            _jsonSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
            _jsonSettings.NullValueHandling = NullValueHandling.Ignore;
            _jsonSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            _jsonSettings.Converters.Add(datetimeConverter);
            _jsonSettings.ContractResolver = new LowercaseContractResolver();
        }

        /// <summary>
        /// 将指定的对象序列化成 JSON 数据。
        /// </summary>
        /// <param name="obj">要序列化的对象。</param>
        /// <returns></returns>
        public static string Serialize(object obj)
        {
            try
            {
                if (null == obj)
                    return null;

                return JsonConvert.SerializeObject(obj, Formatting.None, _jsonSettings);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 将指定的对象序列化成 JSON 数据。
        /// </summary>
        /// <param name="obj">要序列化的对象。</param>
        /// <returns></returns>
        public static string SerializeNoSetting(object obj)
        {
            try
            {
                IsoDateTimeConverter datetimeConverter = new IsoDateTimeConverterContent();
                datetimeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                JsonSerializerSettings jsonSettings = new JsonSerializerSettings();
                jsonSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
                jsonSettings.NullValueHandling = NullValueHandling.Ignore;
                jsonSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                jsonSettings.Converters.Add(datetimeConverter);

                if (null == obj)
                    return null;

                return JsonConvert.SerializeObject(obj, Formatting.None);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 将指定的 JSON 数据反序列化成指定对象。
        /// </summary>
        /// <typeparam name="T">对象类型。</typeparam>
        /// <param name="json">JSON 数据。</param>
        /// <returns></returns>
        public static T Deserialize<T>(string json)
        {
            if (string.IsNullOrEmpty(json))
                return default(T);
            try
            {
                return JsonConvert.DeserializeObject<T>(json, _jsonSettings);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 反序列化对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T JsonToModel<T>(string json)
        {
            T request;
            //反序列化
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(json)))
            {
                DataContractJsonSerializer deseralizer = new DataContractJsonSerializer(typeof(T));
                request = (T)deseralizer.ReadObject(ms);// //反序列化ReadObject
            }
            return request;
        }

        /// <summary>
        /// 将指定的 JSON 数据反序列化成指定对象。
        /// </summary>
        /// <typeparam name="List<T>">对象集合</typeparam>
        /// <param name="json">JSON 数据</param>
        /// <returns></returns>
        public static List<T> DeserializeList<T>(string json)
        {
            if (string.IsNullOrEmpty(json))
                return default(List<T>);
            try
            {
                return JsonConvert.DeserializeObject<List<T>>(json, _jsonSettings);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 将转换后的Key全部设置为小写
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static SortedDictionary<string, object> DeserializeLower(string json)
        {
            var obj = Deserialize<SortedDictionary<string, object>>(json);
            SortedDictionary<string, object> nobj = new SortedDictionary<string, object>();

            foreach (var item in obj)
            {
                nobj[item.Key.ToLower()] = item.Value;
            }
            obj.Clear();
            obj = null;
            return nobj;
        }

        /// <summary>
        /// 多层嵌套
        /// </summary>
        /// <param name="jToken"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetJsonValue(JEnumerable<JToken> jToken, string key)
        {
            IEnumerator enumerator = jToken.GetEnumerator();
            while (enumerator.MoveNext())
            {
                JToken jc = (JToken)enumerator.Current;
                if (jc is JObject || ((JProperty)jc).Value is JObject)
                {
                    return GetJsonValue(jc.Children(), key);
                }
                else
                {
                    if (((JProperty)jc).Name == key)
                    {
                        return ((JProperty)jc).Value.ToString();
                    }
                }
            }
            return null;
        }
    }

    public class LowercaseContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.ToLower();
        }
    }
}
