using Chess;
using System;

namespace Chess
{
    public abstract class AGame
    {
        public abstract void Initialize(Board board);
        public abstract void SaveGame(string filePath);
        public abstract Context LoadGame(string filePath);
    }
}