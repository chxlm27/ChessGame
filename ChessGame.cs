using System;

namespace Chess
{
    [Serializable]
    public class ChessGame
    {
        private Board board;
        private Referee referee;

        public event EventHandler<GameContextChangedEventArgs> GameContextChanged;

        public ChessGame()
        {
        }

        public void Initialize(Board board)
        {
            this.board = board;
            referee = new Referee();

            referee.GameContextChanged += board.OnGameContextChanged;

            board.Initialize();
            referee.Initialize(board);
        }

        public Board GetBoard()
        {
            return board;
        }

        public void SaveGame()
        {
        }

        public void LoadGame()
        {
        }
        private void OnRefereeGameContextChanged(object sender, GameContextChangedEventArgs e)
        {
            GameContextChanged?.Invoke(this, e);
        }
    }
}
