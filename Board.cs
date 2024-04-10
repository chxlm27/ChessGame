using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime;
using System.Windows.Forms;

namespace Chess
{
    public class Board : UserControl
    {
        public int CellSize { get; private set; }

        private ALayout Layout { get; set; }

        private Coordinate LastHoveredCell;

        private int MouseOverCellX;
        private int MouseOverCellY;
        private int _highlightThickness = 2;

        public Board()
        {
            this.MouseMove += Board_MouseMove;
        }

        public void Initialize()
        {
            this.DoubleBuffered = true;

            Layout = new ChessLayout();
            Layout.Initialize(); 
        }

        private void Board_MouseMove(object sender, MouseEventArgs e)
        {
            int mouseX = e.X / CellSize;
            int mouseY = e.Y / CellSize;
            Coordinate currentHoveredCell = Coordinate.GetInstance(mouseY, mouseX);


            if (LastHoveredCell != currentHoveredCell)
            {
                LastHoveredCell = currentHoveredCell;
                Console.WriteLine($"Mouse moved from {LastHoveredCell.X},{LastHoveredCell.Y} to {currentHoveredCell.X},{currentHoveredCell.Y}");
                this.Refresh();
            }
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
            HighlightHoveredOverCell(doubleBufferingGraphics);
            DrawAvailableMoves(doubleBufferingGraphics);
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


        /*        private void DrawAvailableMoves(Graphics g)
                {
                    if (LastHoveredCell != null && Layout.ContainsKey(LastHoveredCell))
                    {
                        APiece piece = Layout[LastHoveredCell];

                        List<Coordinate> availableDestinations = piece.GetAvailableMoves(LastHoveredCell);
                        foreach (Coordinate coordinate in availableDestinations)
                        {
                            g.FillRectangle(Brushes.Green, coordinate.Y * CellSize, coordinate.X * CellSize, CellSize, CellSize);

                        }
                    }
                }*/
        private void DrawAvailableMoves(Graphics g)
        {
            if (LastHoveredCell != null && Layout.ContainsKey(LastHoveredCell))
            {
                APiece piece = Layout[LastHoveredCell];
                if (piece != null)
                {
                    List<Coordinate> availableMoves = piece.GetAvailableMoves(LastHoveredCell);
                    foreach (Coordinate destinationCoordinate in availableMoves)
                    {
                        // Use a stronger green color for the highlight pen
                        Pen highlightPen = new Pen(Color.FromArgb(0, 255, 0), _highlightThickness);
                        g.DrawRectangle(highlightPen, destinationCoordinate.Y * CellSize, destinationCoordinate.X * CellSize, CellSize, CellSize);

                        // Draw a rectangle with the same dimensions, but with a lighter green color
                        highlightPen = new Pen(Color.FromArgb(173, 255, 47), _highlightThickness - 2);
                        g.DrawRectangle(highlightPen, destinationCoordinate.Y * CellSize, destinationCoordinate.X * CellSize, CellSize, CellSize);
                    }
                }
            }
        }



        private void HighlightHoveredOverCell(Graphics g)
        {
            if (LastHoveredCell != null)
            {
                Pen highlightPen = new Pen(Brushes.Red, _highlightThickness);
                g.DrawRectangle(highlightPen, LastHoveredCell.Y * CellSize, LastHoveredCell.X * CellSize, CellSize, CellSize);
            }
        }

    }
}
