using System.Collections.Generic;

public class Coordinate
{
    private static Dictionary<string, Coordinate> _instances;

    public int X { get; }
    public int Y { get; }

    private Coordinate(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static Coordinate GetInstance(int x, int y)
    {
        if (_instances == null)
        {
            _instances = new Dictionary<string, Coordinate>();
        }

        string key = $"{x},{y}";

        if (!_instances.ContainsKey(key))
        {
            _instances[key] = new Coordinate(x, y);
        }
        return _instances[key];
    }
}
