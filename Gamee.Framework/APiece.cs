using System.Collections.Generic;
using System.Drawing;

namespace Gamee.Framework
{
    public abstract class APiece : IPiece
    {
        private static Bitmap ChessPiecesBitmap = new Bitmap("ChessPiecesArray.png");
        private static readonly int NumberOfPieceTypes = 6;

        private Dictionary<(IPieceType, PieceColors), Bitmap> PieceImages = new Dictionary<(IPieceType, PieceColors), Bitmap>();
        public abstract List<Coordinate> GetAvailableMoves(Coordinate source, ALayout layout);
        public PieceColors Color { get; }
        public IPieceType Type { get; }

        protected APiece(PieceColors color, IPieceType type)
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
                int x = (Type.GetPieceIndex() * pieceWidth);
                int y = (Color == PieceColors.White) ? 0 : pieceHeight;

                PieceImages[(Type, Color)] = new Bitmap(pieceWidth, pieceHeight);
                Graphics.FromImage(PieceImages[(Type, Color)]).DrawImage(ChessPiecesBitmap, new Rectangle(0, 0, pieceWidth, pieceHeight), x, y, pieceWidth, pieceHeight, GraphicsUnit.Pixel);
            }
            return PieceImages[(Type, Color)];
        }
    }
}