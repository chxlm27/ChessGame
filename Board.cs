using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Chess
{
    public partial class Board : UserControl
    {
        public int CellSize { get; private set; }

        private ALayout Layout { get; set; }

        private Coordinate LastHoveredCell;

        private Pen highlightPen = new Pen(Brushes.Red, 4);
        private Pen redPen = new Pen(Color.FromArgb(0, 255, 0), 4);
        private Pen greenPen = new Pen(Color.FromArgb(173, 255, 47), 2);

        // Define the MoveProposed event
        public event EventHandler<MoveProposedEventArgs> MoveProposed;

        public Board()
        {
            Initialize();
        }

        public void Initialize()
        {
            this.DoubleBuffered = true;
            this.MouseMove += Board_MouseMove;
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
                        g.DrawRectangle(redPen, destinationCoordinate.Y * CellSize, destinationCoordinate.X * CellSize, CellSize, CellSize);
                        g.DrawRectangle(greenPen, destinationCoordinate.Y * CellSize, destinationCoordinate.X * CellSize, CellSize, CellSize);
                    }
                }
            }
        }

        private void HighlightHoveredOverCell(Graphics g)
        {
            if (LastHoveredCell != null)
            {
                g.DrawRectangle(highlightPen, LastHoveredCell.Y * CellSize, LastHoveredCell.X * CellSize, CellSize, CellSize);
            }
        }

        // Event handler for mouse click event
        // Event handler for mouse click event
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            // Calculate the clicked cell's coordinates
            int clickedX = e.Y / CellSize;
            int clickedY = e.X / CellSize;

            // Create Coordinate objects for the source and destination cells
            Coordinate sourceCell = LastHoveredCell;
            Coordinate destinationCell = Coordinate.GetInstance(clickedX, clickedY);

            // Create a Move object with source and destination cells
            Move move = new Move(sourceCell, destinationCell);

            // Raise the MoveProposed event with the Move object
            MoveProposed?.Invoke(this, new MoveProposedEventArgs(move));
        }
    }
}
