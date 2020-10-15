using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace ToolLibrary.Helper
{
    public class IsoDateTimeConverterContent : IsoDateTimeConverter
    {
        /// <summary>
        /// Json日期格式
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is DateTime)
            {
                DateTime dateTime = (DateTime)value;
                if (dateTime == default(DateTime)
                    || dateTime == DateTime.MinValue
                    || dateTime.ToString("yyyy-MM-dd") == "1970-01-01"
                    || dateTime.ToString("yyyy-MM-dd") == "1900-01-01")
                {
                    writer.WriteValue("");
                    return;
                }
            }
            base.WriteJson(writer, value, serializer);
        }
    }
}
