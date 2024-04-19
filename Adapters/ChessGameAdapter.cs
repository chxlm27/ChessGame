using Chess;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;

namespace ChessGame.Adapters
{
    public class ChessGameAdapter : AdapterBase<Chess.ChessGame>
    {
        protected override byte[] Serialize(Chess.ChessGame game)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                formatter.Serialize(memoryStream, game);

                // Access private fields using reflection
                FieldInfo boardField = typeof(Chess.ChessGame).GetField("board", BindingFlags.NonPublic | BindingFlags.Instance);
                FieldInfo refereeField = typeof(Chess.ChessGame).GetField("referee", BindingFlags.NonPublic | BindingFlags.Instance);

                Board board = (Board)boardField.GetValue(game);
                Referee referee = (Referee)refereeField.GetValue(game);

                formatter.Serialize(memoryStream, board);
                formatter.Serialize(memoryStream, referee);

                return memoryStream.ToArray();
            }
        }

        protected override Chess.ChessGame Deserialize(byte[] data)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream(data))
            {
                Chess.ChessGame game = (Chess.ChessGame)formatter.Deserialize(memoryStream);

                // Access private fields using reflection
                FieldInfo boardField = typeof(Chess.ChessGame).GetField("board", BindingFlags.NonPublic | BindingFlags.Instance);
                FieldInfo refereeField = typeof(Chess.ChessGame).GetField("referee", BindingFlags.NonPublic | BindingFlags.Instance);

                Board board = (Board)formatter.Deserialize(memoryStream);
                Referee referee = (Referee)formatter.Deserialize(memoryStream);

                boardField.SetValue(game, board);
                refereeField.SetValue(game, referee);

                return game;
            }
        }
    }
}
