using Chess;
using System;

[Serializable]
public class CoordinateAdapter
{
    public int X { get; set; }
    public int Y { get; set; }

    public static explicit operator CoordinateAdapter(Coordinate coord)
    {
        return new CoordinateAdapter { X = coord.X, Y = coord.Y };
    }

    public static explicit operator Coordinate(CoordinateAdapter coordAdapter)
    {
        return Coordinate.GetInstance(coordAdapter.X, coordAdapter.Y);
    }
}
