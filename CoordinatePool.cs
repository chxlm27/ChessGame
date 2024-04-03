using System.Collections.Generic;

namespace Chess
{
    public class CoordinatePool
    {
        private readonly List<Coordinate> _coordinates;

        private CoordinatePool()
        {
            _coordinates = new List<Coordinate>();
            // Fill the pool with coordinates for an 8x8 chessboard
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    _coordinates.Add(new Coordinate(row, col));
                }
            }
        }

        // Get a coordinate from the pool
        public Coordinate GetFromPool(int row, int column)
        {
            // Find and return the coordinate with the specified row and column
            foreach (var coordinate in _coordinates)
            {
                if (coordinate.Row == row && coordinate.Column == column)
                {
                    _coordinates.Remove(coordinate); // Remove the coordinate from the pool
                    return coordinate;
                }
            }
            return null; // Coordinate not found
        }
    }
}
