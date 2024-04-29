using System;

namespace Gamee.Framework
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
            //Context.Layout = new ChessLayout();
/*            Context.Layout.Initialize();
            
            ALayout Layout;// new ChessLayout();
            Layout.Initialize();
            Context.Layout = Layout;*/
        }

        public void StartWith(Context context) 
        {
            Context = context; //.clone
            //ALayout Layout = new ChessLayout(); //!!!!
            //Layout.Initialize();
            //Context.Layout = Layout;
            ////lansat eveniment de game context changed, trebuie sa pun context clonat
        }

        public bool IsValid(Coordinate originalCell, Coordinate destinationCell)
        {
            if (Context != null && Context.Layout.ContainsKey(originalCell))
            {
                IPiece piece = Context.Layout[originalCell];  // Using IPiece here
                if (piece is APiece aPiece)  // Explicitly checking and casting to APiece
                {
                    return aPiece.GetAvailableMoves(originalCell, Context.Layout).Contains(destinationCell);
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
