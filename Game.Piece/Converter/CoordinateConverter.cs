using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Game.Framework
{
    public class CoordinateConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Coordinate);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);
            int x = jObject["X"].Value<int>();
            int y = jObject["Y"].Value<int>();
            return Coordinate.GetInstance(x, y);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Coordinate coordinate = (Coordinate)value;
            JObject jObject = new JObject
        {
            { "X", coordinate.X },
            { "Y", coordinate.Y }
        };
            jObject.WriteTo(writer);
        }
    }
}