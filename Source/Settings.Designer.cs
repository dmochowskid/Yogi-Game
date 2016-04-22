namespace Yogi
{
    partial class Settings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lKeys = new System.Windows.Forms.Label();
            this.lLeft = new System.Windows.Forms.Label();
            this.lRight = new System.Windows.Forms.Label();
            this.cbLeft = new System.Windows.Forms.ComboBox();
            this.cbRight = new System.Windows.Forms.ComboBox();
            this.bOK = new System.Windows.Forms.Button();
            this.lGameLevel = new System.Windows.Forms.Label();
            this.lEasy = new System.Windows.Forms.Label();
            this.lMedium = new System.Windows.Forms.Label();
            this.lHard = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lKeys
            // 
            this.lKeys.AutoSize = true;
            this.lKeys.BackColor = System.Drawing.Color.Transparent;
            this.lKeys.Font = new System.Drawing.Font("Showcard Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lKeys.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lKeys.Location = new System.Drawing.Point(79, 9);
            this.lKeys.Name = "lKeys";
            this.lKeys.Size = new System.Drawing.Size(73, 30);
            this.lKeys.TabIndex = 0;
            this.lKeys.Text = "Keys";
            // 
            // lLeft
            // 
            this.lLeft.AutoSize = true;
            this.lLeft.Font = new System.Drawing.Font("Showcard Gothic", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lLeft.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lLeft.Location = new System.Drawing.Point(5, 42);
            this.lLeft.Name = "lLeft";
            this.lLeft.Size = new System.Drawing.Size(47, 21);
            this.lLeft.TabIndex = 1;
            this.lLeft.Text = "Left";
            // 
            // lRight
            // 
            this.lRight.AutoSize = true;
            this.lRight.Font = new System.Drawing.Font("Showcard Gothic", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lRight.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lRight.Location = new System.Drawing.Point(5, 69);
            this.lRight.Name = "lRight";
            this.lRight.Size = new System.Drawing.Size(63, 21);
            this.lRight.TabIndex = 2;
            this.lRight.Text = "Right";
            // 
            // cbLeft
            // 
            this.cbLeft.FormattingEnabled = true;
            this.cbLeft.Items.AddRange(new object[] {
            "Left",
            "A"});
            this.cbLeft.Location = new System.Drawing.Point(72, 42);
            this.cbLeft.Name = "cbLeft";
            this.cbLeft.Size = new System.Drawing.Size(121, 21);
            this.cbLeft.TabIndex = 3;
            this.cbLeft.Text = "Left";
            this.cbLeft.SelectedIndexChanged += new System.EventHandler(this.cbLeft_SelectedIndexChanged);
            // 
            // cbRight
            // 
            this.cbRight.FormattingEnabled = true;
            this.cbRight.Items.AddRange(new object[] {
            "Right",
            "D"});
            this.cbRight.Location = new System.Drawing.Point(72, 69);
            this.cbRight.Name = "cbRight";
            this.cbRight.Size = new System.Drawing.Size(121, 21);
            this.cbRight.TabIndex = 4;
            this.cbRight.Text = "Right";
            this.cbRight.SelectedIndexChanged += new System.EventHandler(this.cbRight_SelectedIndexChanged);
            // 
            // bOK
            // 
            this.bOK.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.bOK.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.bOK.Location = new System.Drawing.Point(82, 164);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(68, 23);
            this.bOK.TabIndex = 5;
            this.bOK.Text = "Save";
            this.bOK.UseVisualStyleBackColor = false;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // lGameLevel
            // 
            this.lGameLevel.AutoSize = true;
            this.lGameLevel.Font = new System.Drawing.Font("Showcard Gothic", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lGameLevel.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lGameLevel.Location = new System.Drawing.Point(51, 102);
            this.lGameLevel.Name = "lGameLevel";
            this.lGameLevel.Size = new System.Drawing.Size(138, 28);
            this.lGameLevel.TabIndex = 1;
            this.lGameLevel.Text = "Game Level";
            // 
            // lEasy
            // 
            this.lEasy.AutoSize = true;
            this.lEasy.Font = new System.Drawing.Font("Showcard Gothic", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lEasy.ForeColor = System.Drawing.Color.AliceBlue;
            this.lEasy.Location = new System.Drawing.Point(3, 135);
            this.lEasy.Name = "lEasy";
            this.lEasy.Size = new System.Drawing.Size(49, 20);
            this.lEasy.TabIndex = 1;
            this.lEasy.Text = "Easy";
            this.lEasy.Click += new System.EventHandler(this.lEasy_Click);
            // 
            // lMedium
            // 
            this.lMedium.AutoSize = true;
            this.lMedium.Font = new System.Drawing.Font("Showcard Gothic", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lMedium.ForeColor = System.Drawing.Color.AliceBlue;
            this.lMedium.Location = new System.Drawing.Point(75, 135);
            this.lMedium.Name = "lMedium";
            this.lMedium.Size = new System.Drawing.Size(77, 20);
            this.lMedium.TabIndex = 1;
            this.lMedium.Text = "Medium";
            this.lMedium.Click += new System.EventHandler(this.lMedium_Click);
            // 
            // lHard
            // 
            this.lHard.AutoSize = true;
            this.lHard.Font = new System.Drawing.Font("Showcard Gothic", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lHard.ForeColor = System.Drawing.Color.AliceBlue;
            this.lHard.Location = new System.Drawing.Point(165, 135);
            this.lHard.Name = "lHard";
            this.lHard.Size = new System.Drawing.Size(54, 20);
            this.lHard.TabIndex = 1;
            this.lHard.Text = "Hard";
            this.lHard.Click += new System.EventHandler(this.lHard_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(231, 199);
            this.ControlBox = false;
            this.Controls.Add(this.lHard);
            this.Controls.Add(this.lMedium);
            this.Controls.Add(this.lEasy);
            this.Controls.Add(this.lGameLevel);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.cbRight);
            this.Controls.Add(this.cbLeft);
            this.Controls.Add(this.lRight);
            this.Controls.Add(this.lLeft);
            this.Controls.Add(this.lKeys);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.Text = "Settings";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lKeys;
        private System.Windows.Forms.Label lLeft;
        private System.Windows.Forms.Label lRight;
        private System.Windows.Forms.ComboBox cbLeft;
        private System.Windows.Forms.ComboBox cbRight;
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.Label lGameLevel;
        private System.Windows.Forms.Label lEasy;
        private System.Windows.Forms.Label lMedium;
        private System.Windows.Forms.Label lHard;
    }
}