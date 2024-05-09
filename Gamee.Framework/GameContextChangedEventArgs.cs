using Gamee.Framework;
using System;

public class GameContextChangedEventArgs : EventArgs
{
    public Context NewContext { get; }

    public GameContextChangedEventArgs(Context newContext)
    {
        NewContext = newContext;
    }
}
