using System;

namespace Chess
{
    public class Referee
    {
        private Context context;
        public ALayout Layout { get; set; }

        public event EventHandler<GameContextChangedEventArgs> GameContextChanged;

        public Referee()
        {
        }

        public void Initialize(Board board)
        {
            context = new Context();
            board.MoveProposed += OnMoveProposed;
        }

        public void ValidateMove(Move move)
        {
            if (IsMoveValid(move.Source, move.Destination))
            {
                context.MakeMove(move.Source, move.Destination);
                OnGameContextChanged(new GameContextChangedEventArgs(context.Clone()));
            }
        }

        public bool IsMoveValid(Coordinate originalCell, Coordinate destinationCell)
        {
            if (Layout.ContainsKey(originalCell))
            {
                APiece piece = Layout[originalCell];
                if (piece != null)
                {
                    return piece.GetAvailableMoves(originalCell, Layout).Contains(destinationCell);
                }
            }
            return false;
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
