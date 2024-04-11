using System;
using System.Runtime.Remoting.Contexts;

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

            board.MoveProposed += OnMoveProposed; // OnMoveProposed MUST be in Referee
            referee.GameContextChanged += OnGameContextChanged;

            board.Initialize();
            referee.Initialize();
        }

        //GameContextChanged (listener should be in Referee)
        private void OnMoveProposed(object sender, MoveProposedEventArgs e)
        {
            // Propagate the move to the referee for validation
            referee.ValidateMove(e);
        }

        //This should be moved in Board.cs
        private void OnGameContextChanged(object sender, GameContextChangedEventArgs e)
        {
            GameContextChanged?.Invoke(this, e);
        }
    }
}
