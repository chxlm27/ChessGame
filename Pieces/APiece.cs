using System.Collections.Generic;
using System.Drawing;

namespace Chess
{
    public abstract class APiece
    {
        private static Bitmap ChessPiecesBitmap = new Bitmap("ChessPiecesArray.png");
        private static readonly int NumberOfPieceTypes = 6;
        private Dictionary<(PieceType, PieceColors), Bitmap> PieceImages = new Dictionary<(PieceType, PieceColors), Bitmap>();
        public abstract List<Coordinate> GetAvailableMoves(Coordinate source, ALayout layout);
        public Point Position { get; set; }

        public PieceColors Color { get; }
        public PieceType Type { get; }

        protected APiece(PieceColors color, PieceType type)
        {
            Color = color;
            Type = type;
        }

        public Image GetImage()
        {
            if (!PieceImages.ContainsKey((Type, Color)))
            {
                int pieceWidth = ChessPiecesBitmap.Width / NumberOfPieceTypes;
                int pieceHeight = ChessPiecesBitmap.Height / 2;
                int x = ((int)Type * pieceWidth);
                int y = (Color == PieceColors.White) ? 0 : pieceHeight;

                PieceImages[(Type, Color)] = new Bitmap(pieceWidth, pieceHeight);
                Graphics.FromImage(PieceImages[(Type, Color)]).DrawImage(ChessPiecesBitmap, new Rectangle(0, 0, pieceWidth, pieceWidth), x, y, pieceWidth, pieceHeight, GraphicsUnit.Pixel);
            }
            return PieceImages[(Type, Color)];
        }
    }
}
