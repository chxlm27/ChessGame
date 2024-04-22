using Chess;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ChessGame.Adapters
{
    public class RefereeAdapter
    {
        public byte[] Serialize(Referee referee)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                formatter.Serialize(memoryStream, referee);
                return memoryStream.ToArray();
            }
        }

        public Referee Deserialize(byte[] data)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream(data))
            {
                return (Referee)formatter.Deserialize(memoryStream);
            }
        }
    }
}
