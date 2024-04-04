using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Chess
{
    public class Board : UserControl
    {
        public int CellSize { get; private set; }
        private float _pieceScale = 0.8f; // Determines the piece size relative to the cell size, 80% in this case

        public Dictionary<Coordinate, APiece> BoardState { get; private set; }
        private Image _chessPiecesImage;

        public Board()
        {
            BoardState = new Dictionary<Coordinate, APiece>();
            _chessPiecesImage = Image.FromFile("ChessPiecesArray.png");
        }

        public void Rescale(int windowWidth, int windowHeight)
        {
            int width = windowWidth - 16;
            int height = windowHeight - 39;

            CellSize = Math.Min(width, height) / 8;
            this.SetBounds((width < height ? 0 : (width - height) / 2),
                           (width < height ? (height - width) / 2 : 0),
                           CellSize * 8, CellSize * 8);
            this.Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap doubleBufferingImage = new Bitmap(CellSize * 8, CellSize * 8);
            Graphics doubleBufferingGraphics = Graphics.FromImage(doubleBufferingImage);

            DrawSquares(doubleBufferingGraphics);
            DrawLayout(doubleBufferingGraphics);
            e.Graphics.DrawImage(doubleBufferingImage, 0, 0);
        }

        private void DrawSquares(Graphics g)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Brush brush = ((i + j) % 2 == 0) ? Brushes.LightYellow : Brushes.SaddleBrown;
                    g.FillRectangle(brush, CellSize * j, CellSize * i, CellSize, CellSize);
                }
            }
        }

        private void DrawLayout(Graphics g)
        {
            // PieceSize is now dynamic, calculated as a percentage of CellSize
            int pieceSize = (int)(CellSize * _pieceScale);
            int[] pieceOrder = { 2, 3, 4, 1, 0, 4, 3, 2 }; // Index for Rook, Knight, Bishop, Queen, King, Bishop, Knight, Rook
            int imageHeight = _chessPiecesImage.Height / 2; // Assuming two rows of pieces, one for each color.

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int pieceImageIndex = -1;
                    bool isBlack = i < 2;
                    int imageYOffset = isBlack ? 0 : imageHeight; // Y Offset to select the correct color pieces from the image

                    if (i == 0 || i == 7)
                    {
                        pieceImageIndex = pieceOrder[j];
                    }
                    else if (i == 1 || i == 6)
                    {
                        pieceImageIndex = 5; // Index of Pawn in the image
                    }

                    if (pieceImageIndex != -1)
                    {
                        int imageXOffset = pieceImageIndex * imageHeight;
                        Rectangle sourceRectangle = new Rectangle(imageXOffset, imageYOffset, imageHeight, imageHeight);

                        // Centering the piece in the cell
                        int xOffset = (CellSize - pieceSize) / 2;
                        int yOffset = (CellSize - pieceSize) / 2;
                        Rectangle destRectangle = new Rectangle(j * CellSize + xOffset, i * CellSize + yOffset, pieceSize, pieceSize);

                        g.DrawImage(_chessPiecesImage, destRectangle, sourceRectangle, GraphicsUnit.Pixel);
                    }
                }
            }
        }
    }
}