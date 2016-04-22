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
            this.SuspendLayout();
            // 
            // lKeys
            // 
            this.lKeys.AutoSize = true;
            this.lKeys.BackColor = System.Drawing.Color.Transparent;
            this.lKeys.Font = new System.Drawing.Font("Showcard Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lKeys.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lKeys.Location = new System.Drawing.Point(96, 18);
            this.lKeys.Name = "lKeys";
            this.lKeys.Size = new System.Drawing.Size(73, 30);
            this.lKeys.TabIndex = 0;
            this.lKeys.Text = "Keys";
            // 
            // lLeft
            // 
            this.lLeft.AutoSize = true;
            this.lLeft.Font = new System.Drawing.Font("Showcard Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lLeft.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lLeft.Location = new System.Drawing.Point(17, 65);
            this.lLeft.Name = "lLeft";
            this.lLeft.Size = new System.Drawing.Size(59, 27);
            this.lLeft.TabIndex = 1;
            this.lLeft.Text = "Left";
            // 
            // lright
            // 
            this.lRight.AutoSize = true;
            this.lRight.Font = new System.Drawing.Font("Showcard Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lRight.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lRight.Location = new System.Drawing.Point(12, 114);
            this.lRight.Name = "lright";
            this.lRight.Size = new System.Drawing.Size(76, 27);
            this.lRight.TabIndex = 2;
            this.lRight.Text = "Right";
            // 
            // cbLeft
            // 
            this.cbLeft.FormattingEnabled = true;
            this.cbLeft.Items.AddRange(new object[] {
            "Left",
            "L"});
            this.cbLeft.Location = new System.Drawing.Point(116, 71);
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
            "R"});
            this.cbRight.Location = new System.Drawing.Point(116, 120);
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
            this.bOK.Location = new System.Drawing.Point(101, 178);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(75, 23);
            this.bOK.TabIndex = 5;
            this.bOK.Text = "Save";
            this.bOK.UseVisualStyleBackColor = false;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(284, 222);
            this.ControlBox = false;
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
            this.ResumeLayout(false);
            this.PerformLayout();
            this.TopMost = true;

        }

        #endregion

        private System.Windows.Forms.Label lKeys;
        private System.Windows.Forms.Label lLeft;
        private System.Windows.Forms.Label lRight;
        private System.Windows.Forms.ComboBox cbLeft;
        private System.Windows.Forms.ComboBox cbRight;
        private System.Windows.Forms.Button bOK;
    }
}