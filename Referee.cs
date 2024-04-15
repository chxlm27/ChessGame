using System;

namespace Chess
{
    public class Referee
    {
        private Context context;
        private Board board; // Reference to the board

        public event EventHandler<GameContextChangedEventArgs> GameContextChanged;

        public Referee()
        {
        }

        public void Initialize(Board board)
        {
            this.board = board; // Initialize the board reference
            context = new Context();
            board.MoveProposed += OnMoveProposed;
        }

        public void ValidateMove(Move move)
        {
            if (context.IsMoveValid(move.Source, move.Destination))
            {
                context.MakeMove(move.Source, move.Destination);
                OnGameContextChanged(new GameContextChangedEventArgs(context.Clone()));
            }
            // If the move is not valid, you may handle this case as needed
        }

        private void OnMoveProposed(object sender, MoveProposedEventArgs e)
        {
            ValidateMove(e.ProposedMove);
        }

        private void OnGameContextChanged(GameContextChangedEventArgs e)
        {
            GameContextChanged?.Invoke(this, e);
        }
    }

}
