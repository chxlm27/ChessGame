using Gamee.Framework;

namespace Gamee.Chess
{
    public class ChessPieceTypeAdapter : IPieceType
    {
        public ChessPieceType Type { get; private set; }

        public ChessPieceTypeAdapter(ChessPieceType type)
        {
            Type = type;
        }

        public int GetPieceIndex()
        {
            return (int)Type; 
        }
    }
}
