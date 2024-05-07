using Gamee.Framework;

namespace Gamee.Chess
{
    public class ChessLayoutFactory : ILayoutFactory
    {
        public ALayout CreateLayout()
        {
            return new ChessLayout();
        }
    }
}
