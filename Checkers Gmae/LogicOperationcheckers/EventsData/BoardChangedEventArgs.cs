using System;

namespace Ex02_checkers
{
    public class BoardChangedEventArgs : EventArgs
    {
        private readonly Move r_LastMove;

        public BoardChangedEventArgs(Move i_LastMove)
        {
            r_LastMove = i_LastMove;
        }

        public Move LastMove
        {
            get
            {
                return r_LastMove;
            }
        }
    }
}
