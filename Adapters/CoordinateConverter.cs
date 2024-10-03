using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Chess
{
    public class CoordinateConverter : JsonConverter<Coordinate>
    {
        public override Coordinate Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.String)
            {
                throw new JsonException();
            }

            string[] parts = reader.GetString()?.Split(',');
            if (parts?.Length != 2 || !int.TryParse(parts[0], out int row) || !int.TryParse(parts[1], out int col))
            {
                throw new JsonException();
            }

            return Coordinate.GetInstance(row, col);
        }

        public override void Write(Utf8JsonWriter writer, Coordinate value, JsonSerializerOptions options)
        {
            writer.WriteStringValue($"{value.X},{value.Y}");
        }
    }
}
