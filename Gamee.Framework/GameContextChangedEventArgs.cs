using System;

namespace Gamee.Framework
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
