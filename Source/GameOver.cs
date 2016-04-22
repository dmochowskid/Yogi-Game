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
    class GameOver
    {
        public GameOver(Size gameWindowSize, PictureBox mainPictureBox, BestScores bestScores)
        {
            this.mainPictureBox = mainPictureBox;
            //
            // lGameOver
            //
            lGameOver = new Label();
            lGameOver.Text = "Game Over";
            lGameOver.Name = "lGameOver";
            lGameOver.BackColor = Color.Transparent;
            lGameOver.Size = new Size(200, 29);
            lGameOver.Location = new Point(gameWindowSize.Width / 2 - 72, 100);
            lGameOver.Font = new Font("Showcard Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lGameOver.ForeColor = System.Drawing.SystemColors.HotTrack;
            // 
            // tbName
            // 
            tbName = new TextBox();
            this.tbName.Location = new System.Drawing.Point(289, 159);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(100, 20);
            // 
            // bSubmit
            // 
            bSubmit = new Button();
            this.bSubmit.Location = new System.Drawing.Point(301, 203);
            this.bSubmit.Name = "bSubbmit";
            this.bSubmit.Size = new System.Drawing.Size(75, 23);
            this.bSubmit.TabIndex = 2;
            this.bSubmit.Text = "Submit";
            this.bSubmit.UseVisualStyleBackColor = true;
            this.bSubmit.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.bSubmit.Click += (sender, e) =>
            {
                bestScores.updateScore(tbName.Text, score.points);
                mainPictureBox.Controls.Remove(lGameOver);
                mainPictureBox.Controls.Remove(bSubmit);
                mainPictureBox.Controls.Remove(tbName);
            };
        }

        private Label lGameOver;
        private TextBox tbName;
        private Button bSubmit;
        private PictureBox mainPictureBox;
        private Score score;

        public void disply(Score currentScore)
        {
            tbName.Text = "";
            score = currentScore;
            mainPictureBox.Controls.Add(lGameOver);
            mainPictureBox.Controls.Add(bSubmit);
            mainPictureBox.Controls.Add(tbName);
        }
    }
}
