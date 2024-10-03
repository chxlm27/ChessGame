using System;

namespace Gamee.Framework
{
    public class Referee
    {
        public Context Context { get; set; }

        public event EventHandler<GameContextChangedEventArgs> GameContextChanged;

        public Referee()
        {
        }

        public void Initialize()
        {
            Context = new Context();
        }

        public void Start(ALayout layout) 
        {
            SetLayout(layout); 

            if (Context.Layout == null)
            {
                throw new InvalidOperationException("Layout must be set before starting the game.");
            }

            Context.Layout.Initialize();
        }

        public void SetLayout(ALayout layout)
        {
            if (Context == null)
            {
                Initialize();
            }
            Context.Layout = layout;
        }


        public bool IsValid(Coordinate originalCell, Coordinate destinationCell)
        {
            if (Context != null && Context.Layout.ContainsKey(originalCell))
            {
                IPiece piece = Context.Layout[originalCell];
                if (piece is APiece aPiece)
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
                GameContextChangedEventArgs args = new GameContextChangedEventArgs(Context.Clone());
                OnGameContextChanged(args);
            }
        }


        private void OnGameContextChanged(GameContextChangedEventArgs e)
        {
            GameContextChanged?.Invoke(this, e);
        }
    }
}
