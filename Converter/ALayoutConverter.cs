﻿using Chess;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

public class ALayoutConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return typeof(ALayout).IsAssignableFrom(objectType);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        JObject jsonObj = JObject.Load(reader);
        var layout = (ALayout)Activator.CreateInstance(objectType);
        foreach (var item in jsonObj)
        {
            var key = JsonConvert.DeserializeObject<Coordinate>(item.Key);
            var value = item.Value.ToObject<APiece>(serializer);
            layout.Add(key, value);
        }
        return layout;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        var layout = (ALayout)value;
        var dict = new Dictionary<string, object>();

        foreach (var kvp in layout)
        {
            dict[JsonConvert.SerializeObject(kvp.Key)] = kvp.Value;
        }

        serializer.Serialize(writer, dict);
    }
}
