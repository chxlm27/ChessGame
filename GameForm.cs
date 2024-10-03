using Gamee.Checkers;
using Gamee.Chess;
using Gamee.Framework;
using System;
using System.Windows.Forms;

namespace ChessGameApp
{
    public partial class GameForm : Form
    {
        private ChessGame GameChess { get; set; }
        private CheckersGame GameCheckers { get; set; }
        private Board Board { get; set; }
        private Context GameContext { get; set; }

        public GameForm()
        {
            InitializeComponent();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Board?.Rescale(this.Width, this.Height, menuStrip1.Height);
        }

        private void beginChessGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitializeChessGame();
        }

        private void beginCheckersGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitializeCheckersGame();
        }

        private void InitializeChessGame()
        {
            if (Board == null)
            {
                Board = new Board(GameType.Chess);
                Controls.Add(Board);
            }

            if (GameChess == null)
            {
                GameChess = new ChessGame();
            }

            Referee referee = new Referee();

            // Now pass both Board and Referee to the Initialize method
            GameChess.Initialize(Board, referee);
            Board.Initialize(GameType.Chess); // Pass GameType.Chess to the Initialize method
            Board.ChessGame = GameChess;
            Board.GameContext = GameChess.GameContext;
            Board.Rescale(this.Width, this.Height, menuStrip1.Height);
            GameChess.Start();
        }

        private void InitializeCheckersGame()
        {
            if (Board == null)
            {
                Board = new Board(GameType.Checkers);
                Controls.Add(Board);
            }

            if (GameCheckers == null)
            {
                GameCheckers = new CheckersGame();
            }

            // Create a Referee object to pass to the Initialize method
            Referee referee = new Referee();

            // Now pass both Board and Referee to the Initialize method
            GameCheckers.Initialize(Board, referee);
            Board.Initialize(GameType.Checkers);
            Board.CheckersGame = GameCheckers;
            Board.GameContext = GameCheckers.GameContext;
            Board.Rescale(this.Width, this.Height, menuStrip1.Height);
            GameCheckers.Start();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Board != null && Board.GameContext != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "JSON Files (*.json)|*.json",
                    DefaultExt = "json",
                    AddExtension = true,
                    FileName = "Game_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".json"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (Board.ChessGame != null)
                    {
                        Board.ChessGame.SaveGame(saveFileDialog.FileName);
                        MessageBox.Show("Game saved successfully to: " + saveFileDialog.FileName, "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (Board.CheckersGame != null)
                    {
                        Board.CheckersGame.SaveGame(saveFileDialog.FileName);
                        MessageBox.Show("Game saved successfully to: " + saveFileDialog.FileName, "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("No game context available to save or game not initialized.", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void loadGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "JSON Files (*.json)|*.json",
                DefaultExt = "json",
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string selectedFilePath = openFileDialog.FileName;

                    // Check if a game board already exists
                    if (Board == null)
                    {
                        // If no game board exists, create a new one
                        Board = new Board(GameType.Chess); // You can pass any game type here, as it doesn't matter for loading
                        Controls.Add(Board);
                    }

                    // Load the game context
                    Context loadedContext = null;
                    if (Board.ChessGame != null)
                    {
                        loadedContext = Board.ChessGame.LoadGame(selectedFilePath);
                    }
                    else if (Board.CheckersGame != null)
                    {
                        loadedContext = Board.CheckersGame.LoadGame(selectedFilePath);
                    }

                    if (loadedContext != null)
                    {
                        Board.Rescale(this.Width, this.Height, menuStrip1.Height);
                        Board.SetContext(loadedContext);
                        MessageBox.Show("Game loaded successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Failed to load the game.", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading game: {ex.Message}", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


    }
}
