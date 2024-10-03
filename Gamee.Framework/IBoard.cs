using System;

namespace Gamee.Framework
{
    public interface IBoard
    {
        int CellSize { get; }
        Context GameContext { get; set; }

        event EventHandler<MoveProposedEventArgs> MoveProposed;

        void Initialize(GameType gameType);
        void Rescale(int windowWidth, int windowHeight, int menuHeight);
        void SetContext(Context newContext);
        void OnGameContextChanged(object sender, GameContextChangedEventArgs e);
    }
}
