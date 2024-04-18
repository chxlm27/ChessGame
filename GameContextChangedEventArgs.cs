using System;

namespace Chess
{
    [Serializable]
    public class GameContextChangedEventArgs : EventArgs
    {
        public Context Context { get; }

        public GameContextChangedEventArgs(Context context)
        {
            Context = context;
        }
    }
}
