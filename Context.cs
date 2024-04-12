using System;

namespace Chess
{
    public class Context
    {
        public PieceColors CurrentPlayer { get; private set; }
        public ALayout Layout { get; set; } // Property to hold the layout
        //trebuie sa pun layoutul aici, sa il mut din Board.
        // cand incepe jocul, Referee ul face primul new Context
        // Referee.Start() -> game context changed

        public Context()
        {
            // By default, set the current player to white
            CurrentPlayer = PieceColors.White;
        }

        public void SwitchPlayer()
        {
            // Switch the current player to the other player
            CurrentPlayer = (CurrentPlayer == PieceColors.White) ? PieceColors.Black : PieceColors.White;
        }
    }
}
