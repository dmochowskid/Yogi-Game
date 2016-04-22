using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yogi
{
    public partial class MainWindow : Form
    {
        private Game game;
        private BestScores bestScores;
        private Settings set;

        public MainWindow()
        { 
            InitializeComponent();

            bestScores = new BestScores(ClientSize, pbGame);

            game = new Game(ClientSize, pbGame, bestScores);

            pbGame.Size = new System.Drawing.Size(ClientSize.Width, ClientSize.Height);

            set = new Settings();
            set.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (game.startGame == true)
            {
                game.pause();
                DialogResult result = MessageBox.Show("Do you want to finish this game?", "New Game", MessageBoxButtons.OKCancel);
                if (result == System.Windows.Forms.DialogResult.Cancel)
                {
                    game.resume();
                    return;
                }
            }

            pbGame.Focus();

            pbGame.Controls.Clear();
            game.display();
        }
        
        private void bestScoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (game.startGame == true)
                game.pause();

            if(game.pausedGame == false)
                pbGame.Controls.Clear();

            bestScores.display(game);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (game.startGame == true || game.pausedGame == true)
            {
                game.pause();
                MessageBox.Show("You can not change settings during the game!");
                game.resume();
            }
            else
                set.ShowDialog();
        }

        private void keyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (game.startGame == true)
            {
                game.move(e.KeyCode);
            }
            if (e.KeyCode == Keys.Space)
            {
                if (game.pausedGame == true)
                    game.resume();
                else if (game.startGame == true)
                    game.pause();
            }
        }
    }
}
