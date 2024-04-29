using System.Collections.Generic;

namespace Gamee.Framework
{
    public abstract class ALayout : Dictionary<Coordinate, IPiece>
    {
        public abstract void Initialize();
        public abstract ALayout Clone();
    }
}
