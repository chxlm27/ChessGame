using System.Drawing;

namespace Chess
{
    public class Rook : APiece
    {
        private static readonly Rectangle WhiteRookRect = new Rectangle(118, 0, 64, 64);
        private static readonly Rectangle BlackRookRect = new Rectangle(118, 59, 64, 64);

        public Rook(PieceColors color) : base(color)
        {
        }


        public override Image GetImage()
        {
            // Return the appropriate portion of the chess pieces array image based on the piece color
            if (Color == PieceColors.White)
            {
                return CropImage(ChessPiecesImage, WhiteRookRect);
            }
            else
            {
                return CropImage(ChessPiecesImage, BlackRookRect);
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
