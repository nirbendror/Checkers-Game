using System;

namespace CheckersGUI
{
    public class DetailsReceivedEventArgs : EventArgs
    {
        private string m_Player1Name, m_Player2Name;
        private int m_GameBoardSize;
        private bool m_IsComputer;

        public DetailsReceivedEventArgs(string i_Player1, string i_Player2, int i_GameBoardSize, bool i_IsComputer)
        {
            m_Player1Name = i_Player1;
            m_Player2Name = i_Player2;
            m_GameBoardSize = i_GameBoardSize;
            m_IsComputer = i_IsComputer;
        }

        public string PlayerOneName
        {
            get {return m_Player1Name;}
        }

        public string PlayerTwoName
        {
            get { return m_Player2Name;}
        }

        public int GameBoardSize
        {
            get { return m_GameBoardSize;}
        }

        public bool IsPlayer2Computer
        {
            get {return m_IsComputer;}
        }
    }
}
