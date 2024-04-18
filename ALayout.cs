using System;
using System.Collections.Generic;

namespace Chess
{
    [Serializable]
    public abstract class ALayout : Dictionary<Coordinate, APiece>
    {
        public abstract void Initialize();
        public abstract ALayout Clone();
    }
}
// Metoda Move(un move) trebuie aici - implementata aici 