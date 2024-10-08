﻿using Gamee.Checkers;
using Gamee.Chess;
using Gamee.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ChessGameApp
{
    public partial class Board : UserControl, IBoard
    {
        public int CellSize { get; private set; }
        private ALayout Layout { get; set; }
        public Context GameContext { get; set; }

        private Coordinate LastHoveredCell;
        public ChessGame ChessGame { get; set; }
        public CheckersGame CheckersGame { get; set; }

        private IPiece draggedPiece;
        private Coordinate originalCell;
        private int offsetX, offsetY;
        private bool isDragging = false;

        private Pen highlightPen = new Pen(Brushes.Red, 5);
        private Pen redPen = new Pen(Color.FromArgb(0, 255, 0), 5);
        private Pen greenPen = new Pen(Color.FromArgb(0, 100, 0), 3);

        public event EventHandler<MoveProposedEventArgs> MoveProposed;

        public Board(GameType gameType)
        {
            Initialize(gameType);
        }

        public void Initialize(GameType gameType)
        {
            this.DoubleBuffered = true;
            this.MouseMove += Board_MouseMove;

            if (gameType == GameType.Chess)
            {
                Layout = new ChessLayout();
                ChessGame = new ChessGame();
            }
            else if (gameType == GameType.Checkers)
            {
                Layout = new CheckersLayout();
                CheckersGame = new CheckersGame();
            }

            Layout.Initialize();

            if (GameContext == null)
                GameContext = new Context();
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
            if (Layout != null)
            {
                foreach (Coordinate c in Layout.Keys)
                {
                    g.DrawImage(Layout[c].GetImage(), new Rectangle(c.Y * CellSize, c.X * CellSize, CellSize, CellSize));
                }
            }
        }

        private void DrawAvailableMoves(Graphics g)
        {
            if (LastHoveredCell != null && Layout.ContainsKey(LastHoveredCell))
            {
                var piece = Layout[LastHoveredCell] as APiece; // Safe cast
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
                APiece clickedPiece = Layout[clickedCell] as APiece;

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

                if (destinationCell != originalCell)
                {
                    List<Coordinate> availableMoves = draggedPiece.GetAvailableMoves(originalCell, Layout);
                    if (availableMoves.Contains(destinationCell))
                    {
                        if (Layout.ContainsKey(destinationCell))
                        {
                            Layout.Remove(destinationCell);
                        }

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
                APiece piece = Layout[coordinate] as APiece;
                if (piece != null)
                {
                    return piece.Color;
                }
            }
            return null;
        }

        public void OnGameContextChanged(object sender, GameContextChangedEventArgs e)
        {
            Context clonedContext = e.NewContext.Clone();
            Layout = clonedContext.Layout;
            this.Refresh();
        }


        public void SetContext(Context newContext)
        {
            GameContext = newContext;
            Layout = newContext.Layout;
            this.Refresh();
        }
    }
}
