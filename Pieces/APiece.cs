namespace Chess
{
    public abstract class APiece
    {
        public PieceColors Color { get; }

        protected APiece(PieceColors color)
        {
            Color = color;
        }

        public abstract void Move();
    }
}
