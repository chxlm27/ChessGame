using System;

namespace Chess
{
    public class ChessGame
    {
        private Board board;
        private Referee referee;
        private Context context;

        public event EventHandler<GameContextChangedEventArgs> GameContextChanged;

        public ChessGame()
        {
            Initialize();
        }

        private void Initialize()
        {
            board = new Board();
            referee = new Referee();
            context = new Context();

            // Initialize the layout and set it in the context
            context.Layout = new ChessLayout();
            context.Layout.Initialize();

            referee.GameContextChanged += OnGameContextChanged;

            board.Initialize(); // Initialize the board
            referee.Initialize(board);
        }

        private void OnGameContextChanged(object sender, GameContextChangedEventArgs e)
        {
            GameContextChanged?.Invoke(this, e);
        }
    }
}
