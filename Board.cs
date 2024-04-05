using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Chess
{
    public class Board : UserControl
    {
        public int CellSize { get; private set; }

        public Dictionary<Coordinate, APiece> BoardState { get; private set; }

        public Board()
        {
            BoardState = new Dictionary<Coordinate, APiece>();
            ChessLayout layout = new ChessLayout();
            layout.InitializeLayout();
            BoardState = layout;
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
                    g.FillRectangle(((i + j) % 2 == 0) ? Brushes.LightYellow : Brushes.SaddleBrown, CellSize * j, CellSize * i, CellSize, CellSize);
                }
            }
        }

        private void DrawLayout(Graphics g)
        {
            foreach (Coordinate c in BoardState.Keys)
            {
                APiece piece = BoardState[c];
                g.DrawImage(piece.GetImage(), new Rectangle(c.Y * CellSize, c.X * CellSize, CellSize, CellSize));
            }
        }

    }
}
