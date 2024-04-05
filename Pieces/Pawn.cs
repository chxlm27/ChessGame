using System.Drawing;

namespace Chess
{
    public class Pawn : APiece
    {
        private static readonly Rectangle BlackPawnRect = new Rectangle(298, 0, 64, 64);
        private static readonly Rectangle WhitePawnRect = new Rectangle(298, 59, 64, 64);

        public Pawn(PieceColors color) : base(color)
        {
        }

        public override void Move()
        {
            // Implement pawn movement logic
        }

        public override Image GetImage()
        {
            // Return the appropriate portion of the chess pieces array image based on the piece color
            if (Color == PieceColors.White)
            {
                return CropImage(ChessPiecesImage, WhitePawnRect);
            }
            else
            {
                return CropImage(ChessPiecesImage, BlackPawnRect);
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
