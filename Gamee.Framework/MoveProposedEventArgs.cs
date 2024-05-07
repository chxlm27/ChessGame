using System;

namespace Gamee.Framework
{
    public class MoveProposedEventArgs : EventArgs
    {
        public Move ProposedMove { get; }

        public MoveProposedEventArgs(Move proposedMove)
        {
            ProposedMove = proposedMove;
        }
    }
}
