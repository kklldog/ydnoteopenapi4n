//作者：       minjie.zhou
// 创建时间：   2012/12/9 23:46:49
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace YDNoteOpenAPI4N
{
    /// <summary>
    /// 以毫秒为单位的josn格式转换为datetime
    /// </summary>
    internal class YDDateTimeConverter4ms : JavaScriptDateTimeConverter
    {
        public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if(reader.Value!=null)
            {

                long javaScriptTicks = (long)reader.Value;
                var dateTime =  new DateTime(javaScriptTicks * 10000L + 621355968000000000L, DateTimeKind.Utc);
                return dateTime;
            }

            return null;

        }
    }
}
