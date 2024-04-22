﻿using System;
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
            Board = new Board();
            Game = new ChessGame();

            Board.Initialize();
            Game.Initialize(Board);

            Controls.Add(Board);
            Board.Rescale(this.Width, this.Height, menuStrip1.Height);

            Game.Start();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Game != null)
            {
                Game.Save();
            }
            else
            {
                MessageBox.Show("No game initialized to save.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loadGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Game == null)
            {
                Game = new ChessGame(); // Create a new instance of ChessGame if one doesn't exist
            }

            try
            {
                Game.Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading game: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
