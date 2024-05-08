using Gamee.Checkers;
using Gamee.Chess;
using System.Windows.Forms;

namespace ChessGameApp
{
    partial class GameForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private ChessGame chessGame;
        private CheckersGame checkersGame; // Added for Checkers Game
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.beginChessGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.beginCheckersGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem(); // Added for Checkers Game
            this.saveGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gameToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(284, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // gameToolStripMenuItem
            // 
            this.gameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.beginChessGameToolStripMenuItem,
            this.beginCheckersGameToolStripMenuItem, // Added for Checkers Game
            this.saveGameToolStripMenuItem,
            this.loadGameToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
            this.gameToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.gameToolStripMenuItem.Text = "&Game";
            // 
            // beginChessGameToolStripMenuItem
            // 
            this.beginChessGameToolStripMenuItem.Name = "beginChessGameToolStripMenuItem";
            this.beginChessGameToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.beginChessGameToolStripMenuItem.Text = "&Chess Game";
            this.beginChessGameToolStripMenuItem.Click += new System.EventHandler(this.beginChessGameToolStripMenuItem_Click);
            // 
            // beginCheckersGameToolStripMenuItem
            // 
            this.beginCheckersGameToolStripMenuItem.Name = "beginCheckersGameToolStripMenuItem";
            this.beginCheckersGameToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.beginCheckersGameToolStripMenuItem.Text = "&Checkers Game"; // Added for Checkers Game
            this.beginCheckersGameToolStripMenuItem.Click += new System.EventHandler(this.beginCheckersGameToolStripMenuItem_Click); // Added for Checkers Game
            // 
            // saveGameToolStripMenuItem
            // 
            this.saveGameToolStripMenuItem.Name = "saveGameToolStripMenuItem";
            this.saveGameToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveGameToolStripMenuItem.Text = "&Save Game";
            this.saveGameToolStripMenuItem.Click += new System.EventHandler(this.saveGameToolStripMenuItem_Click);
            // 
            // loadGameToolStripMenuItem
            // 
            this.loadGameToolStripMenuItem.Name = "loadGameToolStripMenuItem";
            this.loadGameToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.loadGameToolStripMenuItem.Text = "&Load Game";
            this.loadGameToolStripMenuItem.Click += new System.EventHandler(this.loadGameToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // GameForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GameForm";
            this.Text = "Game Form";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem gameToolStripMenuItem;
        private ToolStripMenuItem beginChessGameToolStripMenuItem;
        private ToolStripMenuItem beginCheckersGameToolStripMenuItem; // Added for Checkers Game
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem saveGameToolStripMenuItem;
        private ToolStripMenuItem loadGameToolStripMenuItem;
    }
}
