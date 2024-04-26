using System;
using System.Collections.Generic;

namespace Chess
{
    public class ChessLayout : ALayout
    {
        public ChessLayout() : base()
        {
        }
        public override ALayout Clone()
        {
            ChessLayout clone = new ChessLayout();
            foreach (var kvp in this)
            {
                clone.Add(kvp.Key, kvp.Value);
            }
            return clone;
        }
        public override void Initialize()
        {
            this.Add(Coordinate.GetInstance(7, 0), PieceFactory.CreatePiece(PieceType.Rook, PieceColors.Black));
            this.Add(Coordinate.GetInstance(7, 1), PieceFactory.CreatePiece(PieceType.Knight, PieceColors.Black));
            this.Add(Coordinate.GetInstance(7, 2), PieceFactory.CreatePiece(PieceType.Bishop, PieceColors.Black));
            this.Add(Coordinate.GetInstance(7, 3), PieceFactory.CreatePiece(PieceType.Queen, PieceColors.Black));
            this.Add(Coordinate.GetInstance(7, 4), PieceFactory.CreatePiece(PieceType.King, PieceColors.Black));
            this.Add(Coordinate.GetInstance(7, 5), PieceFactory.CreatePiece(PieceType.Bishop, PieceColors.Black));
            this.Add(Coordinate.GetInstance(7, 6), PieceFactory.CreatePiece(PieceType.Knight, PieceColors.Black));
            this.Add(Coordinate.GetInstance(7, 7), PieceFactory.CreatePiece(PieceType.Rook, PieceColors.Black));

            for (int i = 0; i < 8; i++)
            {
                this.Add(Coordinate.GetInstance(6, i), PieceFactory.CreatePiece(PieceType.Pawn, PieceColors.Black));
                this.Add(Coordinate.GetInstance(1, i), PieceFactory.CreatePiece(PieceType.Pawn, PieceColors.White));
            }

            this.Add(Coordinate.GetInstance(0, 0), PieceFactory.CreatePiece(PieceType.Rook, PieceColors.White));
            this.Add(Coordinate.GetInstance(0, 1), PieceFactory.CreatePiece(PieceType.Knight, PieceColors.White));
            this.Add(Coordinate.GetInstance(0, 2), PieceFactory.CreatePiece(PieceType.Bishop, PieceColors.White));
            this.Add(Coordinate.GetInstance(0, 3), PieceFactory.CreatePiece(PieceType.Queen, PieceColors.White));
            this.Add(Coordinate.GetInstance(0, 4), PieceFactory.CreatePiece(PieceType.King, PieceColors.White));
            this.Add(Coordinate.GetInstance(0, 5), PieceFactory.CreatePiece(PieceType.Bishop, PieceColors.White));
            this.Add(Coordinate.GetInstance(0, 6), PieceFactory.CreatePiece(PieceType.Knight, PieceColors.White));
            this.Add(Coordinate.GetInstance(0, 7), PieceFactory.CreatePiece(PieceType.Rook, PieceColors.White));
        }
    }
}
