namespace Gamee.Framework
{
    public abstract class AGame
    {
        public abstract void Initialize(IBoard board, Referee referee);
        public abstract void SaveGame(string filePath);
        public abstract Context LoadGame(string filePath);
        public abstract void Start();
    }
}