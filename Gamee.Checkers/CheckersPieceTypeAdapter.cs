using Gamee.Framework;

namespace Gamee.Checkers
{
    public class CheckersPieceTypeAdapter : IPieceType
    {
        public CheckersPieceType Type { get; private set; }

        public CheckersPieceTypeAdapter(CheckersPieceType type)
        {
            Type = type;
        }

        public int GetPieceIndex()
        {
            return (int)Type;  
        }
    }
}
