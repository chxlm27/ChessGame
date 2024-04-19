using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using ChessGame.Adapters;

namespace Chess.Adapters
{
    public class BoardAdapter : AdapterBase<Board>
    {
        protected override byte[] Serialize(Board board)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Serialize each field using reflection
                PropertyInfo[] properties = typeof(Board).GetProperties(BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (PropertyInfo property in properties)
                {
                    formatter.Serialize(memoryStream, property.GetValue(board));
                }
                return memoryStream.ToArray();
            }
        }

        protected override Board Deserialize(byte[] data)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream(data))
            {
                Board board = new Board();
                // Deserialize each field using reflection
                PropertyInfo[] properties = typeof(Board).GetProperties(BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (PropertyInfo property in properties)
                {
                    object value = formatter.Deserialize(memoryStream);
                    property.SetValue(board, value);
                }
                return board;
            }
        }
    }
}
