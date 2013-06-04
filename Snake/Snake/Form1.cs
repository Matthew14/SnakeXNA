using System;
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

        private void Form1_FormClosing(Object sender, FormClosingEventArgs e)
        {
            //Make sure the game quits too
            Program.game.Exit();
        }

        private void newGameButton_Click(object sender, EventArgs e)
        {
            Program.game.NewGame();
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newGameButton_Click(sender, e);
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
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
