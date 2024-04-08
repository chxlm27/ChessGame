using System.Drawing;

namespace Chess
{
    public abstract class APiece
    {
        protected static Image ChessPiecesImage = Image.FromFile("ChessPiecesArray.png"); 

        public PieceColors Color { get; }

        protected APiece(PieceColors color)
        {
            Color = color;
        }

        public abstract Image GetImage();
    }
}
