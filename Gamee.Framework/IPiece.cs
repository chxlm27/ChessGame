using System.Collections.Generic;
using System.Drawing;

namespace Gamee.Framework
{
    public interface IPiece
    {
        List<Coordinate> GetAvailableMoves(Coordinate source, ALayout layout);
        PieceColors Color { get; }
        IPieceType Type { get; } 
        Image GetImage();
    }
}
