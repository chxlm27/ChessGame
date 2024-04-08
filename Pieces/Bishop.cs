using System.Drawing;

namespace Chess
{
    public class Bishop : APiece
    {
        private static Bitmap ChessPiecesBitmap = new Bitmap("ChessPiecesArray.png");
        private static readonly int NumColumns = 6;
        private Bitmap BufferedImage;

        public Bishop(PieceColors color) : base(color, PieceType.Bishop)
        {
        }

        public override Image GetImage()
        {
            if (BufferedImage == null)
            {
                int pieceWidth = ChessPiecesBitmap.Width / NumColumns;
                int pieceHeight = ChessPiecesBitmap.Height / 2;

                int x = ((int)Type * pieceWidth);
                int y = (Color == PieceColors.White) ? 0 : pieceHeight;

                Rectangle cropRect = new Rectangle(x, y, pieceWidth, pieceHeight);
                BufferedImage = CropImage(ChessPiecesBitmap, cropRect); //intr o var
            }
            return BufferedImage;
        }

        private Bitmap CropImage(Image source, Rectangle cropRect)
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
