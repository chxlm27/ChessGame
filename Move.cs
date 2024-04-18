using System;

namespace Chess
{
    public class Move
    {
        public Coordinate Source { get; }
        public Coordinate Destination { get; }

        public Move(Coordinate source, Coordinate destination)
        {
            Source = source;
            Destination = destination;
        }
    }
}
