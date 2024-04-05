using System.Drawing;

namespace Chess
{
    public class Bishop : APiece
    {
        private static readonly Rectangle WhiteBishopRect = new Rectangle(237, 0, 64, 64);
        private static readonly Rectangle BlackBishopRect = new Rectangle(237, 59, 64, 64);

        public Bishop(PieceColors color) : base(color)
        {
        }

        public override void Move()
        {
            // Implement bishop movement logic
        }

        public override Image GetImage()
        {
            // Return the appropriate portion of the chess pieces array image based on the piece color
            if (Color == PieceColors.White)
            {
                return CropImage(ChessPiecesImage, WhiteBishopRect);
            }
            else
            {
                return CropImage(ChessPiecesImage, BlackBishopRect);
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
