using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Chess
{
    public class Board : UserControl
    {
        public int CellSize { get; private set; }
        public Dictionary<Coordinate, APiece> BoardState { get; private set; } // Added property to represent the state of the chessboard

        public Board()
        {
            BoardState = new Dictionary<Coordinate, APiece>();
        }

        public void Rescale(int windowWidth, int windowHeight)
        {
            int width = windowWidth - 16;
            int height = windowHeight - 39;

            CellSize = Math.Min(width, height) / 8;

            this.SetBounds((width < height ? 0 : (width - height) / 2), (width < height ? (height - width) / 2 : 0), CellSize * 8, CellSize * 8);
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
                    if ((i % 2 + j % 2) % 2 == 0)
                        g.FillRectangle(System.Drawing.Brushes.LightYellow, CellSize * j, CellSize * i, CellSize, CellSize);
                    else
                        g.FillRectangle(System.Drawing.Brushes.SaddleBrown, CellSize * j, CellSize * i, CellSize, CellSize);
                }
            }
        }
        private void DrawLayout(Graphics g)
        {
/*            // Replace "YourImageFilePath" with the actual path to your image file
            Image image = Image.FromFile("ChessPiecesArray.png");
            int imageSize = CellSize * 8;

            // Draw the image
            g.DrawImage(image, 0, 0, imageSize, imageSize);*/
        }

/*        private void DrawLayout(Graphics g)
        {
            foreach (var kvp in BoardState)
            {
                Coordinate coordinate = kvp.Key;
                APiece piece = kvp.Value;
                // Draw the piece at the correct position on the board
                // You need to implement the drawing logic based on the piece type and color
                // For simplicity, let's assume drawing a placeholder rectangle
                Brush pieceBrush = piece.Color == PieceColors.White ? Brushes.White : Brushes.Black;
                g.FillRectangle(pieceBrush, coordinate.X * CellSize, coordinate.Y * CellSize, CellSize, CellSize);
            }
        }*/
    }
}