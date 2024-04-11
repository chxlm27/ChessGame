using System.Collections.Generic;
using System.Drawing.Text;
namespace Chess
{
    public class Coordinate
    {
        private static Dictionary<int, Dictionary<int, Coordinate>> _instances;

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
                _instances = new Dictionary<int, Dictionary<int, Coordinate>>();
            }

            if (!_instances.ContainsKey(x))
            {
                _instances[x] = new Dictionary<int, Coordinate>();
            }

            if (!_instances[x].ContainsKey(y))
            {
                _instances[x][y] = new Coordinate(x, y);
            }

            return _instances[x][y];
        }
    }
}