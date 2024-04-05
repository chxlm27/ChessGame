using System.Drawing;

namespace Chess
{
    public class King : APiece
    {
        private static readonly Rectangle WhiteKingRect = new Rectangle(57, 0, 64, 64);
        private static readonly Rectangle BlackKingRect = new Rectangle(57, 59, 64, 64);

        public King(PieceColors color) : base(color)
        {
        }

        public override void Move()
        {
            // Implement king movement logic
        }

        public override Image GetImage()
        {
            // Return the appropriate portion of the chess pieces array image based on the piece color
            if (Color == PieceColors.White)
            {
                return CropImage(ChessPiecesImage, WhiteKingRect);
            }
            else
            {
                return CropImage(ChessPiecesImage, BlackKingRect);
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
