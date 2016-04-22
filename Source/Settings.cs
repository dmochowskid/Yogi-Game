using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yogi
{
    public partial class Settings : Form
    {
        public Keys lKey { get; internal set; } 
        public Keys rKey { get; internal set; }

        public Settings()
        {
            InitializeComponent();

            // My initialize
            lKey = Keys.Left;
            rKey = Keys.Right;
        }

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
    }
}
