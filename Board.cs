using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Chess
{
    public partial class Board : UserControl
    {
        public int CellSize { get; private set; }
        private Dictionary<Coordinate, APiece> pieces;
        private ALayout Layout { get; set; }
        private Context GameContext;
        private Coordinate LastHoveredCell;

        private APiece draggedPiece;
        private Coordinate originalCell;
        private int offsetX, offsetY;
        private bool isDragging = false;

        private Pen highlightPen = new Pen(Brushes.Red, 5);
        private Pen redPen = new Pen(Color.FromArgb(0, 255, 0), 5);
        private Pen greenPen = new Pen(Color.FromArgb(0, 100, 0), 3);


        private Referee referee;

        public event EventHandler<MoveProposedEventArgs> MoveProposed;

        public Board()
        {
            Initialize();
            referee = new Referee();
            referee.Initialize(this);
        }

        public void Initialize()
        {
            this.DoubleBuffered = true;
            this.MouseMove += Board_MouseMove;
            Layout = new ChessLayout();
            Layout.Initialize();
            GameContext = new Context();
            GameContext.CurrentPlayer = PieceColors.Black;
            referee = new Referee();
            referee.Initialize(this);
            referee.GameContextChanged += Referee_GameContextChanged;
        }


        private void Referee_GameContextChanged(object sender, GameContextChangedEventArgs e)
        {
            Layout = e.NewContext.Layout; // Update the layout with the new context's layout
            this.Refresh(); // Refresh the board to reflect the changes
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
            if (isDragging)
            {
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
            DrawDraggedPiece(doubleBufferingGraphics);
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
                if (piece != null && piece.Color == GameContext.CurrentPlayer) // Only show moves for the current player's pieces
                {
                    List<Coordinate> availableMoves = piece.GetAvailableMoves(LastHoveredCell, Layout);
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
                int mouseX = MousePosition.X - this.Parent.PointToScreen(this.Location).X;
                int mouseY = MousePosition.Y - this.Parent.PointToScreen(this.Location).Y;
                g.DrawImage(draggedPiece.GetImage(), mouseX - offsetX, mouseY - offsetY, CellSize, CellSize);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            PieceColors? pieceColor = GetPieceColorFromLayout(LastHoveredCell);

            if ((GameContext.CurrentPlayer == PieceColors.Black || (GameContext.CurrentPlayer == PieceColors.White && pieceColor == PieceColors.White)) && pieceColor == GameContext.CurrentPlayer)
            {
                ProcessPlayerMove(e);
            }
        }

        private void ProcessPlayerMove(MouseEventArgs e)
        {
            int clickedX = e.Y / CellSize;
            int clickedY = e.X / CellSize;
            Coordinate clickedCell = Coordinate.GetInstance(clickedX, clickedY);

            if (Layout.ContainsKey(clickedCell))
            {
                APiece clickedPiece = Layout[clickedCell];

                if (clickedPiece != null && clickedPiece.GetAvailableMoves(clickedCell, Layout).Count > 0)
                {
                    draggedPiece = clickedPiece;
                    originalCell = clickedCell;
                    offsetX = e.X % CellSize;
                    offsetY = e.Y % CellSize;
                    isDragging = true;
                }
            }
        }


        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (isDragging && draggedPiece != null)
            {
                int clickedX = e.Y / CellSize;
                int clickedY = e.X / CellSize;
                Coordinate destinationCell = Coordinate.GetInstance(clickedX, clickedY);

                if (destinationCell != originalCell && Layout.ContainsKey(destinationCell) == false)
                {
                    List<Coordinate> availableMoves = draggedPiece.GetAvailableMoves(originalCell, Layout);
                    if (availableMoves.Contains(destinationCell))
                    {
                        Layout.Remove(originalCell);
                        Layout.Add(destinationCell, draggedPiece);

                        MoveProposed?.Invoke(this, new MoveProposedEventArgs(new Move(originalCell, destinationCell)));

                        GameContext.SwitchPlayer();
                    }
                }
            }

            draggedPiece = null;
            originalCell = null;
            isDragging = false;

            this.Refresh();
        }

        private PieceColors? GetPieceColorFromLayout(Coordinate coordinate)
        {
            if (Layout.ContainsKey(coordinate))
            {
                APiece piece = Layout[coordinate];
                if (piece != null)
                {
                    return piece.Color;
                }
            }
            return null;
        }
    }
}
