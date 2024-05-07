using Gamee.Framework;

namespace Gamee.Checkers
{
    public class CheckersLayoutFactory : ILayoutFactory
    {
        public ALayout CreateLayout()
        {
            return new CheckersLayout();
        }
    }
}
