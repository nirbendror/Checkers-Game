using System.Collections.Generic;
using System.Drawing;

namespace Ex02_checkers
{
    public class Move
    {
        private Point m_From;
        private Point m_To;
        private Point m_Eaten;
        private bool m_isSkipMove = false;

        public Move()
        {
        }

        public Move(Point i_From, Point i_To)
        {
            m_From = i_From;
            m_To = i_To;
        }

        public Point From
        {
            get { return m_From;}
            set { m_From = value;}
        }

        public Point To
        {
            get { return m_To;}
            set { m_To = value;}
        }

        public Point Eaten
        {
            get { return m_Eaten;}
            set { m_Eaten = value;}
        }

        public bool IsSkipMove
        {
            get { return m_isSkipMove;}
            set { m_isSkipMove = value;}
        }


        public Checker ApplyMove(Board i_GameBoard, List<Checker> i_CurrentPlayerCheckers, List<Checker> i_NextPlayerCheckers)
        {
            Checker checkerToMove, checkerToErase;

            checkerToMove = findCheckerByLocation(i_CurrentPlayerCheckers, m_From);
            checkerToMove.Location = m_To;
            i_GameBoard.UpdateMoveOnBoard(this, checkerToMove);

            if (IsSkipMove)
            {
                checkerToErase = findCheckerByLocation(i_NextPlayerCheckers, m_Eaten);
                i_GameBoard.DeleteFromBoard(m_Eaten);
                i_NextPlayerCheckers.Remove(checkerToErase);
            }

            return checkerToMove;
        }

        private Checker findCheckerByLocation(List<Checker> i_CurrentPlayerCheckers, Point i_GameCheckerLocation)
        {
            Checker result = null;

            foreach (Checker currentGameChecker in i_CurrentPlayerCheckers)
            {
                if (currentGameChecker.Location == i_GameCheckerLocation)
                {
                    result = currentGameChecker;
                    break;
                }
            }

            return result;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Move))
                return false;

            Move move = (Move)obj;

            return (this.From == move.From && this.To == move.To);

        }
    }
}
