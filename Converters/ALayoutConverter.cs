using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Chess
{
    public class ALayoutConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ALayout) || objectType.IsSubclassOf(typeof(ALayout));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jsonObj = JObject.Load(reader);
            var layout = (ALayout)Activator.CreateInstance(objectType);
            foreach (var item in jsonObj)
            {
                string[] parts = item.Key.Split(',');
                int x = int.Parse(parts[0]);
                int y = int.Parse(parts[1]);
                Coordinate coord = Coordinate.GetInstance(x, y);
                APiece piece = item.Value.ToObject<APiece>(serializer);
                layout.Add(coord, piece);
            }
            return layout;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var layout = (ALayout)value;
            JObject jsonObj = new JObject();
            foreach (var kvp in layout)
            {
                string key = $"{kvp.Key.X},{kvp.Key.Y}";
                jsonObj.Add(key, JToken.FromObject(kvp.Value, serializer));
            }
            jsonObj.WriteTo(writer);
        }
    }
}
