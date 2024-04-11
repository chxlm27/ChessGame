using System;

namespace Chess
{
    public class GameContextChangedEventArgs : EventArgs
    {
        public Context NewContext { get; }

        public GameContextChangedEventArgs(Context newContext)
        {
            NewContext = newContext;
        }
    }
}
