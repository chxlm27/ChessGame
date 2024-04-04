using System;
using System.Collections.Generic;

namespace Chess
{
    public class Layout
    {
        private Dictionary<int, Dictionary<int, APiece>> initialLayout;

        public Layout()
        {
            initialLayout = new Dictionary<int, Dictionary<int, APiece>>();
        }

        public void SetupInitialLayout(Board board)
        {

            // Initialize the dictionary for each row
            for (int i = 0; i < 8; i++)
            {
                initialLayout[i] = new Dictionary<int, APiece>();
            }

            // Setup white pieces
            SetupPiecesForRow(PieceColors.White, 0, board);
            SetupPiecesForRow(PieceColors.White, 1, board);

            // Setup black pieces
            SetupPiecesForRow(PieceColors.Black, 7, board);
            SetupPiecesForRow(PieceColors.Black, 6, board);
        }

        private void SetupPiecesForRow(PieceColors color, int row, Board board)
        {
            // Set up pawns
            for (int i = 0; i < 8; i++)
            {
                AddPieceToLayout(board, PieceFactory.CreatePiece(PieceType.Pawn, color), row, i);
            }

            // Set up other pieces
            AddPieceToLayout(board, PieceFactory.CreatePiece(PieceType.Rook, color), row, 0);
            AddPieceToLayout(board, PieceFactory.CreatePiece(PieceType.Knight, color), row, 1);
            AddPieceToLayout(board, PieceFactory.CreatePiece(PieceType.Bishop, color), row, 2);
            AddPieceToLayout(board, PieceFactory.CreatePiece(PieceType.Queen, color), row, 3);
            AddPieceToLayout(board, PieceFactory.CreatePiece(PieceType.King, color), row, 4);
            AddPieceToLayout(board, PieceFactory.CreatePiece(PieceType.Bishop, color), row, 5);
            AddPieceToLayout(board, PieceFactory.CreatePiece(PieceType.Knight, color), row, 6);
            AddPieceToLayout(board, PieceFactory.CreatePiece(PieceType.Rook, color), row, 7);
        }

        private void AddPieceToLayout(Board board, APiece piece, int row, int col)
        {
            Coordinate coordinate = Coordinate.GetInstance(col, row);
            board.BoardState[coordinate] = piece;
            initialLayout[row][col] = piece;
        }
    }
}
