using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Chess
{
    public class APieceConverter : JsonConverter<APiece>
    {
        public override APiece Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            using (JsonDocument document = JsonDocument.ParseValue(ref reader))
            {
                JsonElement root = document.RootElement;
                PieceColors color = (PieceColors)Enum.Parse(typeof(PieceColors), root.GetProperty("Color").GetString());
                PieceType type = (PieceType)Enum.Parse(typeof(PieceType), root.GetProperty("Type").GetString());

                APiece piece = null;
                switch (type)
                {
                    case PieceType.Pawn:
                        piece = new Pawn(color);
                        break;
                    case PieceType.Rook:
                        piece = new Rook(color);
                        break;
                    case PieceType.Knight:
                        piece = new Knight(color);
                        break;
                    case PieceType.Bishop:
                        piece = new Bishop(color);
                        break;
                    case PieceType.Queen:
                        piece = new Queen(color);
                        break;
                    case PieceType.King:
                        piece = new King(color);
                        break;
                    default:
                        throw new ArgumentException($"Unknown piece type: {type}");
                }

                return piece;
            }
        }

        public override void Write(Utf8JsonWriter writer, APiece value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("Color", value.Color.ToString());
            writer.WriteString("Type", value.Type.ToString());
            writer.WriteEndObject();
        }
    }
}
