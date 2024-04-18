using System;
using System.Windows.Forms;
using System.IO;

namespace Chess
{
    public partial class GameForm : Form
    {
        private ChessGame Game;

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
            if (Game == null)
            {
                Game = new ChessGame();
                Board board = new Board(); // Instantiate Board here
                board.Initialize();
                Game.Initialize(board); // Pass the instantiated Board to ChessGame
                Controls.Add(board); // Add board to controls list
            }

            Game.GetBoard()?.Rescale(this.Width, this.Height, menuStrip1.Height);
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