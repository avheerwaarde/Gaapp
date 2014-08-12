using System;

namespace Gaapp
{
    public class GaappDoesNotExistException : Exception
    {
        public GaappDoesNotExistException(string message)
            : base(message)
        {
        }

        public GaappDoesNotExistException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
