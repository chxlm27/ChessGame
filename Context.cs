using System;

namespace Chess
{
    public class Context
    {
        public PieceColors CurrentPlayer { get; private set; }
        //trebuie sa pun layoutul aici, sa il mut din Board.
        // cand incepe jocul, referee ul face primul new Context
        // referee.Start() -> game context changed

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
