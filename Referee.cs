using Chess;
using System;

public class Referee
{
    private Context context;
    private Board board; // Reference to the board

    public event EventHandler<GameContextChangedEventArgs> GameContextChanged;

    public Referee()
    {
        // Constructor is now empty
    }

    public void Initialize(Board board)
    {
        // Perform any necessary initialization
        // Subscribe to the MoveProposed event of the board
        this.board = board; // Initialize the board reference
        context = new Context();
        board.MoveProposed += OnMoveProposed;
    }

    public void ValidateMove(MoveProposedEventArgs move)
    {
        // Implement move validation logic here
        // This method will determine if the proposed move is valid
        // and if so, update the game context accordingly
        // Then, it will raise the GameContextChanged event

        // For now, let's just raise the event to demonstrate
        OnGameContextChanged(new GameContextChangedEventArgs(context));
    }

    public void OnMoveProposed(object sender, MoveProposedEventArgs e)
    {
        // Propagate the move to the referee for validation
        ValidateMove(e);
    }

    private void OnGameContextChanged(GameContextChangedEventArgs e)
    {
        // Raise the GameContextChanged event
        GameContextChanged?.Invoke(this, e);
    }
}
