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
        private Referee Referee; // Reference to the Referee

        private Coordinate LastHoveredCell;

        private APiece draggedPiece;
        private Coordinate originalCell;
        private int offsetX, offsetY;
        private bool isDragging = false;

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
            Referee = new Referee(); // Instantiate the Referee
            Referee.Initialize(this); // Pass a reference to this board
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
            DrawDraggedPiece(doubleBufferingGraphics); // Draw the dragged piece
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

        private void DrawDraggedPiece(Graphics g)
        {
            if (isDragging && draggedPiece != null)
            {
                // Calculate the position of the dragged piece based on the mouse cursor
                int mouseX = MousePosition.X - this.Parent.PointToScreen(this.Location).X;
                int mouseY = MousePosition.Y - this.Parent.PointToScreen(this.Location).Y;
                int cellX = mouseY / CellSize;
                int cellY = mouseX / CellSize;

                // Draw the dragged piece centered on the current cell
                g.DrawImage(draggedPiece.GetImage(), new Rectangle(cellY * CellSize + offsetX, cellX * CellSize + offsetY, CellSize, CellSize));
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            // Check if the mouse is within the bounds of a piece
            int clickedX = e.Y / CellSize;
            int clickedY = e.X / CellSize;
            Coordinate clickedCell = Coordinate.GetInstance(clickedX, clickedY);

            if (Layout.ContainsKey(clickedCell))
            {
                draggedPiece = Layout[clickedCell];
                originalCell = clickedCell;

                // Calculate the offset relative to the center of the cell
                offsetX = e.X - ((clickedY * CellSize) + (CellSize / 2));
                offsetY = e.Y - ((clickedX * CellSize) + (CellSize / 2));

                isDragging = true;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (isDragging && draggedPiece != null)
            {
                // Calculate the destination cell
                int clickedX = e.Y / CellSize;
                int clickedY = e.X / CellSize;
                Coordinate destinationCell = Coordinate.GetInstance(clickedX, clickedY);

                // Check if the destination cell is valid and empty
                if (Layout.ContainsKey(destinationCell) && destinationCell != originalCell)
                {
                    // Check if the move is valid (for now, considering all moves valid)
                    Move proposedMove = new Move(originalCell, destinationCell);
                    if (IsMoveValid(proposedMove))
                    {
                        // Remove the piece from the original cell
                        Layout.Remove(originalCell);

                        // Update the layout with the moved piece
                        Layout.Add(destinationCell, draggedPiece);

                        // Raise the MoveProposed event with the Move object
                        MoveProposed?.Invoke(this, new MoveProposedEventArgs(proposedMove));
                        this.Refresh(); // Redraw the board
                    }
                }
            }

            // Reset drag variables
            draggedPiece = null;
            originalCell = null;
            isDragging = false;
        }


        // Method to check if the move is valid
        private bool IsMoveValid(Move move)
        {
            // Implement your move validation logic here
            // You can access source and destination coordinates via move.Source and move.Destination
            return true; // For now, consider all moves valid
        }
    }
}
