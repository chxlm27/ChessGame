using System.Drawing;

namespace Chess
{
    public class PieceExtractor
    {
        private readonly Image _chessPiecesImage;
        private readonly CoordinatePool coordinatePool;
        private const int PieceWidth = 64; // Width of each piece in the image
        private const int PieceHeight = 64; // Height of each piece in the image

        public PieceExtractor(string imagePath, CoordinatePool coordinatePool)
        {
            _chessPiecesImage = Image.FromFile(imagePath);
            this.coordinatePool = coordinatePool;
        }

        public Bitmap ExtractPiece(int column, int row)
        {
            int x = column * PieceWidth;
            int y = row * PieceHeight;

            Bitmap pieceBitmap = new Bitmap(PieceWidth, PieceHeight);
            using (Graphics g = Graphics.FromImage(pieceBitmap))
            {
                g.DrawImage(_chessPiecesImage, new Rectangle(0, 0, PieceWidth, PieceHeight), x, y, PieceWidth, PieceHeight, GraphicsUnit.Pixel);
            }

            return pieceBitmap;
        }
    }
}
