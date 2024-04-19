using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using Chess;

namespace ChessGame.Adapters
{
    public class ContextAdapter : AdapterBase<Context>
    {
        protected override byte[] Serialize(Context context)
        {
            // de citit despre reflection
            PropertyInfo currentPlayerProp = typeof(Context).GetProperty("CurrentPlayer", BindingFlags.NonPublic | BindingFlags.Instance);
            PropertyInfo layoutProp = typeof(Context).GetProperty("Layout", BindingFlags.NonPublic | BindingFlags.Instance);

            PieceColors currentPlayer = (PieceColors)currentPlayerProp.GetValue(context);
            ALayout layout = (ALayout)layoutProp.GetValue(context);

            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                formatter.Serialize(memoryStream, currentPlayer);
                formatter.Serialize(memoryStream, layout);
                return memoryStream.ToArray();
            }
        }

        protected override Context Deserialize(byte[] data)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream(data))
            {
                PieceColors currentPlayer = (PieceColors)formatter.Deserialize(memoryStream);
                ALayout layout = (ALayout)formatter.Deserialize(memoryStream);

                Context context = new Context();
                typeof(Context).GetProperty("CurrentPlayer", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(context, currentPlayer);
                typeof(Context).GetProperty("Layout", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(context, layout);

                return context;
            }
        }
    }
}
