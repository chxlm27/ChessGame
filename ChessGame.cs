using Chess;

namespace ChessGame
{
    public class ChessGame : AGame
    {
        private Board _board;

        public override void Initialize(Board board)
        {
            _board = board;
            // Additional initialization code if needed
        }

        public override void Start()
        {
            // Start the chess game logic
            // For example, initializing game state, setting up players, etc.
        }
    }
}
