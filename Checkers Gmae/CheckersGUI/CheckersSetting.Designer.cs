namespace CheckersGUI
{
    public partial class CheckersSetting
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
            this.DoneButton = new System.Windows.Forms.Button();
            this.BoardSizeradioButton6x6 = new System.Windows.Forms.RadioButton();
            this.BoardSizeradioButton8x8 = new System.Windows.Forms.RadioButton();
            this.BoardSizeradioButton10x10 = new System.Windows.Forms.RadioButton();
            this.BoardSizeLabel = new System.Windows.Forms.Label();
            this.textBoxPlayer1Name = new System.Windows.Forms.TextBox();
            this.textBoxPlayer2Name = new System.Windows.Forms.TextBox();
            this.PlayersTitleLabel = new System.Windows.Forms.Label();
            this.Player1Label = new System.Windows.Forms.Label();
            this.Player2CheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // DoneButton
            // 
            this.DoneButton.ForeColor = System.Drawing.Color.DarkBlue;
            this.DoneButton.Location = new System.Drawing.Point(268, 224);
            this.DoneButton.Name = "DoneButton";
            this.DoneButton.Size = new System.Drawing.Size(75, 23);
            this.DoneButton.TabIndex = 0;
            this.DoneButton.Text = "Done";
            this.DoneButton.UseVisualStyleBackColor = true;
            this.DoneButton.Click += new System.EventHandler(this.buttonDone_Click);
            // 
            // BoardSizeradioButton6x6
            // 
            this.BoardSizeradioButton6x6.AutoSize = true;
            this.BoardSizeradioButton6x6.Checked = true;
            this.BoardSizeradioButton6x6.ForeColor = System.Drawing.Color.DarkBlue;
            this.BoardSizeradioButton6x6.Location = new System.Drawing.Point(33, 54);
            this.BoardSizeradioButton6x6.Name = "BoardSizeradioButton6x6";
            this.BoardSizeradioButton6x6.Size = new System.Drawing.Size(48, 17);
            this.BoardSizeradioButton6x6.TabIndex = 1;
            this.BoardSizeradioButton6x6.TabStop = true;
            this.BoardSizeradioButton6x6.Text = "6 x 6";
            this.BoardSizeradioButton6x6.UseVisualStyleBackColor = true;
            this.BoardSizeradioButton6x6.CheckedChanged += new System.EventHandler(this.radioButtonSmallBoardSize_CheckedChanged);
            // 
            // BoardSizeradioButton8x8
            // 
            this.BoardSizeradioButton8x8.AutoSize = true;
            this.BoardSizeradioButton8x8.ForeColor = System.Drawing.Color.DarkBlue;
            this.BoardSizeradioButton8x8.Location = new System.Drawing.Point(138, 54);
            this.BoardSizeradioButton8x8.Name = "BoardSizeradioButton8x8";
            this.BoardSizeradioButton8x8.Size = new System.Drawing.Size(48, 17);
            this.BoardSizeradioButton8x8.TabIndex = 1;
            this.BoardSizeradioButton8x8.Text = "8 x 8";
            this.BoardSizeradioButton8x8.UseVisualStyleBackColor = true;
            this.BoardSizeradioButton8x8.CheckedChanged += new System.EventHandler(this.radioButtonMediumBoardSize_CheckedChanged);
            // 
            // BoardSizeradioButton10x10
            // 
            this.BoardSizeradioButton10x10.AutoSize = true;
            this.BoardSizeradioButton10x10.ForeColor = System.Drawing.Color.DarkBlue;
            this.BoardSizeradioButton10x10.Location = new System.Drawing.Point(245, 54);
            this.BoardSizeradioButton10x10.Name = "BoardSizeradioButton10x10";
            this.BoardSizeradioButton10x10.Size = new System.Drawing.Size(60, 17);
            this.BoardSizeradioButton10x10.TabIndex = 1;
            this.BoardSizeradioButton10x10.Text = "10 x 10";
            this.BoardSizeradioButton10x10.UseVisualStyleBackColor = true;
            this.BoardSizeradioButton10x10.CheckedChanged += new System.EventHandler(this.radioButtonLargeBoardSize_CheckedChanged);
            // 
            // BoardSizeLabel
            // 
            this.BoardSizeLabel.AutoSize = true;
            this.BoardSizeLabel.ForeColor = System.Drawing.Color.DarkBlue;
            this.BoardSizeLabel.Location = new System.Drawing.Point(12, 23);
            this.BoardSizeLabel.Name = "BoardSizeLabel";
            this.BoardSizeLabel.Size = new System.Drawing.Size(61, 13);
            this.BoardSizeLabel.TabIndex = 2;
            this.BoardSizeLabel.Text = "Board Size:";
            // 
            // textBoxPlayer1Name
            // 
            this.textBoxPlayer1Name.ForeColor = System.Drawing.Color.DarkBlue;
            this.textBoxPlayer1Name.Location = new System.Drawing.Point(120, 125);
            this.textBoxPlayer1Name.Name = "textBoxPlayer1Name";
            this.textBoxPlayer1Name.Size = new System.Drawing.Size(154, 20);
            this.textBoxPlayer1Name.TabIndex = 3;
            // 
            // textBoxPlayer2Name
            // 
            this.textBoxPlayer2Name.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxPlayer2Name.Enabled = false;
            this.textBoxPlayer2Name.ForeColor = System.Drawing.Color.DarkBlue;
            this.textBoxPlayer2Name.Location = new System.Drawing.Point(120, 166);
            this.textBoxPlayer2Name.Name = "textBoxPlayer2Name";
            this.textBoxPlayer2Name.Size = new System.Drawing.Size(154, 20);
            this.textBoxPlayer2Name.TabIndex = 3;
            this.textBoxPlayer2Name.Text = "[Computer]";
            // 
            // PlayersTitleLabel
            // 
            this.PlayersTitleLabel.AutoSize = true;
            this.PlayersTitleLabel.ForeColor = System.Drawing.Color.DarkBlue;
            this.PlayersTitleLabel.Location = new System.Drawing.Point(12, 93);
            this.PlayersTitleLabel.Name = "PlayersTitleLabel";
            this.PlayersTitleLabel.Size = new System.Drawing.Size(44, 13);
            this.PlayersTitleLabel.TabIndex = 2;
            this.PlayersTitleLabel.Text = "Players:";
            // 
            // Player1Label
            // 
            this.Player1Label.AutoSize = true;
            this.Player1Label.ForeColor = System.Drawing.Color.DarkBlue;
            this.Player1Label.Location = new System.Drawing.Point(30, 132);
            this.Player1Label.Name = "Player1Label";
            this.Player1Label.Size = new System.Drawing.Size(48, 13);
            this.Player1Label.TabIndex = 4;
            this.Player1Label.Text = "Player 1:";
            // 
            // Player2CheckBox
            // 
            this.Player2CheckBox.AutoSize = true;
            this.Player2CheckBox.ForeColor = System.Drawing.Color.DarkBlue;
            this.Player2CheckBox.Location = new System.Drawing.Point(33, 166);
            this.Player2CheckBox.Name = "Player2CheckBox";
            this.Player2CheckBox.Size = new System.Drawing.Size(67, 17);
            this.Player2CheckBox.TabIndex = 5;
            this.Player2CheckBox.Text = "Player 2:";
            this.Player2CheckBox.UseVisualStyleBackColor = true;
            this.Player2CheckBox.CheckedChanged += new System.EventHandler(this.Player2CheckBox_CheckedChanged);
            // 
            // CheckersSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 263);
            this.Controls.Add(this.Player2CheckBox);
            this.Controls.Add(this.Player1Label);
            this.Controls.Add(this.textBoxPlayer2Name);
            this.Controls.Add(this.textBoxPlayer1Name);
            this.Controls.Add(this.PlayersTitleLabel);
            this.Controls.Add(this.BoardSizeLabel);
            this.Controls.Add(this.BoardSizeradioButton10x10);
            this.Controls.Add(this.BoardSizeradioButton8x8);
            this.Controls.Add(this.BoardSizeradioButton6x6);
            this.Controls.Add(this.DoneButton);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CheckersSetting";
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Checkers Game Settings";
            this.Load += new System.EventHandler(this.FormGameSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button DoneButton;
        private System.Windows.Forms.RadioButton BoardSizeradioButton6x6;
        private System.Windows.Forms.RadioButton BoardSizeradioButton8x8;
        private System.Windows.Forms.RadioButton BoardSizeradioButton10x10;
        private System.Windows.Forms.Label BoardSizeLabel;
        private System.Windows.Forms.TextBox textBoxPlayer1Name;
        private System.Windows.Forms.TextBox textBoxPlayer2Name;
        private System.Windows.Forms.Label PlayersTitleLabel;
        private System.Windows.Forms.Label Player1Label;
        private System.Windows.Forms.CheckBox Player2CheckBox;
    }
}