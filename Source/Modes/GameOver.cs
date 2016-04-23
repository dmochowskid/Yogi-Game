using System.Drawing;
using System.Windows.Forms;

namespace Yogi
{
    class GameOver : Mode
    {
        private GameOver() { }

        private GameOver(PictureBox mainPictureBox)
        {
            //
            // lGameOver
            //
            lGameOver = new Label();
            lGameOver.Text = "Game Over";
            lGameOver.Name = "lGameOver";
            lGameOver.BackColor = Color.Transparent;
            lGameOver.Size = new Size(200, 29);
            lGameOver.Location = new Point(mainPictureBox.Size.Width / 2 - 72, 100);
            lGameOver.Font = new Font("Showcard Gothic", 18F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            lGameOver.ForeColor = SystemColors.HotTrack;
            // 
            // tbName
            // 
            tbName = new TextBox();
            tbName.Location = new Point(289, 159);
            tbName.Name = "tbName";
            tbName.Size = new Size(100, 20);
            tbName.MaxLength = 15;
            // 
            // bSubmit
            // 
            bSubmit = new Button();
            bSubmit.Location = new Point(301, 203);
            bSubmit.Name = "bSubbmit";
            bSubmit.Size = new Size(75, 23);
            bSubmit.TabIndex = 2;
            bSubmit.Text = "Submit";
            bSubmit.UseVisualStyleBackColor = true;
            bSubmit.ForeColor = SystemColors.HotTrack;
            bSubmit.Click += (sender, e) =>
            {
                BestScores.getInstance().updateScore(tbName.Text, Game.getInstance().score.points);
                removeControls();
            };

            this.mainPictureBox = mainPictureBox;
        }

        private Label lGameOver; // etykieta z napisem 'Game Over'
        private TextBox tbName; // Miejsce na podanie nazwy rekordzisty
        private Button bSubmit; // Przycisk sluzacy do dodania rekordu do listy
        private PictureBox mainPictureBox;
        private static GameOver instance;

        public static GameOver getInstance(PictureBox mainPictureBox)
        {
            if (instance == null)
                instance = new GameOver(mainPictureBox);
            return instance;
        }
        
        public static GameOver getInstance()
        {
            return instance;
        }
        
        public void display()
        {
            tbName.Text = "";

            addControls();
        }

        /// <summary>
        /// Dodanie wszystkich kontrolek nalezacych do GameOver
        /// </summary>
        private void addControls()
        {
            mainPictureBox.Controls.Add(lGameOver);
            mainPictureBox.Controls.Add(bSubmit);
            mainPictureBox.Controls.Add(tbName);
        }

        /// <summary>
        /// Usuniecie wszystkich kontrolek nalezacych do GameOver
        /// </summary>
        private void removeControls()
        {
            mainPictureBox.Controls.Remove(lGameOver);
            mainPictureBox.Controls.Remove(bSubmit);
            mainPictureBox.Controls.Remove(tbName);
        }
    }
}
