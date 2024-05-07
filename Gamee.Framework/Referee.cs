using System;

namespace Gamee.Framework
{
    public class Referee
    {
        public Context GameContext { get; set; }

        public event EventHandler<GameContextChangedEventArgs> GameContextChanged;

        public Referee()
        {
            GameContext = new Context();
        }

        public void Initialize()
        {
            // Ensure the context is clean or properly reset, without setting Layout here
        }

        public void Start()
        {
            if (GameContext.Layout == null)
            {
                throw new InvalidOperationException("Layout must be set before starting the game.");
            }
            // Initialize Layout if needed or other start logic
            GameContext.Layout.Initialize();
        }

        public void SetLayout(ALayout layout)
        {
            GameContext.Layout = layout;
        }

        public bool IsValid(Coordinate originalCell, Coordinate destinationCell)
        {
            if (GameContext != null && GameContext.Layout.ContainsKey(originalCell))
            {
                IPiece piece = GameContext.Layout[originalCell];
                if (piece is APiece aPiece)
                {
                    return aPiece.GetAvailableMoves(originalCell, GameContext.Layout).Contains(destinationCell);
                }
            }
            return false;
        }

        public void OnMoveProposed(object sender, MoveProposedEventArgs e)
        {
            if (GameContext != null && IsValid(e.ProposedMove.Source, e.ProposedMove.Destination))
            {
                GameContext.Move(e.ProposedMove.Source, e.ProposedMove.Destination);
                OnGameContextChanged(new GameContextChangedEventArgs(GameContext.Clone()));
            }
        }

        private void OnGameContextChanged(GameContextChangedEventArgs e)
        {
            GameContextChanged?.Invoke(this, e);
        }
    }
}
