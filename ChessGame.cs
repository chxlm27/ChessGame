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
            referee = new Referee(); // Create the Referee object
            context = new Context();

            // No need to subscribe to the board's event here
            referee.GameContextChanged += OnGameContextChanged;

            board.Initialize();
            referee.Initialize(board); // Pass the board object to referee's Initialize method
        }

        private void OnGameContextChanged(object sender, GameContextChangedEventArgs e)
        {
            GameContextChanged?.Invoke(this, e);
        }
    }

}
