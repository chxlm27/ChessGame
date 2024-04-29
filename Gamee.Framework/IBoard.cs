using System;

namespace Gamee.Framework
{
    public interface IBoard
    {
        // Properties
        int CellSize { get; }
        // Removed ChessGame property from the interface
        Context GameContext { get; set; }

        // Events
        event EventHandler<MoveProposedEventArgs> MoveProposed;

        // Methods
        void Initialize();
        void Rescale(int windowWidth, int windowHeight, int menuHeight);
        void SetContext(Context newContext);
        void OnGameContextChanged(object sender, GameContextChangedEventArgs e);
    }
}
