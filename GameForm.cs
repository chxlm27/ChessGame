using System;
using System.Windows.Forms;
using System.IO;

namespace Chess
{
    public partial class GameForm : Form
    {
        private ChessGame Game { get; set; }
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

        private void beginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
            if (Board == null)
            {
                Board = new Board();
                Controls.Add(Board);
            }

            if (Game == null)
            {
                Game = new ChessGame();
                Game.Initialize(Board);
            }

            Board.Initialize();
            Game.Initialize(Board);
            Board.ChessGame = Game; // Ensure Board has reference to ChessGame
            Board.GameContext = Game.GameContext;
            Board.Rescale(this.Width, this.Height, menuStrip1.Height);
            Game.Start();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Board != null && Board.ChessGame != null && Board.ChessGame.GameContext != null)
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
                    Board.ChessGame.SaveGame(saveFileDialog.FileName);
                    MessageBox.Show("Game saved successfully to: " + saveFileDialog.FileName, "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("No game context available to save or Chess Game not initialized.", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loadGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "JSON Files (*.json)|*.json",
                DefaultExt = "json",
                InitialDirectory = @"F:\IT Perspectives\"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string selectedFilePath = openFileDialog.FileName;
                    if (Board == null)
                    {
                        Board = new Board();
                        Controls.Add(Board);
                    }

                    if (Game == null)
                    {
                        Game = new ChessGame();
                    }

                    Context loadedContext = Game.LoadGame(selectedFilePath);
                    if (loadedContext != null)
                    {
                        Board.Rescale(this.Width, this.Height, menuStrip1.Height);
                        Board.SetContext(loadedContext);  // Ensure the board is updated with the loaded game contex
                        MessageBox.Show("Game loaded successfully.");
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
