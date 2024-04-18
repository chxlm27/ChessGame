using Chess;
using System;

namespace Chess
{
    [Serializable]
    public abstract class AGame
    {
        public abstract void Initialize(Board board);
        public abstract void Start();
    }
}
//save load etc