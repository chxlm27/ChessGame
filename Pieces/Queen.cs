using System.Drawing;

namespace Chess
{
    public class Queen : APiece
    {
        private static readonly Rectangle WhiteQueenRect = new Rectangle(-2, 0, 64, 64);
        private static readonly Rectangle BlackQueenRect = new Rectangle(-2, 59, 64, 64);

        public Queen(PieceColors color) : base(color)
        {
        }

        public override Image GetImage()
        {
            // Return the appropriate portion of the chess pieces array image based on the piece color
            if (Color == PieceColors.White)
            {
                return CropImage(ChessPiecesImage, WhiteQueenRect);
            }
            else
            {
                return CropImage(ChessPiecesImage, BlackQueenRect);
            }
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
