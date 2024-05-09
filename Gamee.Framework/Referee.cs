using System;

namespace Gamee.Framework
{
    public class Referee
    {
        public Context Context { get; set; }

        public event EventHandler<GameContextChangedEventArgs> GameContextChanged;

        public Referee()
        {
            // Constructor remains empty as per your instruction
        }

        public void Initialize()
        {
            // Ensure the context is clean or properly reset
            Context = new Context();  // Context is initialized here if needed
        }

        public void Start(ALayout layout) // Now taking a layout parameter
        {
            SetLayout(layout);  // Set the layout passed as parameter

            if (Context.Layout == null)
            {
                throw new InvalidOperationException("Layout must be set before starting the game.");
            }

            // Initialize Layout if needed or other start logic
            Context.Layout.Initialize();
        }

        public void SetLayout(ALayout layout)
        {
            if (Context == null)
            {
                Initialize(); // Ensure the context is initialized
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
