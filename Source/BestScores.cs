using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yogi
{
    class BestScores
    {
        public BestScores(Size ClientSize, PictureBox pictureBoxOnBitmap)
        {
            // pictureBoxOnBitmap
            this.pictureBoxOnBitmap = pictureBoxOnBitmap;
            //
            // File
            //
            createFile();
            //
            // BestScores
            //
            lBestScore = new Label();
            lBestScore.Name = "lBestScore";
            lBestScore.BackColor = Color.Transparent;
            lBestScore.Location = new System.Drawing.Point(ClientSize.Width / 2 - 130, 40);
            lBestScore.Font = new Font("Showcard Gothic", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lBestScore.Text = System.String.Format("Best Score:");
            lBestScore.ForeColor = System.Drawing.SystemColors.HotTrack;
            lBestScore.AutoSize = true;
            //
            // BestScoresAll
            //
            lBestScoreAll = new Label();
            lBestScoreAll.Name = "lBestScoreAll";
            lBestScoreAll.BackColor = Color.Transparent;
            lBestScoreAll.Location = new System.Drawing.Point(ClientSize.Width / 2 - 130, 70);
            lBestScoreAll.Font = new Font("Times New Roman", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lBestScoreAll.ForeColor = System.Drawing.SystemColors.HotTrack;
            lBestScoreAll.AutoSize = true;
            // 
            // bBestScoreOk
            // 
            bBestScoreOk = new Button();
            this.bBestScoreOk.Location = new System.Drawing.Point(301, 203);
            this.bBestScoreOk.Name = "bBestScoreOk";
            this.bBestScoreOk.Size = new System.Drawing.Size(75, 23);
            this.bBestScoreOk.TabIndex = 2;
            this.bBestScoreOk.Text = "ok";
            this.bBestScoreOk.UseVisualStyleBackColor = true;
            this.bBestScoreOk.Click += (sender, e) =>
            {
                pictureBoxOnBitmap.Controls.Remove(bBestScoreOk);
                pictureBoxOnBitmap.Controls.Remove(lBestScore);
                pictureBoxOnBitmap.Controls.Remove(lBestScoreAll);
                game.resume();
            };
            bBestScoreOk.ForeColor = System.Drawing.SystemColors.HotTrack;
        }

        const string fileName = "best_scores.txt";
        string[] bestName = { "none", "none" };
        int[] bestScore = new int[2];
        private Label lBestScore;
        private Label lBestScoreAll;
        private Button bBestScoreOk;
        private PictureBox pictureBoxOnBitmap;
        private Game game;

        private void createFile()
        {
            if (File.Exists(fileName) == false)
            {
                using (StreamWriter wr = new StreamWriter(fileName))
                {
                    for (int i = 0; i < 5; i++)
                        wr.WriteLine("none-0");
                }
            }
        }

        public void updateScore(string name, int score)
        {
            // Wczytanie calego pliku
            String line;
            using (StreamReader sr = new StreamReader(fileName))
                line = sr.ReadToEnd();

            // Parsowanie
            String[] scoresTym = line.Split('\n');
            String[][] scores = new String[scoresTym.Length][];
            for (int i = 0; i < scoresTym.Length - 1; i++)
                scores[i] = scoresTym[i].Split('-');

            // Zmiana
            int n = scores.Length - 2;
            while (n >= 0)
            {
                if (Int32.Parse(scores[n][1]) >= score)
                {
                    n++;
                    break;
                }
                n--;
            }

            // Zapisanie
            using (StreamWriter wr = new StreamWriter(fileName))
            {
                int ilosc = 0, czyN = 0;

                // Przed
                for (int i = 0; i < n; i++, ilosc++)
                    wr.WriteLine("{0}-{1}", scores[i][0], Int32.Parse(scores[i][1]));

                // Nowy
                if (n < 5)
                {
                    ilosc++;
                    czyN = 1;
                    wr.WriteLine("{0}-{1}", name, score);
                }

                // Po
                for (int i = ilosc; i < 5; i++)
                    wr.WriteLine("{0}-{1}", scores[i - czyN][0], Int32.Parse(scores[i - czyN][1]));
            }
        }

        public void display(Game game)
        {
            this.game = game;

            String line;
            using (StreamReader sr = new StreamReader(fileName))
                line = sr.ReadToEnd();

            lBestScoreAll.Text = System.String.Format(line);
            
            pictureBoxOnBitmap.Controls.Add(lBestScore);
            pictureBoxOnBitmap.Controls.Add(bBestScoreOk);
            pictureBoxOnBitmap.Controls.Add(lBestScoreAll);
            lBestScoreAll.BringToFront();
            lBestScore.BringToFront();
            bBestScoreOk.BringToFront();
        }
    }
}
