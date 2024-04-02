using Chess;
using System;
using System.Windows.Forms;

namespace ChessGame
{
    partial class GameForm : Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.board = new Board(); // Create the chess board
            this.Controls.Add(this.board); // Add the chess board to the form
            //this.ClientSize = new System.Drawing.Size(800, 800); // Set the form size
            this.SetBounds(0, 0, 800, 600);
            this.Text = "Chess Game"; // Set the form title
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>

        #endregion
    }
}
