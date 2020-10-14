using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CheckersGUI
{
    public partial class CheckersBoard : Form
    {
        public enum EPlayersTurn
        {
            One = 1,
            Two,
        }

        private const int k_PictureBoxWidth = 60;
        private const int k_PictureBoxHeight = 60;
        private const int k_StartingPictureBoxX = 10;
        private const int k_StartingPictureBoxY = 40;
        private const int k_WidthExtension = 30;
        private const int k_HeightExtension = 80;

        public event EventHandler GameDetailsFilled;

        public event EventHandler MoveEntered;

        public event EventHandler MessageBoxAnswered;

        private readonly CheckersSetting r_FormGameSettings = new CheckersSetting();
        private PictureBoxGameChecker[,] pictureBoxMatrix;
        private PictureBoxGameChecker pictureBoxPressed;
        private Label labelPlayer1 = new Label();
        private Label labelPlayer2 = new Label();
        private bool anotherPictureBoxPressed = false;

        public CheckersBoard()
        {
            InitializeComponent();
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            PictureBoxGameChecker currentGameCheckerPressed = sender as PictureBoxGameChecker;
            MoveEnteredEventArgs moveEnteredEventArgs;
            if (currentGameCheckerPressed.IsEnabled)
            {
                if (anotherPictureBoxPressed)
                {
                    if (currentGameCheckerPressed.Image == null)
                    {
                        moveEnteredEventArgs = createMoveEnterdEventArgs(pictureBoxPressed.PlaceOnBoard, currentGameCheckerPressed.PlaceOnBoard);
                        OnMoveEntered(moveEnteredEventArgs);
                        anotherPictureBoxPressed = false;
                        pictureBoxPressed.BorderStyle = BorderStyle.FixedSingle;
                        pictureBoxPressed.IsEnabled = true;
                        pictureBoxPressed.BackgroundImage = global::CheckersGUI.Properties.Resources.EnabledBackground;
                    }
                }
                else
                {
                    if (currentGameCheckerPressed.Image != null)
                    {
                        pictureBoxPressed = currentGameCheckerPressed;
                        currentGameCheckerPressed.BorderStyle = BorderStyle.Fixed3D;
                        currentGameCheckerPressed.IsEnabled = false;
                        anotherPictureBoxPressed = true;
                        pictureBoxPressed.BackgroundImage = global::CheckersGUI.Properties.Resources.GameCheckerPressed;
                    }
                }
            }
            else
            {
                anotherPictureBoxPressed = false;
                currentGameCheckerPressed.BorderStyle = BorderStyle.FixedSingle;
                currentGameCheckerPressed.IsEnabled = true;
                pictureBoxPressed.BackgroundImage = global::CheckersGUI.Properties.Resources.EnabledBackground;
            }
        }

        public void ErrorMessageBox(string i_ErrorMessage)
        {
            MessageBox.Show(i_ErrorMessage);
        }

        public void ResetGameBoard()
        {
            foreach (PictureBoxGameChecker currentGameChecker in pictureBoxMatrix)
            {
                currentGameChecker.BackColor = Color.Black;
                currentGameChecker.BackgroundImage = global::CheckersGUI.Properties.Resources.DisabledBackground;
                currentGameChecker.Image = null;
            }
        }

        private MoveEnteredEventArgs createMoveEnterdEventArgs(Point i_From, Point i_To)
        {
            MoveEnteredEventArgs result = new MoveEnteredEventArgs(i_From, i_To);

            return result;
        }

        public void CheckersSetting_FormClosed(object sender, FormClosedEventArgs e)
        {
            CheckPlayersName();

            DetailsReceivedEventArgs detailsReceivedEvent = new DetailsReceivedEventArgs(
            r_FormGameSettings.Player1TextName,
            r_FormGameSettings.Player2TextName,
            r_FormGameSettings.CheckersBoardSize,
            r_FormGameSettings.CheckIfPlayer2IsComputer);
            pictureBoxMatrix = new PictureBoxGameChecker[r_FormGameSettings.CheckersBoardSize, r_FormGameSettings.CheckersBoardSize];
            setFormBoarders();
            createPictureBoxMatrix();
            setPlayersLabels();
            OnGameDetailsFiled(detailsReceivedEvent);
        }

        private void CheckPlayersName()
        {
            if (r_FormGameSettings.CheckIfPlayer2IsComputer)
            {
                r_FormGameSettings.Player2TextName = "Computer";
            }
            if (r_FormGameSettings.Player1TextName=="")

            {
                r_FormGameSettings.Player1TextName = "Player 1";
            }

            if (r_FormGameSettings.Player2TextName == "")
            {
                r_FormGameSettings.Player2TextName = "Player 2";
            }
        }

        public void MarkCurrentPlayerLabel(string i_CurrentPlayerName)
        {
            if(i_CurrentPlayerName == r_FormGameSettings.Player1TextName)
            {
                labelPlayer1.ForeColor = Color.MediumBlue;
                labelPlayer2.ForeColor = Color.Black;
            }
            else
            {
                labelPlayer2.ForeColor = Color.Red;
                labelPlayer1.ForeColor = Color.Black;
            }
        }

        private void setPlayersLabels()
        {
            int playerStartingScore = 0;

            setPlayersLabelLocationAndFont();
            UpdateGameScore(playerStartingScore, playerStartingScore);
            Controls.Add(labelPlayer1);
            Controls.Add(labelPlayer2);
        }


        private void setPlayersLabelLocationAndFont()
        {

            Point labelPlayer1Position = new Point(r_FormGameSettings.CheckersBoardSize*10, 5);
            Point labelPlayer2Position = new Point(r_FormGameSettings.CheckersBoardSize*40, 5);

            labelPlayer1.Location = labelPlayer1Position;
            labelPlayer1.AutoSize = true;

            labelPlayer2.Location = labelPlayer2Position;
            labelPlayer2.AutoSize = true;

            labelPlayer1.Font = new Font("Arial", 10, FontStyle.Bold, GraphicsUnit.Point);
            labelPlayer2.Font = new Font("Arial", 10, FontStyle.Bold, GraphicsUnit.Point);
        }

        private void updateLabelsText(string i_PlayerName, EPlayersTurn i_PlayerNumber, int i_PlayerScore)
        {
            if (i_PlayerNumber == EPlayersTurn.One)
            {
                labelPlayer1.Text = string.Format("{0}: {1}", i_PlayerName, i_PlayerScore);
            }
            else
            {
                labelPlayer2.Text = string.Format("{0}: {1}", i_PlayerName, i_PlayerScore);
            }
        }

        public void UpdateGameScore(int i_Player1Score, int i_Player2Score)
        {
            labelPlayer1.Text = string.Format("{0}: {1}", r_FormGameSettings.Player1TextName, i_Player1Score);
            labelPlayer2.Text = string.Format("{0}: {1}", r_FormGameSettings.Player2TextName, i_Player2Score);
        }

        private void setFormBoarders()
        {
            this.Size = new Size((r_FormGameSettings.CheckersBoardSize * k_PictureBoxWidth) + k_WidthExtension, (r_FormGameSettings.CheckersBoardSize * k_PictureBoxHeight) + k_HeightExtension);
        }

        private void createPictureBoxMatrix()
        {
            bool newLine = false;
            bool isFirstPictureBox = true;
            PictureBox lastPictureBoxInMatrix = new PictureBox();

            for (int i = 0; i < r_FormGameSettings.CheckersBoardSize; i++)
            {
                for (int j = 0; j < r_FormGameSettings.CheckersBoardSize; j++)
                {
                    PictureBoxGameChecker currentPictureBox = new PictureBoxGameChecker(i, j);
                    setPictureBoxLocation(currentPictureBox, newLine, isFirstPictureBox, lastPictureBoxInMatrix);
                    intializePictureBox(currentPictureBox);
                    pictureBoxMatrix[i, j] = currentPictureBox;
                    this.Controls.Add(currentPictureBox);
                    newLine = false;
                    isFirstPictureBox = false;
                    lastPictureBoxInMatrix = currentPictureBox;
                }

                newLine = true;
            }
        }

        private void intializePictureBox(PictureBoxGameChecker i_CurrentPictureBox)
        {
            setPictureBoxFigure(i_CurrentPictureBox);
            i_CurrentPictureBox.Enabled = false;
            i_CurrentPictureBox.Click += pictureBox_Click;
        }

        private void setPictureBoxFigure(PictureBoxGameChecker i_CurrentPictureBox)
        {
            i_CurrentPictureBox.Height = k_PictureBoxHeight;
            i_CurrentPictureBox.Width = k_PictureBoxWidth;
        }

        private void setPictureBoxLocation(
            PictureBoxGameChecker i_CurrentPictureBox,
            bool i_NewLine,
            bool i_IsFirstPictureBox,
            PictureBox i_LastPictureBoxInMatrix)
        {
            Point newLocation;
            int   pictureBoxMatrixMaxLine = r_FormGameSettings.CheckersBoardSize - 1;
            int   pictureBoxMatrixMaxCol = r_FormGameSettings.CheckersBoardSize - 1;

            if (i_IsFirstPictureBox)
            {
                newLocation = new Point(k_StartingPictureBoxX, k_StartingPictureBoxY);
            }
            else
            {
                newLocation = i_LastPictureBoxInMatrix.Location;
                if (!i_NewLine)
                {
                    newLocation.Offset(i_LastPictureBoxInMatrix.Width, 0);
                }
                else
                {
                    newLocation.X = k_StartingPictureBoxX;
                    newLocation.Offset(0, i_LastPictureBoxInMatrix.Height);
                }
            }

            i_CurrentPictureBox.Location = newLocation;         
        }

        protected virtual void OnGameDetailsFiled(DetailsReceivedEventArgs df)
        {
            if (GameDetailsFilled != null)
            {
                GameDetailsFilled(this, df);
            }
        }

        protected virtual void OnMoveEntered(MoveEnteredEventArgs me)
        {
            if (MoveEntered != null)
            {
                MoveEntered(this, me);
            }
        }

        public void AddGameCheckersToGameBoard(List<Point> i_PointsToAdd, Image i_GameCheckerImage)
        {
            foreach (Point currentPoint in i_PointsToAdd)
            {
                setPictureBoxImage(currentPoint, i_GameCheckerImage);
            }

            EnableGamePictureBoxes(i_PointsToAdd);
        }

        private void setPictureBoxImage(Point i_CurrentPoint, Image i_GameCheckerImage)
        {
            pictureBoxMatrix[i_CurrentPoint.X, i_CurrentPoint.Y].Image = i_GameCheckerImage;
            pictureBoxMatrix[i_CurrentPoint.X, i_CurrentPoint.Y].SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
        }

        public void EraseGameCheckersFromGameBoard(List<Point> i_PointsToErase)
        {
            foreach (Point currentPoint in i_PointsToErase)
            {
                pictureBoxMatrix[currentPoint.X, currentPoint.Y].Image = null;
                pictureBoxMatrix[currentPoint.X, currentPoint.Y].BorderStyle = BorderStyle.FixedSingle;
            }
        }
        
        public void EnableGamePictureBoxes(List<Point> i_GamePictureBoxesToEnable)
        {
            foreach (Point currentPoint in i_GamePictureBoxesToEnable)
            {                
                pictureBoxMatrix[currentPoint.X, currentPoint.Y].Enabled = true;
                pictureBoxMatrix[currentPoint.X, currentPoint.Y].BackgroundImage = global::CheckersGUI.Properties.Resources.EnabledBackground;
                pictureBoxMatrix[currentPoint.X, currentPoint.Y].IsEnabled = true;
            }
        }

        public void DisableGamePictureBoxes(List<Point> i_GamePictureBoxesToDisable)
        {
            foreach (Point currentPoint in i_GamePictureBoxesToDisable)
            {
                pictureBoxMatrix[currentPoint.X, currentPoint.Y].Enabled = false;
                pictureBoxMatrix[currentPoint.X, currentPoint.Y].IsEnabled = false;
            }
        }

        public void CreateYesNoMessageBox(string i_MessageBoxString, string i_Caption)
        {
            DialogResult dialogResult = MessageBox.Show(i_MessageBoxString, i_Caption, MessageBoxButtons.YesNo);
            MessageBoxResponseEventArgs mba;
            bool isMessageBoxAnswerIsYes = false;

            if (dialogResult == DialogResult.Yes)
            {
                isMessageBoxAnswerIsYes = true;
            }
            else if (dialogResult == DialogResult.No)
            {
                isMessageBoxAnswerIsYes = false;
            }

            mba = new MessageBoxResponseEventArgs(isMessageBoxAnswerIsYes);
            OnMessageBoxAnswered(mba);
        }

        private void OnMessageBoxAnswered(MessageBoxResponseEventArgs mba)
        {
            if (MessageBoxAnswered != null)
            {
                MessageBoxAnswered(this, mba);
            }
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // CheckersBoard
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "CheckersBoard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chekers";
            this.Load += new System.EventHandler(this.CheckersBoard_Load);
            this.ResumeLayout(false);

        }

        private void CheckersBoard_Load(object sender, EventArgs e)
        {
            r_FormGameSettings.FormClosed += CheckersSetting_FormClosed;
            r_FormGameSettings.ShowDialog();
        }

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

        #endregion

    }
}
