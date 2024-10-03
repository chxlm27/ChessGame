using Gamee.Framework;

namespace Gamee.Checkers
{
    public class CheckersLayout : ALayout
    {
        public CheckersLayout() : base()
        {
        }

        public override ALayout Clone()
        {
            CheckersLayout clone = new CheckersLayout();
            foreach (var kvp in this)
            {
                clone.Add(kvp.Key, kvp.Value);
            }
            return clone;
        }

        public override void Initialize()
        {
            // Initialize Black pieces (top three rows)
            for (int y = 0; y < 3; y++)
            {
                for (int x = (y % 2 == 0) ? 1 : 0; x < 8; x += 2) // Start on 1 for even rows, 0 for odd rows
                {
                    this.Add(Coordinate.GetInstance(y, x), CheckersPieceFactory.CreatePiece(CheckersPieceType.Man, PieceColors.Black));
                }
            }

            // Initialize White pieces (bottom three rows)
            for (int y = 5; y < 8; y++)
            {
                for (int x = (y % 2 == 0) ? 1 : 0; x < 8; x += 2) // Start on 1 for even rows, 0 for odd rows
                {
                    this.Add(Coordinate.GetInstance(y, x), CheckersPieceFactory.CreatePiece(CheckersPieceType.Man, PieceColors.White));
                }
            }
        }
    }
}
