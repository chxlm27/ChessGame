using System.IO;


namespace ChessGame.Adapters
{
    public abstract class AdapterBase<T>
    {
        protected abstract byte[] Serialize(T obj);
        protected abstract T Deserialize(byte[] data);

        public void SaveToFile(T obj, string filePath)
        {
            byte[] data = Serialize(obj);
            File.WriteAllBytes(filePath, data);
        }

        public T LoadFromFile(string filePath)
        {
            byte[] data = File.ReadAllBytes(filePath);
            return Deserialize(data);
        }
    }
}
