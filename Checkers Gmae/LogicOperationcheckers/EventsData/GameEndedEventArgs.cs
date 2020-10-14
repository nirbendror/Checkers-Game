using System;

namespace Ex02_checkers
{
    public class GameEndedEventArgs : EventArgs
    {
        private readonly string r_Message;

        public GameEndedEventArgs(string i_Message)
        {
            r_Message = i_Message;
        }

        public string Message
        {
            get
            {
                return r_Message;
            }
        }
    }
}
