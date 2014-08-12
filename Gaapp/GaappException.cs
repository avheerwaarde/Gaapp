using System;

namespace Gaapp
{
    public class GaappException : Exception
    {
        public GaappException(string message)
            : base(message)
        {
        }

        public GaappException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
