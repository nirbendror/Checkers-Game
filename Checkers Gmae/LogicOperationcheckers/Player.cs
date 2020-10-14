using System;
using System.Collections.Generic;

namespace Ex02_checkers
{
    public class Player
    {
        private const int k_MaxUserNameSize = 20;

        public enum ePLayerType
        {
            Human,
            Computer
        }

        public enum ePlayerNumber
        {
            PlayerOne = 1,
            PlayerTwo = 2,
        }

        private int m_PlayerType;
        private string m_Name;
        private char m_CheckerShape;
        private string m_LastMovement;
        private int m_Score = 0;
        private List<Checker> m_PlayerCheckers = new List<Checker>();

        public int Score
        {
            get { return m_Score;}
            set { m_Score = value;}
        }

        public int PlayerType
        {
            get { return m_PlayerType;}
            set { m_PlayerType = value;}
        }

        public string Name
        {
            get { return m_Name;}
            set { m_Name = value;}
        }

        public List<Checker> PlayerCheckers
        {
            get { return m_PlayerCheckers;}
            set { m_PlayerCheckers = value;}
        }

        public char CheckerShape
        {
            get { return m_CheckerShape;}
            set { m_CheckerShape = value;}
        }

        public string LastMovement
        {
            get { return m_LastMovement;}
            set { m_LastMovement = value;}
        }

        public void InitializePlayer(int i_PlayerType, int i_PlayerNumber, Board i_GameBoard)
        {
            m_PlayerType = i_PlayerType;

            m_LastMovement = null;
            m_CheckerShape = i_PlayerNumber == (int)ePlayerNumber.PlayerOne ? (char)Checker.eSigns.PlayerO : (char)Checker.eSigns.PlayerX;
            i_GameBoard.InitializePlayerCheckers(this, i_PlayerNumber);
        }

        public bool IsPlayerPC(int i_PlayerType)
        {
            return i_PlayerType == (int)ePLayerType.Computer;
        }

        private bool checkIfKing(Checker currentPlayerChecker)
        {
            return currentPlayerChecker.Sign == (char)Checker.eSigns.PlayerOKing ||
                   currentPlayerChecker.Sign == (char)Checker.eSigns.PlayerXKing;
        }

        public List<Move> CreateRegularArray(Board i_GameBoard)
        {
            List<Move> result = new List<Move>();

            foreach (Checker value in m_PlayerCheckers)
            {
                value.AddRegularMoves(i_GameBoard, result);
            }

            return result;
        }

        public List<Move> CreateSkipsArray(Board i_GameBoard)
        {
            List<Move> result = new List<Move>();

            foreach (Checker value in m_PlayerCheckers)
            {
                value.AddSkipMoves(i_GameBoard, result);
            }

            return result;
        }

        public int CalculateWinnerScore(Player i_Loser)
        {
            int winnerValueOfCheckers, loserValueOfCheckers;

            winnerValueOfCheckers = getCheckersValue();
            loserValueOfCheckers = i_Loser.getCheckersValue();

            return winnerValueOfCheckers - loserValueOfCheckers;
        }

        private int getCheckersValue()
        {
            int result = 0;

            foreach (Checker currentGameChecker in m_PlayerCheckers)
            {
                result += currentGameChecker.Value;
            }

            return result;
        }

        public bool CanQuit(Player i_Opponent)
        {
            int OpponentCheckersValue, currentPlayerCheckersValue;

            OpponentCheckersValue = i_Opponent.getCheckersValue();
            currentPlayerCheckersValue = getCheckersValue();

            return currentPlayerCheckersValue < OpponentCheckersValue;
        }

        public void ResetCheckers(Board i_GameBoard)
        {
            int playerNumber = m_CheckerShape == (char)Checker.eSigns.PlayerO ? (int)ePlayerNumber.PlayerOne : (int)ePlayerNumber.PlayerTwo;

            m_PlayerCheckers.Clear();
            InitializePlayer(m_PlayerType, playerNumber, i_GameBoard);
        }

        public bool IfNoLegalMoveLeft(List<Move> playerRegularMoves, List<Move> playerSkipMoves)
        {
            return playerRegularMoves.Count == 0 && playerSkipMoves.Count == 0;
        }

        public bool IsPc()
        {
            return m_PlayerType == (int)ePLayerType.Computer;
        }

        public Move GetPCMove(List<Move> playerRegularMoves, List<Move> playerSkipMoves)
        {
            Move   result = new Move();
            Random rnd = new Random();
            int    rndIndextMove;

            if(playerSkipMoves.Count != 0)
            {
                rndIndextMove = rnd.Next(playerSkipMoves.Count);
                result = playerSkipMoves[rndIndextMove];
            }
            else if(playerRegularMoves.Count != 0)
            {
                rndIndextMove = rnd.Next(playerRegularMoves.Count);
                result = playerRegularMoves[rndIndextMove];
            }

            return result;
        }
    }
}
