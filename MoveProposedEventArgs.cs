using System;

namespace Chess
{
    [Serializable]
    public class MoveProposedEventArgs : EventArgs
    {
        public Move ProposedMove { get; }

        public MoveProposedEventArgs(Move proposedMove)
        {
            ProposedMove = proposedMove;
        }
    }
}
