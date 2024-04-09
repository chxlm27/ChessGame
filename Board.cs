using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Chess
{
    public class Board : UserControl
    {
        public int CellSize { get; private set; }

        private ALayout Layout { get; set; }
        private Coordinate lastHoveredCell = Coordinate.GetInstance(0, 0);

        public Board()
        {
            this.MouseMove += Board_MouseMove;
        }

        private void Board_MouseMove(object sender, MouseEventArgs e)
        {
            int mouseX = e.X / CellSize;
            int mouseY = e.Y / CellSize;
            Coordinate currentHoveredCell = Coordinate.GetInstance(mouseY, mouseX);

            if (lastHoveredCell != currentHoveredCell)
            {
                Console.WriteLine($"Mouse moved from {lastHoveredCell.X},{lastHoveredCell.Y} to {currentHoveredCell.X},{currentHoveredCell.Y}");

                if (Layout.ContainsKey(currentHoveredCell))
                {
                    APiece piece = Layout[currentHoveredCell];
                    Console.WriteLine($"Chess piece at {currentHoveredCell.X},{currentHoveredCell.Y}: {piece.Type} ({piece.Color})");
                }

                lastHoveredCell = currentHoveredCell;
            }
        }

        public void Initialize()
        {
            Layout = new ChessLayout();
            Layout.Initialize();
        }

        public void Rescale(int windowWidth, int windowHeight, int menuHeight)
        {
            int width = windowWidth - 16;
            int height = windowHeight - 39 - menuHeight;

            CellSize = Math.Min(width, height) / 8;
            this.SetBounds((width < height ? 0 : (width - height) / 2),
                           (width < height ? (height - width) / 2 + menuHeight : menuHeight),
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
            foreach (Coordinate c in Layout.Keys)
            {
                g.DrawImage(Layout[c].GetImage(), new Rectangle(c.Y * CellSize, c.X * CellSize, CellSize, CellSize));
            }
        }
    }
}
