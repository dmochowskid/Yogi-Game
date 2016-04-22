using System;
using System.Drawing;
using System.Windows.Forms;

namespace Yogi
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();

            pbGame.Size = new Size(ClientSize.Width, ClientSize.Height);

            bestScores = BestScores.getInstance(pbGame);

            game = Game.getInstance(pbGame);
            
            set = new Settings();
        }

        private Game game;
        private BestScores bestScores;
        private Settings set;
        
        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (game.startGame == true)
            {
                game.pause();
                DialogResult result = MessageBox.Show("Do you want to finish this game?", "New Game", MessageBoxButtons.OKCancel);
                if (result == DialogResult.Cancel)
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

            bestScores.display();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (game.startGame == true || game.pausedGame == true)
            {
                if(game.startGame == true)
                {
                    game.pause();
                    MessageBox.Show("You can not change settings during the game!");
                    game.resume();
                }
                else
                    MessageBox.Show("You can not change settings during the game!");
            }
            else
                set.ShowDialog();
        }

        private void keyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (game.startGame == true)
                game.move(e.KeyCode);
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
