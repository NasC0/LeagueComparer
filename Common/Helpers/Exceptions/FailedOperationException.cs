using System;

namespace Helpers.Exceptions
{
    public class FailedOperationException : Exception
    {
        public FailedOperationException()
        {
        }

        public FailedOperationException(string message)
            : base(message)
        {
        }

        public FailedOperationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
