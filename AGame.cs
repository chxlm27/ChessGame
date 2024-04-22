using Chess;
using System;

namespace Chess
{
    public abstract class AGame
    {
        public abstract void Initialize(Board board);
        public abstract void Save();
        public abstract void Load();
        public abstract void Start();
    }
}