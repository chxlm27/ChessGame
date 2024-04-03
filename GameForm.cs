using Chess;
using System.Windows.Forms;
using System;

namespace ChessGame
{
    public partial class GameForm : Form
    {
        private Board board;
        public GameForm()
        {
            InitializeComponent();
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        { 
            this.Close(); 
        }
        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            board = new Board();
            this.Controls.Add(board);
            AGame chessGame = new ChessGame();
            chessGame.Initialize(board);
            chessGame.Start();
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (board != null)
                board.RescaleBoard(this.Width, this.Height);
        }
    }
}
