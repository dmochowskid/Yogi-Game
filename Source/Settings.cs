using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Yogi
{
    public enum Level { EASY, MEDIUM, HARD };

    public partial class Settings : Form
    {
        /// <summary>
        /// Inicjalizacja, ustawienie domyslnych ustawien dotyczacych poziomu oraz sterowania
        /// </summary>
        public Settings()
        {
            InitializeComponent();
            
            level = Level.MEDIUM;
            lKey = Keys.Left;
            rKey = Keys.Right;

            changeActiveLevel(Level.MEDIUM);
        }

        public static Level level { get; private set; } // Poziom gry
        public static Keys lKey { get; private set; } // Przycisk do poruszania sie Yogim w lewo
        public static Keys rKey { get; private set; } // Przycisk do poruszania sie Yogim w prawo
        
        /// <summary>
        /// Zmiana sterowania w lewo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbLeft_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (((ComboBox)sender).Text)
                {
                    case "A": lKey = Keys.A;
                        break;
                    case "Left": lKey = Keys.Left;
                        break;
                    default:
                        throw new InvalidDataException();
                }
            }
            catch (InvalidDataException er)
            {
                MessageBox.Show(string.Format("Error description: {0}", er), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        /// <summary>
        /// Zmiana sterowania w prawo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbRight_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (((ComboBox)sender).Text)
                {
                    case "D":
                        rKey = Keys.D;
                        break;
                    case "Right":
                        rKey = Keys.Right;
                        break;
                    default:
                        throw new InvalidDataException();
                }
            }
            catch (InvalidDataException er)
            {
                MessageBox.Show(string.Format("Error description: {0}", er), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        /// <summary>
        /// Zamkniecie okna
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bOK_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        /// <summary>
        /// Zmiana koloru napisu na przycisku
        /// </summary>
        /// <param name="level">Rodzaj przycisku na ktormy ma zostac zmieniony kolor</param>
        /// <param name="newColor">Nowy kolor</param>
        private void setColor(Level level, System.Drawing.Color newColor)
        {
            switch (level)
            {
                case Level.EASY:
                    lEasy.ForeColor = newColor;
                    break;
                case Level.MEDIUM:
                    lMedium.ForeColor = newColor;
                    break;
                case Level.HARD:
                    lHard.ForeColor = newColor;
                    break;
                default:
                    throw new InvalidDataException();
            }
        }

        /// <summary>
        /// Zmiana poziomu gry
        /// </summary>
        /// <param name="newLebel">Nowy poziom gry</param>
        private void changeActiveLevel(Level newLebel)
        {
            try
            {
                setColor(level, Color.AliceBlue);
                level = newLebel;
                setColor(level, Color.SkyBlue);
            }
            catch (InvalidDataException e)
            {
                MessageBox.Show(string.Format("Error description: {0}", e), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
        
        private void lEasy_Click(object sender, EventArgs e)
        {
            changeActiveLevel(Level.EASY);
        }

        private void lMedium_Click(object sender, EventArgs e)
        {
            changeActiveLevel(Level.MEDIUM);
        }

        private void lHard_Click(object sender, EventArgs e)
        {
            changeActiveLevel(Level.HARD);
        }
    }
}
