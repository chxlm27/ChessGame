namespace Game.Framework
{
    public class GameContextChangedEventArgs : EventArgs
    {
        public Context NewContext { get; private set; }

        public GameContextChangedEventArgs(Context newContext)
        {
            NewContext = newContext;
        }
    }

}
