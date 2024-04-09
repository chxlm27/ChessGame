using System.Drawing;

namespace Chess
{
    public class King : APiece
    {
        public King(PieceColors color) : base(color, PieceType.King)
        {
        }

        protected override void LoadPieceImage()
        {
            int pieceWidth = ChessPiecesBitmap.Width / NumColumns;
            int pieceHeight = ChessPiecesBitmap.Height / 2;

            int x = ((int)Type * pieceWidth);
            int y = (Color == PieceColors.White) ? 0 : pieceHeight;

            Rectangle cropRect = new Rectangle(x, y, pieceWidth, pieceHeight);
            PieceImages[(Type, Color)] = CropImage(cropRect);
        }
    }
}
