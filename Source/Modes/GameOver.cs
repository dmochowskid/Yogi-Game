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
            lGameOver.Font = new Font("Showcard Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lGameOver.ForeColor = System.Drawing.SystemColors.HotTrack;
            // 
            // tbName
            // 
            tbName = new TextBox();
            this.tbName.Location = new System.Drawing.Point(289, 159);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(100, 20);
            this.tbName.MaxLength = 15;
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
                BestScores.getInstance().updateScore(tbName.Text, Game.getInstance().score.points);
                removeControls();
            };

            this.mainPictureBox = mainPictureBox;
        }

        private Label lGameOver;
        private TextBox tbName;
        private Button bSubmit;
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
            if (instance == null)
                instance = new GameOver();
            return instance;
        }
        
        public void display()
        {
            tbName.Text = "";

            addControls();
        }

        private void addControls()
        {
            mainPictureBox.Controls.Add(lGameOver);
            mainPictureBox.Controls.Add(bSubmit);
            mainPictureBox.Controls.Add(tbName);
        }

        private void removeControls()
        {
            mainPictureBox.Controls.Remove(lGameOver);
            mainPictureBox.Controls.Remove(bSubmit);
            mainPictureBox.Controls.Remove(tbName);
        }
    }
}
