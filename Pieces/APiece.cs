using System.Collections.Generic;
using System.Drawing;

namespace Chess
{
    public abstract class APiece
    {
        protected static Bitmap ChessPiecesBitmap = new Bitmap("ChessPiecesArray.png");
        protected static readonly int NumColumns = 6;
        protected Dictionary<(PieceType, PieceColors), Bitmap> PieceImages = new Dictionary<(PieceType, PieceColors), Bitmap>();

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
                LoadPieceImage();
            }
            return PieceImages[(Type, Color)];
        }

        protected abstract void LoadPieceImage();

        protected Bitmap CropImage(Rectangle cropRect)
        {
            Bitmap bmp = new Bitmap(cropRect.Width, cropRect.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.DrawImage(ChessPiecesBitmap, new Rectangle(0, 0, bmp.Width, bmp.Height), cropRect, GraphicsUnit.Pixel);
            }
            return bmp;
        }
    }
}
