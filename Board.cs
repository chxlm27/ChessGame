/*using System.Drawing;
using System.Windows.Forms;

namespace Chess
{
    public class Board : UserControl
    {
        public const int BoardSize = 8;
        public const int CellSize = 50; // Adjust as needed

        private readonly PictureBox[,] _cells;
        private Image _chessPiecesImage;

        public Board()
        {
            _cells = new PictureBox[BoardSize, BoardSize];

            // Load the chess pieces image
            _chessPiecesImage = Image.FromFile("ChessPiecesArray.png");

            // Initialize the board
            for (int row = 0; row < BoardSize; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    _cells[row, col] = new PictureBox
                    {
                        Size = new Size(CellSize, CellSize),
                        Location = new Point(col * CellSize, row * CellSize),
                        BackColor = (row + col) % 2 == 0 ? Color.White : Color.Gray
                    };
                    Controls.Add(_cells[row, col]);
                }
            }

            // Adjust the size of the user control based on the cells
            Size = new Size(BoardSize * CellSize, BoardSize * CellSize);

            // Place the chess pieces on the board
            PlaceChessPieces();
        }



        private void PlaceChessPieces()
        {
            // Determine the size of each chess piece in the image
            int pieceWidth = _chessPiecesImage.Width / 6; // 6 pieces in total in the image
            int pieceHeight = _chessPiecesImage.Height / 2; // 2 rows (black and white)

            // Define the mapping of piece types to their positions in the image
            PieceType[] pieceTypes = {
        PieceType.Queen, PieceType.King, PieceType.Rook, PieceType.Knight, PieceType.Bishop, PieceType.Pawn
    };

            // Rows to place pieces
            int[] rowsWithPieces = { 0, 1, 6, 7 };

            foreach (int row in rowsWithPieces)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    // Calculate the index of the piece in the image based on its position on the board
                    int pieceIndex;
                    if (row == 0 || row == BoardSize - 1)
                    {
                        // Place Tower, Horse, Crazy, Queen, King, Crazy, Horse, Tower in reverse order for white pieces
                        int[] pieceOrder = { 2, 3, 4, 0, 1, 4, 3, 2 }; // Tower, Horse, Crazy, Queen, King, Crazy, Horse, Tower
                        pieceIndex = pieceOrder[col];
                        if (row == BoardSize - 1) // Invert for the last row (white pieces)
                            pieceIndex = pieceOrder[7 - col];
                    }
                    else if (row == 1 || row == 6)
                    {
                        // Set black pawns for the second and white pawns for the seventh row
                        pieceIndex = 5; // Index of black/white pawn in the image
                    }
                    else
                    {
                        // Rows 2 to 5 are empty, skip placing pieces
                        continue;
                    }

                    // Calculate the position of the piece in the image
                    int x = pieceIndex * pieceWidth;
                    int y = row < 4 ? 0 : pieceHeight; // First 4 rows for black, next 4 for white

                    // Crop the piece from the image
                    Bitmap pieceBitmap = new Bitmap(pieceWidth, pieceHeight);
                    Graphics g = Graphics.FromImage(pieceBitmap);
                    Rectangle sourceRectangle = new Rectangle(x, y, pieceWidth, pieceHeight);
                    Rectangle destRectangle = new Rectangle(0, 0, pieceWidth, pieceHeight);
                    g.DrawImage(_chessPiecesImage, destRectangle, sourceRectangle, GraphicsUnit.Pixel);
                    g.Dispose();

                    // Create a PictureBox to display the piece
                    _cells[row, col].Image = pieceBitmap;
                    _cells[row, col].SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
        }

    }

    // Define the PieceType enumeration
    public enum PieceType
    {
        Queen,
        King,
        Rook,
        Knight,
        Bishop,
        Pawn
    }
}
*/
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Chess
{
    public class Board : UserControl
    {
        private const int _size = 8;
        private const int _cellSize = 50;
        private int CellSize;
        private Image _chessPiecesImage;

        public Board()
        {
            // Load the chess pieces image
            _chessPiecesImage = Image.FromFile("ChessPiecesArray.png");

            // Set initial size of the board
            this.Width = _size * _cellSize;
            this.Height = _size * _cellSize;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Bitmap doubleBufferingImage = new Bitmap(CellSize * 8, CellSize * 8);
            using (Graphics doubleBufferingGraphics = Graphics.FromImage(doubleBufferingImage))
            {
                DrawSquares(doubleBufferingGraphics);
                e.Graphics.DrawImage(doubleBufferingImage, 0, 0);
            }
        }

        private void DrawSquares(Graphics g)
        {
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    Brush brush = ((i % 2 + j % 2) % 2 == 0) ? Brushes.LightYellow : Brushes.SaddleBrown;
                    g.FillRectangle(brush, CellSize * j, CellSize * i, CellSize, CellSize);
                }
            }
        }

        public void PlaceChessPieces()
        {
            // Code to place the chess pieces on the board goes here
            // ...
        }

        public void RescaleBoard(int windowWidth, int windowHeight)
        {
            int width = windowWidth - 16;
            int height = windowHeight - 39;
            CellSize = Math.Min(width, height) / 8;

            if (width < height)
                this.SetBounds(0, (height - width) / 2, CellSize * 8, CellSize * 8);
            else
                this.SetBounds((width - height) / 2, 0, CellSize * 8, CellSize * 8);

            this.Refresh();
        }

        // Any additional methods such as event handlers or helper methods for drawing, etc.
        // ...
    }
}
