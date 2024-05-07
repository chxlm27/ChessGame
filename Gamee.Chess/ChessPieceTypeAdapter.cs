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
            return (int)Type;  // Assuming ChessPieceType enum's values are set up as indexes
        }
    }
}
