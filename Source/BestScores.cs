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
    class BestScores : Module
    {
        private BestScores() { }

        private BestScores(Size ClientSize, PictureBox pictureBoxOnBitmap)
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
            // BestNameAll
            //
            lBestNameAll = new Label();
            lBestNameAll.Name = "lBestNameAll";
            lBestNameAll.BackColor = Color.Transparent;
            lBestNameAll.Location = new System.Drawing.Point(ClientSize.Width / 2 - 130, 70);
            lBestNameAll.Font = new Font("Times New Roman", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lBestNameAll.ForeColor = System.Drawing.SystemColors.HotTrack;
            lBestNameAll.AutoSize = true;
            //
            // BestScoresAll
            //
            lBestScoreAll = new Label();
            lBestScoreAll.Name = "lBestScoreAll";
            lBestScoreAll.BackColor = Color.Transparent;
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
                pictureBoxOnBitmap.Controls.Remove(lBestNameAll);
                Game.getInstance().resume();
            };
            bBestScoreOk.ForeColor = System.Drawing.SystemColors.HotTrack;
        }

        const string fileName = "best_scores.txt";
      
        string[] bestName = { "none", "none" };
        int[] bestScore = new int[2];
        private Label lBestScore;
        private Label lBestNameAll;
        private Label lBestScoreAll;
        private Button bBestScoreOk;
        private PictureBox pictureBoxOnBitmap;
        private static BestScores instance;

        public static BestScores getInstance(Size ClientSize, PictureBox pictureBoxOnBitmap)
        {
            if(instance == null)
            {
                instance = new BestScores(ClientSize, pictureBoxOnBitmap);
            }
            return instance;
        }

        public static BestScores getInstance()
        {
            if (instance == null)
            {
                instance = new BestScores();
            }
            return instance;
        }

        private void createFile()
        {
            if (File.Exists(fileName) == false)
            {
                using (StreamWriter wr = new StreamWriter(fileName))
                {
                    wr.WriteLine("Mirek-10000");
                    wr.WriteLine("Stefan-6000");
                    wr.WriteLine("Jan-4000");
                    wr.WriteLine("Andrzej-2000");
                    wr.WriteLine("Seba-1000");
                }
            }
        }

        public void updateScore(string name, int score)
        {
            try
            { 
                // Wczytanie calego pliku
                String line;
                using (StreamReader sr = new StreamReader(fileName))
                    line = sr.ReadToEnd();

                // Parsowanie
                String[] scoresTym = line.Split('\n');
                if (scoresTym.Length < 5)
                    throw new InvalidDataException();
                String[][] scores = new String[scoresTym.Length][];
                for (int i = 0; i < scoresTym.Length - 1; i++)
                {
                    scores[i] = scoresTym[i].Split('-');
                    if (scores[i].Length != 2)
                        throw new InvalidDataException();
                }

                // Zmiana
                int result;
                int n = scores.Length - 2;
                while (n >= 0)
                {
                    if(Int32.TryParse(scores[n][1], out result) == false)
                        throw new InvalidDataException();
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
                    {
                        if (Int32.TryParse(scores[i][1], out result) == false)
                            throw new InvalidDataException();
                        wr.WriteLine("{0}-{1}", scores[i][0], Int32.Parse(scores[i][1]));
                    }

                    // Nowy
                    if (n < 5)
                    {
                        ilosc++;
                        czyN = 1;
                        wr.WriteLine("{0}-{1}", name, score);
                    }

                    // Po
                    for (int i = ilosc; i < 5; i++)
                    {
                        if (Int32.TryParse(scores[i - czyN][1], out result) == false)
                            throw new InvalidDataException();
                        wr.WriteLine("{0}-{1}", scores[i - czyN][0], Int32.Parse(scores[i - czyN][1]));
                    }
                }
            }
            catch (InvalidDataException e)
            {
                MessageBox.Show(string.Format("Error description: {0}", e), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void readScores()
        {
            try
            {
                String lines;
                using (StreamReader sr = new StreamReader(fileName))
                    lines = sr.ReadToEnd();

                String[] line = lines.Split('\n');

                String[] singleScore;
                String tekst = "";
                String name = "";
                int result;
                if (line.Length < 5)
                    throw new InvalidDataException();
                for (int i = 0; i < 5; i++)
                {
                    singleScore = line[i].Split('-');
                    if (singleScore.Length != 2 || Int32.TryParse(singleScore[1], out result) == false)
                        throw new InvalidDataException();
                    tekst += String.Format("{0}\n", singleScore[0]);
                    name += String.Format("-  {0}\n", singleScore[1]);
                }
                lBestNameAll.Text = String.Format(tekst);
                lBestScoreAll.Text = String.Format(name);
            }
            catch (InvalidDataException e)
            {
                MessageBox.Show(string.Format("Error description: {0}", e), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        public void display()
        {
            readScores();

            pictureBoxOnBitmap.Controls.Add(lBestNameAll);
            lBestScoreAll.Location = new System.Drawing.Point(lBestNameAll.Location.X + lBestNameAll.Size.Width, 70);
            pictureBoxOnBitmap.Controls.Add(lBestScore);
            pictureBoxOnBitmap.Controls.Add(bBestScoreOk);
            pictureBoxOnBitmap.Controls.Add(lBestScoreAll);

            lBestScoreAll.BringToFront();
            lBestScore.BringToFront();
            bBestScoreOk.BringToFront();
            lBestNameAll.BringToFront();
        }
    }
}
