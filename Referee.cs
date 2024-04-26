using System;

namespace Chess
{
    public class Referee
    {
        private Context Context { get; set; }

        public event EventHandler<GameContextChangedEventArgs> GameContextChanged;

        public Referee()
        {
        }

        public void Initialize()
        {
        }

        public void Start()
        {
            if (Context == null)
            {
                Context = new Context();
                ALayout layout = new ChessLayout();
                layout.Initialize();
                Context.Layout = layout;
                OnGameContextChanged(new GameContextChangedEventArgs(Context.Clone()));
            }
            Context.CurrentPlayer = PieceColors.Black; // Default starting player
        }

        public void StartWith(Context loadedContext)
        {
            Context = loadedContext;
            OnGameContextChanged(new GameContextChangedEventArgs(Context.Clone()));
        }


        public bool IsValid(Coordinate originalCell, Coordinate destinationCell)
        {
            if (Context.Layout.ContainsKey(originalCell))
            {
                APiece piece = Context.Layout[originalCell];
                if (piece != null)
                {
                    return piece.GetAvailableMoves(originalCell, Context.Layout).Contains(destinationCell);
                }
            }
            return false;
        }

        public void OnMoveProposed(object sender, MoveProposedEventArgs e)
        {
            if (IsValid(e.ProposedMove.Source, e.ProposedMove.Destination))
            {
                Context.Move(e.ProposedMove.Source, e.ProposedMove.Destination);
                OnGameContextChanged(new GameContextChangedEventArgs(Context.Clone()));
            }
        }

        private void OnGameContextChanged(GameContextChangedEventArgs e)
        {
            GameContextChanged?.Invoke(this, e);
        }
    }
}
