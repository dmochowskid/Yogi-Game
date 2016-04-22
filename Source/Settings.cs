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
    public enum Level { EASY, MEDIUM, HARD };

    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();

            // My initialize
            level = Level.MEDIUM;
            changeActiveLevel(Level.MEDIUM);
            lKey = Keys.Left;
            rKey = Keys.Right;
        }

        public static Level level { get; private set; }
        public static Keys lKey { get; private set; }
        public static Keys rKey { get; private set; }
        
        private void cbLeft_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (((ComboBox)sender).Text)
            {
                case "L": lKey = Keys.L;
                    break;
                case "Left": lKey = Keys.Left;
                    break;
                default:
                    throw new InvalidCastException();
            }
        }

        private void cbRight_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (((ComboBox)sender).Text)
            {
                case "R":
                    rKey = Keys.R;
                    break;
                case "Right":
                    rKey = Keys.Right;
                    break;
                default:
                    throw new InvalidCastException();
            }
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void setColor(Level level, System.Drawing.Color newColor)
        {
            switch (level)
            {
                case Level.EASY: lEasy.ForeColor = newColor;
                    break;
                case Level.MEDIUM: lMedium.ForeColor = newColor;
                    break;
                case Level.HARD: lHard.ForeColor = newColor;
                    break;
                default:
                    throw new InvalidCastException();
            }
        }

        private void changeActiveLevel(Level newLebel)
        {
            setColor(level, Color.AliceBlue);
            level = newLebel;
            setColor(level, Color.SkyBlue);
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
