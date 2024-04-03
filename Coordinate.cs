using System.Collections.Generic;

public class Coordinate
{
    private static readonly Dictionary<string, Coordinate> _instances = new Dictionary<string, Coordinate>();

    public int X { get; }
    public int Y { get; }

    private Coordinate(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static Coordinate GetInstance(int x, int y)
    {
        string key = $"{x},{y}";


        if (!_instances.ContainsKey(key))
        {
            _instances[key] = new Coordinate(x, y);
        }
        return _instances[key];
    }
}
