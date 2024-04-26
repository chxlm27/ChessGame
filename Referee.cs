using System;
using System.Windows.Forms;

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
            Context = null;
        }

        public void Start()
        {
            Context = new Context();
            if (Context == null)
            {
                Context = new Context();
                Context.Layout = new ChessLayout();
                Context.Layout.Initialize();
            }
            ALayout Layout = new ChessLayout();
            Layout.Initialize();
            Context.Layout = Layout;
        }
        public void StartWith(Context context)
        {
            Context = context;
            ALayout Layout = new ChessLayout();
            Layout.Initialize();
            Context.Layout = Layout;
        }

        public bool IsValid(Coordinate originalCell, Coordinate destinationCell)
        {
            if (Context != null && Context.Layout.ContainsKey(originalCell))
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
            if (Context != null && IsValid(e.ProposedMove.Source, e.ProposedMove.Destination))
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
