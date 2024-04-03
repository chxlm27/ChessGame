using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Chess
{
    public class Board : UserControl
    {
        private const int _size = 8;
        private const int _cellSize = 50; // Initial size for the cells
        private int CellSize = 50;
        private Image _chessPiecesImage;
        private PictureBox[,] _cells = new PictureBox[_size, _size];

        public Board()
        {
            this.Width = _size * _cellSize;
            this.Height = _size * _cellSize;
            _chessPiecesImage = Image.FromFile("ChessPiecesArray.png");
            InitializeCells();
            PlaceChessPieces();
        }

        private void InitializeCells()
        {
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    _cells[i, j] = new PictureBox
                    {
                        Width = _cellSize,
                        Height = _cellSize,
                        Location = new Point(j * _cellSize, i * _cellSize),
                        BackColor = ((i + j) % 2 == 0) ? Color.LightYellow : Color.SaddleBrown,
                        SizeMode = PictureBoxSizeMode.StretchImage
                    };
                    this.Controls.Add(_cells[i, j]);
                }
            }
        }

        private void PlaceChessPieces()
        {
            int pieceWidth = _chessPiecesImage.Width / 6;
            int pieceHeight = _chessPiecesImage.Height / 2;

            for (int row = 0; row < _size; row++)
            {
                for (int col = 0; col < _size; col++)
                {
                    int pieceIndex = GetPieceIndex(row, col);
                    if (pieceIndex != -1)
                    {
                        int x = pieceIndex * pieceWidth;
                        int y = (row < _size / 2) ? 0 : pieceHeight; // Top half of image for black, bottom for white

                        Rectangle pieceRect = new Rectangle(x, y, pieceWidth, pieceHeight);
                        _cells[row, col].Image = CropImage(_chessPiecesImage, pieceRect);
                    }
                }
            }
        }

        private Image CropImage(Image image, Rectangle source)
        {
            Bitmap bmp = new Bitmap(source.Width, source.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.DrawImage(image, 0, 0, source, GraphicsUnit.Pixel);
            }
            return bmp;
        }

        private int GetPieceIndex(int row, int col)
        {
            // Correct piece placement at the beginning of a chess game
            if (row == 1 || row == 6)
            {
                return 5; // Pawn
            }
            else if (row == 0 || row == 7)
            {
                int[] order = { 2, 3, 4, 1, 0, 4, 3, 2 }; // Rook, Knight, Bishop, Queen, King, Bishop, Knight, Rook
                return order[col];
            }
            return -1; // No piece
        }

        public void RescaleBoard(int windowWidth, int windowHeight)
        {
            // Calculate the new cell size, maintaining aspect ratio
            int newCellSize = Math.Min(windowWidth / _size, windowHeight / _size);

            // Set the new bounds for each cell
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    _cells[i, j].Width = newCellSize;
                    _cells[i, j].Height = newCellSize;
                    _cells[i, j].Location = new Point(j * newCellSize, i * newCellSize);
                }
            }

            // Calculate the actual width and height of the board
            int boardWidth = _size * newCellSize;
            int boardHeight = _size * newCellSize;

            // Center the board within the form
            this.Width = boardWidth;
            this.Height = boardHeight;
            this.Location = new Point((windowWidth - boardWidth) / 2, (windowHeight - boardHeight) / 2);

            // Redraw the board to apply changes
            this.Refresh();
        }



    }
}