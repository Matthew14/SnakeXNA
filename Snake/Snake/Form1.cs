using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
        }

        public IntPtr getDrawSurface()
        {
            return pctSurface.Handle;
        }
        
        public ToolStripLabel getScoreLabel()
        {
            return scoreLabel;
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        
        private void Form1_FormClosing(Object sender, FormClosingEventArgs e)
        {

            Program.game.Exit();
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void newGameButton_Click(object sender, EventArgs e)
        {
            Program.game.NewGame();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.game.NewGame();
        }

        private void gameToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.game.paused = !Program.game.paused;
            pauseToolStripMenuItem.Text = pauseToolStripMenuItem.Text == "Pause" ? "Unpause" : "Pause";

        }

        private void pctSurface_Click(object sender, EventArgs e)
        {
            pauseToolStripMenuItem_Click(sender, e);
        }
    }
}
