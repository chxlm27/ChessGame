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
            board?.Rescale(this.Width, this.Height);
        }
    }
}
