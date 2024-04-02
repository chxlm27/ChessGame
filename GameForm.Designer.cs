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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 800);
            this.Text = "Game Form";
            this.Resize += GameForm_Resize; // Subscribe to the form's Resize event

            // Create an instance of the Board control
            Chess.Board board1 = new Chess.Board();

            // Add the Board control to the form's controls collection
            this.Controls.Add(board1);

            // Call the method to center the Board control initially
            CenterBoard();
        }

        // Event handler for the form's Resize event
        private void GameForm_Resize(object sender, EventArgs e)
        {
            // Call the method to center the Board control whenever the form is resized
            CenterBoard();
        }


        // Method to center the Board control within the form
        private void CenterBoard()
        {
            foreach (Control control in Controls)
            {
                if (control is Chess.Board)
                {
                    Chess.Board board = (Chess.Board)control;

                    // Calculate the center position for the Board control
                    int boardX = (this.ClientSize.Width - board.Width) / 2;
                    int boardY = (this.ClientSize.Height - board.Height) / 2;

                    // Add limits to ensure all rows and columns are visible
                    boardX = Math.Max(0, boardX);
                    boardY = Math.Max(0, boardY);

                    // Set the position of the Board control
                    board.Location = new System.Drawing.Point(boardX, boardY);
                }
            }
        }

        #endregion
    }
}
