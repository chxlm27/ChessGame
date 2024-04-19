using Chess;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ChessGame.Adapters
{
    public class ChessLayoutAdapter : AdapterBase<ChessLayout>
    {
        protected override byte[] Serialize(ChessLayout layout)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                formatter.Serialize(memoryStream, layout);
                return memoryStream.ToArray();
            }
        }

        protected override ChessLayout Deserialize(byte[] data)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream(data))
            {
                return (ChessLayout)formatter.Deserialize(memoryStream);
            }
        }
    }
}
