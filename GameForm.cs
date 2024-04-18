using System;
using System.Windows.Forms;
using System.IO;

namespace Chess
{
    public partial class GameForm : Form
    {
        private ChessGame Game; // Corrected variable name

        public GameForm()
        {
            InitializeComponent();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            var board = Game?.GetBoard();
            board?.Rescale(this.Width, this.Height, menuStrip1.Height);
        }

        private void beginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var board = Game?.GetBoard(); // Get the board from ChessGame

            if (board == null)
            {
                Game = new ChessGame();
                board = new Board();
                board.Initialize();
                Game.Initialize(board);

                Controls.Add(board); // Add board to controls list
            }

            board.Rescale(this.Width, this.Height, menuStrip1.Height);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chessGame?.SaveGame();
        }

        private void loadGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chessGame?.LoadGame();
        }

        private string GetSaveFilePath()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            return Path.Combine(currentDirectory, "saved_game.dat");
        }
    }
}
