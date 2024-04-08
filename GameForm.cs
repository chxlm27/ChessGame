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

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            board?.Rescale(this.Width, this.Height, menuStrip1.Height);
        }

        private void beginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            board = new Board();
            board.Initialize();
            Controls.Add(board);
            board.Rescale(this.Width, this.Height, menuStrip1.Height);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
