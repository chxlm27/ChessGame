namespace Gamee.Framework
{
    public class Context
    {
        public PieceColors CurrentPlayer { get; set; }
        public ALayout Layout { get; set; }
        public delegate void StateChangedHandler();
        public event StateChangedHandler StateChanged;
        public Context()
        {
            CurrentPlayer = PieceColors.Black;
            //initializare layout in Game, instantiez acl layout si il transmit
/*            Layout = new ChessLayout();
            Layout.Initialize();*/
        }

        public Context Clone()
        {
            return new Context
            {
                CurrentPlayer = this.CurrentPlayer,
                Layout = this.Layout.Clone()
            };
        }

        public void SwitchPlayer()
        {
            CurrentPlayer = (CurrentPlayer == PieceColors.White) ? PieceColors.Black : PieceColors.White;
        }

        public void Move(Coordinate originalCell, Coordinate destinationCell)
        {
            if (Layout.ContainsKey(originalCell))
            {
                IPiece piece = Layout[originalCell];
                if (piece != null)
                {
                    Layout.Remove(originalCell);

                    if (Layout.ContainsKey(destinationCell))
                    {
                        Layout.Remove(destinationCell);
                    }

                    Layout.Add(destinationCell, piece);
                    OnStateChanged();
                }
            }
        }

        protected virtual void OnStateChanged()
        {
            StateChanged?.Invoke();
        }
    }
}