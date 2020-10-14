﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_checkers
{
    public class Game
    {
        public enum eGameResult
        {
            Draw,
            Player1Win,
            Player2Win,
        }

        public EventHandler NewGameStarted;
        public EventHandler BoardUpdated;
        public EventHandler GameEnded;

        private Board m_GameBoard;
        private Player m_CurrentPlayer = new Player();
        private Player m_NextPlayer = new Player();
        private List<Move> m_PlayerRegularMoves = new List<Move>();
        private List<Move> m_PlayerSkipMoves = new List<Move>();
        private Checker m_LastGameCheckerPlaced;
        private bool m_NeedToSkip = false;

        public Player CurrentPlayer
        {
            get { return m_CurrentPlayer;}
        }

        public Player NextPlayer
        {
            get { return m_NextPlayer;}
        }

        public Board Board
        {
            get { return m_GameBoard;}
        }

        public Checker LastGameCheckerPlaced
        {
            get { return m_LastGameCheckerPlaced;}
        }

        public bool IsEndGame()
        {
            bool result = false;

            if (m_CurrentPlayer.PlayerCheckers.Count == 0)
            {
                result = true;
            }
            else
            {
                createMovesForCurrentPlayer();
                result = m_CurrentPlayer.IfNoLegalMoveLeft(m_PlayerRegularMoves, m_PlayerSkipMoves);
            }

            return result;
        }

        private void updatePlayersDetails(string i_Player1Name, string i_Player2Name, bool i_IsPc)
        {
            int player2Type = i_IsPc == true ? (int)Player.ePLayerType.Computer : (int)Player.ePLayerType.Human;

            m_CurrentPlayer.Name = i_Player1Name;
            m_NextPlayer.Name = i_Player2Name;
            m_CurrentPlayer.InitializePlayer((int) Player.ePLayerType.Human, (int) Player.ePlayerNumber.PlayerOne,
                m_GameBoard);
            m_NextPlayer.InitializePlayer(player2Type, (int) Player.ePlayerNumber.PlayerTwo, m_GameBoard);
        }

        private void updateBoardSize(int i_BoardSize)
        {
            m_GameBoard = new Board(i_BoardSize, i_BoardSize);
        }

        public bool ImplementMove(Move i_InputMove, out string o_Errormessage)
        {
            BoardChangedEventArgs bu;
            GameEndedEventArgs gameEndedAgs;
            Move pcMove = new Move();
            bool result = false, isSkipMove = false;
            string endGameMessage;

            createMovesForCurrentPlayer();
            isSkipMove = m_PlayerSkipMoves.Count != 0;

            if (isLegalMove(ref i_InputMove, out o_Errormessage))
            {
                m_LastGameCheckerPlaced = i_InputMove.ApplyMove(m_GameBoard, m_CurrentPlayer.PlayerCheckers, m_NextPlayer.PlayerCheckers);

                m_PlayerSkipMoves.Clear();
                m_LastGameCheckerPlaced.AddSkipMoves(m_GameBoard, m_PlayerSkipMoves);

                if (isSkipMoveLeft() && isSkipMove)
                {
                    m_NeedToSkip = true;
                }
                else
                {
                    ChangeTurn();
                    m_NeedToSkip = false;
                }

                bu = new BoardChangedEventArgs(i_InputMove);
                OnBoardUpdated(bu);
                result = true;
            }

            if (IsEndGame())
            {
                EndGame(out endGameMessage);
                gameEndedAgs = new GameEndedEventArgs(endGameMessage);
                OnGameEnded(gameEndedAgs);
            }
            else if (CurrentPlayer.IsPc())
            {
                createMovesForCurrentPlayer();
                pcMove = CurrentPlayer.GetPCMove(m_PlayerRegularMoves, m_PlayerSkipMoves);
                ImplementMove(pcMove, out o_Errormessage);
            }

            return result;
        }

        private void createMovesForCurrentPlayer()
        {
            m_PlayerRegularMoves = m_CurrentPlayer.CreateRegularArray(m_GameBoard);
            if (!m_NeedToSkip)
            {
                m_PlayerSkipMoves = m_CurrentPlayer.CreateSkipsArray(m_GameBoard);
            }
        }

        private bool isSkipMoveLeft()
        {
            return m_PlayerSkipMoves.Count != 0;
        }

        private void ChangeTurn()
        {
            SwapTurns(ref m_CurrentPlayer, ref m_NextPlayer);
        }

        private bool isLegalMove(ref Move i_InputMove, out string o_Errormessage)
        {
            bool result = false;

            o_Errormessage = string.Empty;
            
            if (m_PlayerSkipMoves.Count != 0)
            {
                if (isMoveInList(ref i_InputMove, m_PlayerSkipMoves))
                {
                    result = true;
                }
                else
                {
                    o_Errormessage = "You must make a eat move";
                }
            }
            else
            {
                if (isMoveInList(ref i_InputMove, m_PlayerRegularMoves))
                {
                    result = true;
                }
                else
                {
                    o_Errormessage = "Invalid move, Please try again";
                }
            }

            return result;
        }

        public void InitializeStartingDetails(int i_BoardSize, string i_Player1Name, string i_Player2Name, bool i_IsPc)
        {
            updateBoardSize(i_BoardSize);
            updatePlayersDetails(i_Player1Name, i_Player2Name, i_IsPc);
            OnNewGameStarted();
        }

        public void OnNewGameStarted()
        {
            EventArgs e = new EventArgs();

            if (NewGameStarted != null)
            {
                NewGameStarted(this, e);
            }
        }
        public eGameResult EndGame(out string o_EndGamemessage)
        {
            StringBuilder endGameMessage = new StringBuilder();

            List<Move> nextPlayerSkipMoves, nextPlayerRegularMoves;
            int winnerScore = 0;
            eGameResult gameResult;

            nextPlayerSkipMoves = m_NextPlayer.CreateSkipsArray(m_GameBoard);
            nextPlayerRegularMoves = m_NextPlayer.CreateRegularArray(m_GameBoard);

            if (m_NextPlayer.IfNoLegalMoveLeft(nextPlayerRegularMoves, nextPlayerSkipMoves))
            {
                gameResult = eGameResult.Draw;
                endGameMessage.Append("Tie!");
            }
            else
            {
                gameResult = m_CurrentPlayer.CheckerShape == (char)Checker.eSigns.PlayerO ? eGameResult.Player1Win : eGameResult.Player2Win;
                winnerScore = m_NextPlayer.CalculateWinnerScore(m_CurrentPlayer);
                endGameMessage.Append(string.Format("{0} Won!", m_NextPlayer.Name));
            }

            endGameMessage.Append(string.Format("{0}Do you want to play another Round?", Environment.NewLine));
            o_EndGamemessage = endGameMessage.ToString();
            m_NextPlayer.Score += winnerScore;

            return gameResult;
        }

        public void ResetGame()
        {
            m_GameBoard.Clear();
            if (m_CurrentPlayer.CheckerShape == (char)Checker.eSigns.PlayerX)
            {
                ChangeTurn();
            }

            m_CurrentPlayer.ResetCheckers(m_GameBoard);
            m_NextPlayer.ResetCheckers(m_GameBoard);
        }

        public void OnBoardUpdated(BoardChangedEventArgs bu)
        {
            if (BoardUpdated != null)
            {
                BoardUpdated(this, bu);
            }
        }

        public void OnGameEnded(GameEndedEventArgs ge)
        {
            if (GameEnded != null)
            {
                GameEnded(this, ge);
            }
        }
        private bool isMoveInList(ref Move io_inputMove, List<Move> io_List)
        {
            bool retVal = false;

            foreach (Move currentMove in io_List)
            {
                if (currentMove.Equals(io_inputMove))
                {
                    retVal = true;
                    io_inputMove = currentMove;
                    break;
                }
            }

            return retVal;
        }

        public void SwapTurns(ref Player io_PlayerOne, ref Player io_PlayerTwo)
        {
            Player temp;

            temp = io_PlayerOne;
            io_PlayerOne = io_PlayerTwo;
            io_PlayerTwo = temp;
        }

    }
}