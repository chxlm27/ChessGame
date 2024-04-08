using System.Drawing;

namespace Chess
{
    public abstract class APiece
    {
        public PieceColors Color { get; }
        public PieceType Type { get; }
        protected APiece(PieceColors color, PieceType type)
        {
            Color = color;
            Type = type;
        }

        public abstract Image GetImage();
    }
}
