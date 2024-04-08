using System.Drawing;

namespace Chess
{
    public class Rook : APiece
    {
        private static Bitmap ChessPiecesBitmap = new Bitmap("ChessPiecesArray.png");
        private static readonly int NumColumns = 6; // Number of columns in the ChessPiecesArray.png

        public Rook(PieceColors color) : base(color)
        {
        }

        public override Image GetImage()
        {
            int pieceWidth = ChessPiecesBitmap.Width / NumColumns;
            int pieceHeight = ChessPiecesBitmap.Height / 2; // Dividing by 2 for black and white pieces

            // Calculate the x-coordinate of the piece in the bitmap
            int x = ((int)PieceType.Rook * pieceWidth);

            // Calculate the y-coordinate of the piece in the bitmap
            int y = (Color == PieceColors.White) ? 0 : pieceHeight;

            // Crop the piece from the bitmap
            Rectangle cropRect = new Rectangle(x, y, pieceWidth, pieceHeight);
            return CropImage(ChessPiecesBitmap, cropRect);
        }

        // Helper method to crop the image based on a specified rectangle
        private static Image CropImage(Image source, Rectangle cropRect)
        {
            Bitmap bmp = new Bitmap(cropRect.Width, cropRect.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.DrawImage(source, new Rectangle(0, 0, bmp.Width, bmp.Height), cropRect, GraphicsUnit.Pixel);
            }
            return bmp;
        }
    }
}
