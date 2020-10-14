using System.Drawing;

namespace Ex02_checkers
{
    public class Board
    { 
        private readonly char[,] m_GameBoard;
        private int m_Height;
        private int m_Width;

        public Board(int height, int width)
        {
            m_Height = height;
            m_Width = width;
            m_GameBoard = new char[height, width];
            initializeBufferZone();
        }

        public char this[int i_Row, int i_Colum]
        {
            get { return m_GameBoard[i_Row, i_Colum];}
            set { m_GameBoard[i_Row, i_Colum] = value; }
        }

        public int Height
        {
            get { return m_Height;}
            set { m_Height = value;}
        }

        public int Width
        {
            get { return m_Width;}
            set { m_Width = value;}
        }

        private void initializeBufferZone()
        {
            int startLoopIndex = (m_Height / 2) - 1;
            int endLoopIndex = (m_Height / 2) + 1;

            for (int i = startLoopIndex; i < endLoopIndex; i++)
            {
                for(int j = 0; j < m_Width; j++)
                {
                    m_GameBoard[i, j] = (char)Checker.eSigns.Empty;
                }
            }
        }

        public void InitializePlayerCheckers(Player i_Player, int i_PlayerNumber)
        {
            int startLoopValue, endLoopValue;

            startLoopValue = i_PlayerNumber == (int)Player.ePlayerNumber.PlayerOne ? 0 : (m_Height / 2) + 1;
            endLoopValue = i_PlayerNumber == (int)Player.ePlayerNumber.PlayerOne ? (m_Height / 2) - 1 : m_Height;

            for (int i = startLoopValue; i < endLoopValue; i++)
            {
                for(int j = 0; j < m_Width; j++)
                {
                    if ((i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0))
                    {
                        m_GameBoard[i, j] = i_Player.CheckerShape;
                        i_Player.PlayerCheckers.Add(new Checker(new Point(i, j), m_GameBoard[i, j]));
                    }
                    else
                    {
                        m_GameBoard[i, j] = (char)Checker.eSigns.Empty;
                    }
                }
            }
        }

        public void UpdateMoveOnBoard(Move i_Move, Checker i_GameCheckerToMove)
        {
            char tempSign = i_GameCheckerToMove.Sign;

            DeleteFromBoard(i_Move.From);
            if (transferToKing(i_Move.To, i_GameCheckerToMove))
            {
                i_GameCheckerToMove.Sign = i_GameCheckerToMove.FriendSign;
                i_GameCheckerToMove.FriendSign = tempSign;
            }

            addToBoard(i_Move.To, i_GameCheckerToMove.Sign);
        }

        private bool transferToKing(Point i_Location, Checker i_GameCheckerToMove)
        {
            bool result = (i_Location.X == Height - 1 || i_Location.X == 0) && !i_GameCheckerToMove.IsKing();

            return result;
        }

        private void addToBoard(Point i_PointToAdd, char i_Sign)
        {
            m_GameBoard[i_PointToAdd.X, i_PointToAdd.Y] = i_Sign;
        }

        public void DeleteFromBoard(Point i_PointToErase)
        {
            m_GameBoard[i_PointToErase.X, i_PointToErase.Y] = (char)Checker.eSigns.Empty;
        }

        public void Clear()
        {
            for(int i = 0; i < Height; i++)
            {
                for(int j = 0; j < Width; j++)
                {
                    this[i, j] = (char)Checker.eSigns.Empty;
                }
            }
        }
    }
}
