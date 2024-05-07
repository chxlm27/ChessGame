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
            // Initialize the pieces for player one (typically uses black pieces)
            InitializePiecesForPlayer(PieceColors.Black, new int[] { 0, 1, 2 }); // First three rows from top

            // Initialize the pieces for player two (typically uses white pieces)
            InitializePiecesForPlayer(PieceColors.White, new int[] { 5, 6, 7 }); // Last three rows from bottom
        }

        private void InitializePiecesForPlayer(PieceColors color, int[] rows)
        {
            foreach (int row in rows)
            {
                // Pieces are placed on black squares which are the 1, 3, 5, 7 columns on even rows
                // and 0, 2, 4, 6 columns on odd rows
                for (int col = (row % 2 == 0 ? 1 : 0); col < 8; col += 2)
                {
                    this.Add(Coordinate.GetInstance(row, col), CheckersPieceFactory.CreatePiece(CheckersPieceType.Man, color));
                }
            }
        }
    }
}
