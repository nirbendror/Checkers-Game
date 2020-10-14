using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace CheckersGUI
{
    public class MoveEnteredEventArgs : EventArgs
    {
        private Point m_From = new Point();
        private Point m_To = new Point();

        public MoveEnteredEventArgs(Point i_From, Point i_To)
        {
            m_From = i_From;
            m_To = i_To;
        }

        public Point From
        {
            get {return m_From;}
        }

        public Point To
        {
            get {return m_To;}
        }
    }
}
