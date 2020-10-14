using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CheckersGUI
{
    public partial class CheckersSetting : Form
    {
        private static int m_BoardSize = 6;
        public CheckersSetting()
        {
            InitializeComponent();
        }

        public string Player1TextName
        {
            get {return textBoxPlayer1Name.Text;}
            set {textBoxPlayer1Name.Text = value;}
        }

        public string Player2TextName
        {
            get { return textBoxPlayer2Name.Text;}
            set{textBoxPlayer2Name.Text = value;}
        }

        public bool CheckIfPlayer2IsComputer
        {
            get {return !Player2CheckBox.Checked;}
        }

        public int CheckersBoardSize
        {
            get {return m_BoardSize;}
        }
        private void radioButtonSmallBoardSize_CheckedChanged(object sender, EventArgs e)
        {
            m_BoardSize = 6;
        }

        private void radioButtonMediumBoardSize_CheckedChanged(object sender, EventArgs e)
        {
            m_BoardSize = 8;
        }

        private void radioButtonLargeBoardSize_CheckedChanged(object sender, EventArgs e)
        {
            m_BoardSize = 10;
        }
        private void buttonDone_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Player2CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if(textBoxPlayer2Name.Enabled == true)
            {
                textBoxPlayer2Name.Enabled = false;
            }
            else
            {
                textBoxPlayer2Name.Enabled = true;
            }

            if (textBoxPlayer2Name.Enabled)
            {
                textBoxPlayer2Name.BackColor = Color.White;
                textBoxPlayer2Name.Text = "";
            }
            else
            {
                textBoxPlayer2Name.BackColor = System.Drawing.SystemColors.MenuBar;
                textBoxPlayer2Name.Text = "[Computer]";
            }
        }

        private void FormGameSettings_Load(object sender, EventArgs e)
        {

        }

    }
}
