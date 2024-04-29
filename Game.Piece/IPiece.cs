using System.Collections.Generic;
using System.Drawing;

namespace Game.Framework
{
    public interface IPiece
    {
        List<Coordinate> GetAvailableMoves(Coordinate source, ALayout layout);
        PieceColors Color { get; }
        IPieceType Type { get; }  // Changed from 'int' to 'IPieceType'
        Image GetImage();
    }
}
