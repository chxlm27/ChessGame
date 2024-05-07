namespace Gamee.Framework
{
    // The IPieceFactory interface declares a method for creating pieces.
    public interface IPieceFactory
    {
        // CreatePiece should return an IPiece object based on provided type and color.
        // The method parameters might vary depending on how you categorize or identify piece types.
        IPiece CreatePiece(ChessPieceType type, PieceColors color);
    }
}
