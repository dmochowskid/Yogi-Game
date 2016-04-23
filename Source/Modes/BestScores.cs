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
            //
            // pictureBoxOnBitmap
            //
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
            lBestScore.Location = new Point(pictureBoxOnBitmap.Width / 2 - 130, 40);
            lBestScore.Font = new Font("Showcard Gothic", 16F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            lBestScore.Text = String.Format("Best Score:");
            lBestScore.ForeColor = SystemColors.HotTrack;
            lBestScore.AutoSize = true;
            //
            // BestNameAll
            //
            lBestNameAll = new Label();
            lBestNameAll.Name = "lBestNameAll";
            lBestNameAll.BackColor = Color.Transparent;
            lBestNameAll.Location = new Point(pictureBoxOnBitmap.Width / 2 - 130, 70);
            lBestNameAll.Font = new Font("Times New Roman", 16F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            lBestNameAll.ForeColor = SystemColors.HotTrack;
            lBestNameAll.AutoSize = true;
            //
            // BestScoresAll
            //
            lBestScoreAll = new Label();
            lBestScoreAll.Name = "lBestScoreAll";
            lBestScoreAll.BackColor = Color.Transparent;
            lBestScoreAll.Font = new Font("Times New Roman", 16F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            lBestScoreAll.ForeColor = SystemColors.HotTrack;
            lBestScoreAll.AutoSize = true;
            // 
            // bBestScoreOk
            // 
            bBestScoreOk = new Button();
            bBestScoreOk.Location = new Point(301, 203);
            bBestScoreOk.Name = "bBestScoreOk";
            bBestScoreOk.Size = new Size(75, 23);
            bBestScoreOk.TabIndex = 2;
            bBestScoreOk.Text = "ok";
            bBestScoreOk.UseVisualStyleBackColor = true;
            bBestScoreOk.Click += (sender, e) =>
            {
                removeControls();
                Game.getInstance().resume();
            };
            bBestScoreOk.ForeColor = SystemColors.HotTrack;
        }

        const string fileName = "best_scores.txt"; // Nazwa pliku z rekordami
      
        private Label lBestScore; // etykieta z napisem 'Best Score'
        private Label lBestNameAll; // etykieta z napisem ze wszystkimi imionami rekordzistow
        private Label lBestScoreAll; // etykieta z napisem ze wszystkimi wynikami rekordzistow
        private Button bBestScoreOk; // Przycisk 'OK' do zamkniecia BestScores
        private PictureBox pictureBoxOnBitmap;
        private static BestScores instance;

        public static BestScores getInstance(PictureBox pictureBoxOnBitmap)
        {
            if (instance == null)
                instance = new BestScores(pictureBoxOnBitmap);
            return instance;
        }

        public static BestScores getInstance()
        {
            return instance;
        }

        /// <summary>
        /// Usuniecie wszystkich kontrolek nalezacych do BestScores
        /// </summary>
        private void removeControls()
        {
            pictureBoxOnBitmap.Controls.Remove(bBestScoreOk);
            pictureBoxOnBitmap.Controls.Remove(lBestScore);
            pictureBoxOnBitmap.Controls.Remove(lBestScoreAll);
            pictureBoxOnBitmap.Controls.Remove(lBestNameAll);
        }

        /// <summary>
        /// Dodanie wszystkich kontrolek nalezacych do BestScores
        /// Wyswietlenie ich przed wszystkimi innymi elementami gry
        /// </summary>
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
        
        /// <summary>
        /// Jezeli nie ma pliku z rekordami to utworzy nowy z domyslnymi danymi
        /// </summary>
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

        /// <summary>
        /// Dodanie nowego rekordu do listy
        /// </summary>
        /// <param name="name">Nazwa nowego rekordzisty</param>
        /// <param name="score">Wynik nowego rekordzisty</param>
        public void updateScore(string name, int score)
        {
            try
            { 
                // Wczytanie calego pliku
                String allData;
                using (StreamReader sr = new StreamReader(fileName))
                    allData = sr.ReadToEnd();

                // Pojedyncze linie
                String[] scoreLines = allData.Split('\n'); 
                if (scoreLines.Length < 5)
                    throw new InvalidDataException();

                // Pojedyncze rekordy
                String[][] singleScore = new String[scoreLines.Length][];
                for (int i = 0; i < scoreLines.Length - 1; i++)
                {
                    singleScore[i] = scoreLines[i].Split('-');
                    if (singleScore[i].Length != 2)
                        throw new InvalidDataException();
                }

                // znalezienie pozycji na nowy rekord
                int result; // Wynik proby parsowania
                int n = singleScore.Length - 2; // Pozycja na nowy rekord
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
                    int currentAdded = 0; // Ilosc aktualnie dodanych rekordzistow
                    int ifNew = 0; // Czy zostal dodany nowy rekordzista

                    // Przed nowym rekordem
                    for (int i = 0; i < n; i++, currentAdded++)
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

                    // Po nowym rekordzie
                    for (int i = currentAdded; i < 5 - ifNew; i++)
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

        /// <summary>
        /// Wczytanie listy rekordow i dodanie ich do dodanie ich do odpowiednich etykiet
        /// </summary>
        private void readScores()
        {
            try
            {
                String allData; // Caly plik
                using (StreamReader sr = new StreamReader(fileName))
                    allData = sr.ReadToEnd();

                String[] scoreLines = allData.Split('\n'); // Pojedyncze linie
                String[] singleScore; // Pojedynczy wynik
                String score = ""; // Napis ze wszytkimi wynikami rekordzistow
                String name = ""; // Napis ze wszystkimi nazwami rekordzistow
                int result; // Wynik proby parsowania

                if (scoreLines.Length < 5)
                    throw new InvalidDataException();

                for (int i = 0; i < 5; i++)
                {
                    singleScore = scoreLines[i].Split('-');

                    if (singleScore.Length != 2 || Int32.TryParse(singleScore[1], out result) == false)
                        throw new InvalidDataException();

                    // Dodanie nowego rekordu
                    name += String.Format("{0}\n", singleScore[0]);
                    score += String.Format("-  {0}\n", singleScore[1]);
                }

                // Dodanie rekordow do etykiet
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
