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
            if (Board != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "JSON Files (*.json)|*.json",
                    DefaultExt = "json",
                    AddExtension = true
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Board.SaveGame(saveFileDialog.FileName);
                    MessageBox.Show("Game saved successfully.");
                }
            }
            else
            {
                MessageBox.Show("No board initialized to save.", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loadGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "JSON Files (*.json)|*.json"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (Board == null)
                {
                    Board = new Board();
                    Controls.Add(Board);
                }
                try
                {
                    Board.LoadGame(openFileDialog.FileName);
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
