using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Chess
{
    public class ChessLayoutConverter : JsonConverter<ChessLayout>
    {
        public override ChessLayout Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartArray)
            {
                throw new JsonException();
            }

            // Initialize a new ChessLayout object
            ChessLayout layout = new ChessLayout();

            // Read the JSON array containing key-value pairs
            reader.Read(); // Move to the first token in the array
            while (reader.TokenType != JsonTokenType.EndArray)
            {
                if (reader.TokenType != JsonTokenType.StartArray)
                {
                    throw new JsonException();
                }

                // Read the key-value pair (coordinate and piece)
                reader.Read(); // Move to the first token in the inner array (coordinate)
                Coordinate coordinate = JsonSerializer.Deserialize<Coordinate>(ref reader, options);
                reader.Read(); // Move to the second token in the inner array (piece)
                APiece piece = JsonSerializer.Deserialize<APiece>(ref reader, options);

                // Add the key-value pair to the ChessLayout object
                layout.Add(coordinate, piece);

                // Move to the next token
                reader.Read(); // Move to the next token in the outer array
            }

            return layout;
        }

        public override void Write(Utf8JsonWriter writer, ChessLayout value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();

            // Write each key-value pair (coordinate and piece) in the ChessLayout object
            foreach (var kvp in value)
            {
                writer.WriteStartArray();
                JsonSerializer.Serialize(writer, kvp.Key, options); // Write the coordinate
                JsonSerializer.Serialize(writer, kvp.Value, options); // Write the piece
                writer.WriteEndArray();
            }

            writer.WriteEndArray();
        }
    }
}
