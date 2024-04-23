using System;
using System.Windows.Forms;
using System.IO;

namespace Chess
{
    public partial class GameForm : Form
    {
        private ChessGame Game { get; set; }
        private Board Board { get; set; }

        public GameForm()
        {
            InitializeComponent();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Board?.Rescale(this.Width, this.Height, menuStrip1.Height);
        }

        private void beginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
            if (Board == null)
            {
                Board = new Board();
                Controls.Add(Board); // Ensure the board is added only once
            }

            if (Game == null)
                Game = new ChessGame();

            Board.Initialize();
            Game.Initialize(Board);
            Board.Rescale(this.Width, this.Height, menuStrip1.Height);
            Game.Start();
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
                    InitialDirectory = @"F:\IT Perspectives\", // Default directory
                    FileName = "Game_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".json" // Suggest a default filename with timestamp
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Path of the current game state file
                        string sourceFilePath = Path.Combine(@"F:\IT Perspectives\", "current_game.json");
                        // Path where the user wants to save the copy
                        string destinationFilePath = saveFileDialog.FileName;

                        // Copy the current game state file to the new location
                        File.Copy(sourceFilePath, destinationFilePath, true);

                        MessageBox.Show("Game saved successfully to: " + destinationFilePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error saving game: {ex.Message}", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("No game in progress to save.", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void loadGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (Board == null)
                {
                    Board = new Board();
                    Controls.Add(Board);
                }
                Board.LoadGame(); // Directly load the game
                MessageBox.Show("Game loaded successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading game: {ex.Message}", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
