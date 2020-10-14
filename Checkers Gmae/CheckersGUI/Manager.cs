using System;
using System.Collections.Generic;
using System.Drawing;
using Ex02_checkers;

namespace CheckersGUI
{
    public class Manager
    {

        private readonly CheckersBoard r_FormCheckersGame = new CheckersBoard();
        private readonly Game r_CheckersGame = new Game();

        public void Run()
        {
            subscribeToLogicEvents();
            subscribeToFormEvents();
            r_FormCheckersGame.ShowDialog();
        }

        private void subscribeToLogicEvents()
        {
            r_CheckersGame.NewGameStarted += Checker_NewGameStarted;
            r_CheckersGame.BoardUpdated += OnBoardUpdated;
            r_CheckersGame.GameEnded += OnGameEnded;
        }

        private void subscribeToFormEvents()
        {
            r_FormCheckersGame.GameDetailsFilled += FormGame_GameDetailsFilled;
            r_FormCheckersGame.MoveEntered += m_FormGame_MoveEntered;
            r_FormCheckersGame.MessageBoxAnswered += m_FormGame_MessageBoxAnswered;
        }

        private void OnGameEnded(object sender, EventArgs e)
        {
            GameEndedEventArgs gameEndedEventArgs = e as GameEndedEventArgs;

            r_FormCheckersGame.CreateYesNoMessageBox(gameEndedEventArgs.Message, "Checkers");
        }

        private void m_FormGame_MessageBoxAnswered(object sender, EventArgs e)
        {
            MessageBoxResponseEventArgs messageBoxResponse = e as MessageBoxResponseEventArgs;

            if (messageBoxResponse.IsAnsweredYes)
            {
                Checker_NewGameStarted(sender, e);
            }
            else
            {
                r_FormCheckersGame.Close();
            }
        }

        private void m_FormGame_MoveEntered(object sender, EventArgs e)
        {
            MoveEnteredEventArgs moveEnteredEventArgs = e as MoveEnteredEventArgs;
            Move                newMove = new Move(moveEnteredEventArgs.From, moveEnteredEventArgs.To);
            string              errorMessage;
            bool                isLegalMove;

            isLegalMove = r_CheckersGame.ImplementMove(newMove, out errorMessage);
            if (!isLegalMove)
            {
                r_FormCheckersGame.ErrorMessageBox(errorMessage);
            }
        }

        private void OnBoardUpdated(object sender, EventArgs e)
        {
            BoardChangedEventArgs boardChangedEventArgs = e as BoardChangedEventArgs;
            List<Point>           pointsToAddOnBoard = new List<Point>();
            List<Point>           pointsToEraseOnBoard = new List<Point>();
            List<Point>           pointsToEnableOnBoard;
            List<Point>           pointsToDisableOnBoard;
            Image                 playerCheckerImage;

            pointsToAddOnBoard.Add(boardChangedEventArgs.LastMove.To);
            pointsToEraseOnBoard.Add(boardChangedEventArgs.LastMove.From);
            pointsToEnableOnBoard = createPointsListFromPlayerGameCheckers(r_CheckersGame.CurrentPlayer.PlayerCheckers);
            pointsToDisableOnBoard = createPointsListFromPlayerGameCheckers(r_CheckersGame.NextPlayer.PlayerCheckers);
            if (boardChangedEventArgs.LastMove.IsSkipMove)
            {
                pointsToEraseOnBoard.Add(boardChangedEventArgs.LastMove.Eaten);
                pointsToEnableOnBoard.Add(boardChangedEventArgs.LastMove.Eaten);
            }

            getImagePlayerChecker(out playerCheckerImage);
            markCurrentPlayer();
            r_FormCheckersGame.AddGameCheckersToGameBoard(pointsToAddOnBoard, playerCheckerImage);
            r_FormCheckersGame.EraseGameCheckersFromGameBoard(pointsToEraseOnBoard);
            r_FormCheckersGame.EnableGamePictureBoxes(pointsToEnableOnBoard);
            r_FormCheckersGame.DisableGamePictureBoxes(pointsToDisableOnBoard);
        }

        private void markCurrentPlayer()
        {
            r_FormCheckersGame.MarkCurrentPlayerLabel(r_CheckersGame.CurrentPlayer.Name);
        }

        private void getImagePlayerChecker(out Image o_ImagePlayerGameChecker)
        {
            char playerCheckerSign = r_CheckersGame.LastGameCheckerPlaced.Sign;

            if (playerCheckerSign == (char)Checker.eSigns.PlayerO)
            {
                o_ImagePlayerGameChecker = global::CheckersGUI.Properties.Resources.Player1BlueSoldier;
            }
            else if (playerCheckerSign == (char)Checker.eSigns.PlayerOKing)
            {
                o_ImagePlayerGameChecker = global::CheckersGUI.Properties.Resources.Player1BlueKing;
            }
            else if (playerCheckerSign == (char)Checker.eSigns.PlayerX)
            {
                o_ImagePlayerGameChecker = global::CheckersGUI.Properties.Resources.Player2RedSoldier;
            }
            else
            {
                o_ImagePlayerGameChecker = global::CheckersGUI.Properties.Resources.Player2RedKing;
            }
        }

        private List<Point> createPointsListFromPlayerGameCheckers(List<Checker> i_PlayerCheckers)
        {
            List<Point> result = new List<Point>();

            foreach (Checker current in i_PlayerCheckers)
            {
                result.Add(current.Location);
            }

            return result;
        }

        private void FormGame_GameDetailsFilled(object sender, EventArgs e)
        {
            DetailsReceivedEventArgs detailsReceivedEventArgs = e as DetailsReceivedEventArgs;

            r_CheckersGame.InitializeStartingDetails(detailsReceivedEventArgs.GameBoardSize, detailsReceivedEventArgs.PlayerOneName, detailsReceivedEventArgs.PlayerTwoName, detailsReceivedEventArgs.IsPlayer2Computer);
        }

        private void Checker_NewGameStarted(object sender, EventArgs e)
        {
            List<Point> player1GameCheckersPoints = new List<Point>();
            List<Point> player2GameCheckersPoints = new List<Point>();
            int         player1Score, player2Score;
            Image       player1GameCheckerImage = global::CheckersGUI.Properties.Resources.Player1BlueSoldier;
            Image       player2GameCheckerImage = global::CheckersGUI.Properties.Resources.Player2RedSoldier;

            r_FormCheckersGame.ResetGameBoard();
            r_CheckersGame.ResetGame();
            player1GameCheckersPoints = createPointsListFromPlayerGameCheckers(r_CheckersGame.CurrentPlayer.PlayerCheckers);
            player2GameCheckersPoints = createPointsListFromPlayerGameCheckers(r_CheckersGame.NextPlayer.PlayerCheckers);
            r_FormCheckersGame.AddGameCheckersToGameBoard(player1GameCheckersPoints, player1GameCheckerImage);
            r_FormCheckersGame.AddGameCheckersToGameBoard(player2GameCheckersPoints, player2GameCheckerImage);
            r_FormCheckersGame.DisableGamePictureBoxes(player2GameCheckersPoints);
            enabledBufferZone();
            player1Score = r_CheckersGame.CurrentPlayer.Score;
            player2Score = r_CheckersGame.NextPlayer.Score;
            r_FormCheckersGame.UpdateGameScore(player1Score, player2Score);
            markCurrentPlayer();
        }

        private void enabledBufferZone()
        {
            List<Point> enabledBufferZonePoints = new List<Point>();
            int startLoopIndex = (r_CheckersGame.Board.Height / 2) - 1;
            int endLoopIndex = (r_CheckersGame.Board.Height / 2) + 1;

            for (int i = startLoopIndex; i < endLoopIndex; i++)
            {
                for (int j = 0; j < r_CheckersGame.Board.Width; j++)
                {
                    if ((i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0))
                    {
                        enabledBufferZonePoints.Add(new Point(i, j));
                    }
                }
            }

            r_FormCheckersGame.EnableGamePictureBoxes(enabledBufferZonePoints);
        }
    }
}
