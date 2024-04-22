using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ChessGame.Adapters;

namespace Chess.Adapters
{
    public class CoordinateAdapter
    {
        public byte[] Serialize(Coordinate coordinate)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                formatter.Serialize(memoryStream, coordinate.X);
                formatter.Serialize(memoryStream, coordinate.Y);
                return memoryStream.ToArray();
            }
        }

        public Coordinate Deserialize(byte[] data)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream(data))
            {
                int x = (int)formatter.Deserialize(memoryStream);
                int y = (int)formatter.Deserialize(memoryStream);
                return Coordinate.GetInstance(x, y);
            }
        }
    }
}
