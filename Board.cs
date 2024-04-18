using Chess;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Chess
{
    public partial class Board : UserControl
    {
        /*        public int CellSize { get; private set; }
                private Dictionary<Coordinate, APiece> pieces;
                private Context GameContext { get; set; }
                private Coordinate LastHoveredCell;

                private APiece draggedPiece;
                private Coordinate originalCell;
                private int offsetX, offsetY;
                private bool isDragging = false;

                private Pen highlightPen = new Pen(Brushes.Red, 5);
                private Pen redPen = new Pen(Color.FromArgb(0, 255, 0), 5);
                private Pen greenPen = new Pen(Color.FromArgb(0, 100, 0), 3);

                public event EventHandler<MoveProposedEventArgs> MoveProposed;

                public Board()
                {
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
        */
        private int _cellSize = 80;
        private int _mouseOverCellX = 8;
        private int _mouseOverCellY = 8;
        private const int _highlightThickness = 6;
        private Coordinate LastHoveredCell;
        private bool isDragging = false;
        public int CellSize
        {
            get { return _cellSize; }
            set { _cellSize = value; }
        }
        public int MouseOverCellX
        {
            get { return _mouseOverCellX; }
            private set { _mouseOverCellX = value; }
        }
        public int MouseOverCellY
        {
            get { return _mouseOverCellY; }
            private set { _mouseOverCellY = value; }
        }
        public Coordinate DraggedCoordinate { get; set; }
        public Coordinate TargetCoordinate { get; set; }
        private Context CurrentContext { get; set; }
        public event MoveProposedHandler MoveProposed;
        public delegate void MoveProposedHandler(object sender, MoveProposedEventArgs e);
        public void Initialize()
        {
            this.Refresh();
        }
        public void Referee_GameContextChanged(object sender, GameContextChangedEventArgs e)
        {
            this.CurrentContext = e.Context.Clone();
            this.Refresh();
        }

        /*private void Referee_GameContextChanged(object sender, GameContextChangedEventArgs e)
                {
                    Layout = e.NewContext.Layout; // Update the layout with the new context's layout
                    this.Refresh(); // Refresh the board to reflect the changes
                }*/

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

        /*        protected override void OnPaint(PaintEventArgs e)
                {
                    Bitmap doubleBufferingImage = new Bitmap(CellSize * 8, CellSize * 8);
                    Graphics doubleBufferingGraphics = Graphics.FromImage(doubleBufferingImage);

                    DrawSquares(doubleBufferingGraphics);
                    DrawLayout(doubleBufferingGraphics);
                    HighlightHoveredOverCell(doubleBufferingGraphics);
                    DrawAvailableMoves(doubleBufferingGraphics);
                    e.Graphics.DrawImage(doubleBufferingImage, 0, 0);
                }*/
        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap doubleBufferingImage = new Bitmap(CellSize * 8, CellSize * 8);
            Graphics doubleBufferingGraphics = Graphics.FromImage(doubleBufferingImage);
            DrawSquares(doubleBufferingGraphics);
            if (CurrentContext != null && CurrentContext.Layout != null) // Update to use Layout property
            {
                DrawLayout(doubleBufferingGraphics);
                HighlightHoveredOverCell(doubleBufferingGraphics);
                DrawAvailableMoves(doubleBufferingGraphics);
            }
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
                }*/
        private void DrawAvailableMoves(Graphics g)
        {
            if (DraggedCoordinate != null)
            {
                return;
            }
            Coordinate sourceCoordinate = sourceCoordinate.GetInstance(MouseOverCellX, MouseOverCellY);
            if (CurrentContext.Layout.ContainsKey(sourceCoordinate))
            {
                APiece piece = CurrentContext.Layout[sourceCoordinate];
                if (piece != null)
                {
                    List<Coordinate> availableMoves = piece.GetAvailableMoves(sourceCoordinate, CurrentContext);
                    foreach (Coordinate c in availableMoves)
                    {
                        Pen highlightPen = new Pen(Brushes.OrangeRed, _highlightThickness);
                        g.DrawRectangle(highlightPen, c.X * CellSize, c.Y * CellSize, CellSize, CellSize);

                        highlightPen = new Pen(Brushes.Yellow, _highlightThickness - 2);
                        g.DrawRectangle(highlightPen, c.X * CellSize, c.Y * CellSize, CellSize, CellSize);
                    }
                }
            }
        }


        /*   private void HighlightHoveredOverCell(Graphics g)
           {
               if (LastHoveredCell != null)
               {
                   g.DrawRectangle(highlightPen, LastHoveredCell.Y * CellSize, LastHoveredCell.X * CellSize, CellSize, CellSize);
               }
           }*/
        private void HighlightHoveredOverCell(Graphics g)
        {
            if (MouseOverCellX >= 0 && MouseOverCellX <= 7 && MouseOverCellY >= 0 && MouseOverCellY <= 7)
            {
                Pen highlightPen = new Pen(Brushes.Red, _highlightThickness);
                g.DrawRectangle(highlightPen, MouseOverCellX * CellSize, MouseOverCellY * CellSize, CellSize, CellSize);
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

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            var clickedX = e.X / CellSize;
            var clickedY = e.Y / CellSize;

            DraggedCoordinate = Coordinate.GetInstance(clickedX, clickedY);

            if (e.Button == MouseButtons.Left && CurrentContext.Layout.ContainsKey(DraggedCoordinate))
            {
                APiece selectedPiece = CurrentContext.Layout[DraggedCoordinate];
                Bitmap cursorBitmap = new Bitmap(CellSize, CellSize);
                Graphics cursorGraphics = Graphics.FromImage(cursorBitmap);
                cursorGraphics.DrawImage(selectedPiece.GetImage(), 0, 0, CellSize, CellSize);
                Cursor.Current = new Cursor(cursorBitmap.GetHicon());
                this.CurrentContext.Layout.Remove(DraggedCoordinate);
                this.Refresh();
            }
            else
            {
                DraggedCoordinate = null;
            }

        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            var clickedX = e.X / CellSize;
            var clickedY = e.Y / CellSize;
            TargetCoordinate = Coordinate.GetCoordinate(clickedX, clickedY);
            if (e.Button == MouseButtons.Left && DraggedCoordinate != null)
            {
                Cursor.Current = Cursors.Default;
                Move proposedMove = new Move();
                proposedMove.Source = DraggedCoordinate;
                proposedMove.Target = TargetCoordinate;
                MoveProposedEventArgs me = new MoveProposedEventArgs();
                me.ProposedMove = proposedMove;
                if (MoveProposed != null)
                {
                    MoveProposed(this, me);
                }
                DraggedCoordinate = null;
                TargetCoordinate = null;
                this.Refresh();
            }
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
        public void OnGameContextChanged(object sender, GameContextChangedEventArgs e)
        {
            GameContextChanged?.Invoke(this, e);
        }
    }
}
