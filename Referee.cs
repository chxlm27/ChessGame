using System;

namespace Chess
{
    public class Referee
    {
        private Context context;

        public event EventHandler<GameContextChangedEventArgs> GameContextChanged;

        public Referee()
        {
            context = new Context();
        }

        public void Initialize()
        {
            // Perform any necessary initialization
        }

        public void ValidateMove(MoveProposedEventArgs move)
        {
            // Implement move validation logic here
            // This method will determine if the proposed move is valid
            // and if so, update the game context accordingly
            // Then, it will raise the GameContextChanged event
        }

        private void OnGameContextChanged(GameContextChangedEventArgs e)
        {
            // Raise the GameContextChanged event
            GameContextChanged?.Invoke(this, e);
        }
    }
}
