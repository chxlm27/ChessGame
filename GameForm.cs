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
                Controls.Add(Board);
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
                    InitialDirectory = @"F:\IT Perspectives\",
                    FileName = "Game_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".json" 
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                     
                        string sourceFilePath = Path.Combine(@"F:\IT Perspectives\", "current_game.json");
                    
                        string destinationFilePath = saveFileDialog.FileName;

            
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
                    Board.GameContext = chessGame.LoadGame(selectedFilePath);

                    Board.Rescale(this.Width, this.Height, menuStrip1.Height);
                    MessageBox.Show("Game loaded successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading game: {ex.Message}", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


    }
}
