using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Yogi
{
    class BestScores : Mode
    {
        private BestScores() { }

        private BestScores(PictureBox pictureBoxOnBitmap)
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
            lBestScore.Location = new System.Drawing.Point(pictureBoxOnBitmap.Width / 2 - 130, 40);
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
            lBestNameAll.Location = new System.Drawing.Point(pictureBoxOnBitmap.Width / 2 - 130, 70);
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
                removeControls();
                Game.getInstance().resume();
            };
            bBestScoreOk.ForeColor = System.Drawing.SystemColors.HotTrack;
        }

        const string fileName = "best_scores.txt";
      
        private Label lBestScore;
        private Label lBestNameAll;
        private Label lBestScoreAll;
        private Button bBestScoreOk;
        private PictureBox pictureBoxOnBitmap;
        private static BestScores instance;

        private void removeControls()
        {
            pictureBoxOnBitmap.Controls.Remove(bBestScoreOk);
            pictureBoxOnBitmap.Controls.Remove(lBestScore);
            pictureBoxOnBitmap.Controls.Remove(lBestScoreAll);
            pictureBoxOnBitmap.Controls.Remove(lBestNameAll);
        }

        private void addAtFrontControls()
        {
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

        public static BestScores getInstance(PictureBox pictureBoxOnBitmap)
        {
            if(instance == null)
                instance = new BestScores(pictureBoxOnBitmap);
            return instance;
        }

        public static BestScores getInstance()
        {
            if (instance == null)
                instance = new BestScores();
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
                String allData;
                using (StreamReader sr = new StreamReader(fileName))
                    allData = sr.ReadToEnd();

                // Parsowanie
                String[] scoreLines = allData.Split('\n');

                if (scoreLines.Length < 5)
                    throw new InvalidDataException();

                String[][] singleScore = new String[scoreLines.Length][];
                for (int i = 0; i < scoreLines.Length - 1; i++)
                {
                    singleScore[i] = scoreLines[i].Split('-');
                    if (singleScore[i].Length != 2)
                        throw new InvalidDataException();
                }

                // Zmiana
                int result;
                int n = singleScore.Length - 2;
                while (n >= 0)
                {
                    if(Int32.TryParse(singleScore[n][1], out result) == false)
                        throw new InvalidDataException();
                    if (Int32.Parse(singleScore[n][1]) >= score)
                    {
                        n++;
                        break;
                    }
                    n--;
                }

                // Zapisanie
                using (StreamWriter wr = new StreamWriter(fileName))
                {
                    int countAdded = 0, ifNew = 0;

                    // Przed
                    for (int i = 0; i < n; i++, countAdded++)
                    {
                        if (Int32.TryParse(singleScore[i][1], out result) == false)
                            throw new InvalidDataException();
                        wr.WriteLine("{0}-{1}", singleScore[i][0], Int32.Parse(singleScore[i][1]));
                    }

                    // Nowy
                    if (n < 5)
                    {
                        ifNew = 1;
                        wr.WriteLine("{0}-{1}", name, score);
                    }

                    // Po
                    for (int i = countAdded; i < 5 - ifNew; i++)
                    {
                        if (Int32.TryParse(singleScore[i][1], out result) == false)
                            throw new InvalidDataException();
                        wr.WriteLine("{0}-{1}", singleScore[i][0], Int32.Parse(singleScore[i][1]));
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
                String allData;
                using (StreamReader sr = new StreamReader(fileName))
                    allData = sr.ReadToEnd();

                String[] scoreLines = allData.Split('\n');
                String[] singleScore;
                String score = "";
                String name = "";
                int result;

                if (scoreLines.Length < 5)
                    throw new InvalidDataException();
                for (int i = 0; i < 5; i++)
                {
                    singleScore = scoreLines[i].Split('-');
                    if (singleScore.Length != 2 || Int32.TryParse(singleScore[1], out result) == false)
                        throw new InvalidDataException();
                    name += String.Format("{0}\n", singleScore[0]);
                    score += String.Format("-  {0}\n", singleScore[1]);
                }
                lBestNameAll.Text = String.Format(name);
                lBestScoreAll.Text = String.Format(score);
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

            addAtFrontControls();
        }
    }
}
