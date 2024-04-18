using Chess;
using System;

namespace Chess
{
    [Serializable]
    public class Context
    {

        public PieceColors CurrentPlayer { get; set; }
        public ALayout Layout { get; set; }

        public Context()
        {
            CurrentPlayer = PieceColors.White;
            Layout = new ChessLayout(); // Initialize the layout
            Layout.Initialize();
        }

        public Context Clone()
        {
            // Implement deep clone logic here if necessary
            return new Context
            {
                CurrentPlayer = this.CurrentPlayer,
                Layout = this.Layout.Clone() // Clone the layout
            };
        }

        public void SwitchPlayer()
        {
            CurrentPlayer = (CurrentPlayer == PieceColors.White) ? PieceColors.Black : PieceColors.White;
        }

        public void MakeMove(Coordinate originalCell, Coordinate destinationCell)
        {
            if (Layout.ContainsKey(originalCell))
            {
                APiece piece = Layout[originalCell];
                if (piece != null)
                {
                    Layout.Remove(originalCell);
                    Layout.Add(destinationCell, piece);
                }
            }
        }
    }
}