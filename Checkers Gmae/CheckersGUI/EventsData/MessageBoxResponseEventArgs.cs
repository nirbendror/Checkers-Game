using System;


namespace CheckersGUI
{
    public class MessageBoxResponseEventArgs : EventArgs
    {
        private bool m_IsYesAnsweredYes;

        public MessageBoxResponseEventArgs(bool i_IsYesAnswered)
        {
            m_IsYesAnsweredYes = i_IsYesAnswered;
        }

        public bool IsAnsweredYes
        {
            get {return m_IsYesAnsweredYes; }
        }
    }
}
