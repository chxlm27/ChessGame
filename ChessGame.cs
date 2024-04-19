using System;

namespace Chess
{
    [Serializable]
    public class ChessGame : AGame
    {
        private Board board;
        private Referee referee;

        public event EventHandler<GameContextChangedEventArgs> GameContextChanged;

        public ChessGame()
        {
        }

        public override void Initialize(Board _board)
        {
            board = _board;
            referee = new Referee();

            board.Initialize();
            referee.Initialize();

            board.MoveProposed += referee.OnMoveProposed;
            referee.GameContextChanged += board.OnGameContextChanged;

        }


        public override void Save()
        {
        }

        public override void Load()
        {
        }

        public override void Start()
        {
        }

        private void OnRefereeGameContextChanged(object sender, GameContextChangedEventArgs e)
        {
            GameContextChanged?.Invoke(this, e);
        }
    }
}
