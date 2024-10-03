using Chess;
using System.Collections.Generic;
using System;

[Serializable]
public class BoardState
{
    public Context GameContext { get; set; }
    public Dictionary<CoordinateAdapter, APiece> Pieces { get; set; }

    // Optionally include other settings like game history, player turn, etc.
}
