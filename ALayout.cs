using System;
using System.Collections.Generic;

namespace Chess
{
    public abstract class ALayout : Dictionary<Coordinate, APiece>
    {
        public abstract void InitializeLayout();
    }
}
