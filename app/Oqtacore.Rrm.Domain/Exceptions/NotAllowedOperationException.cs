using System;

namespace Oqtacore.Rrm.Domain.Exceptions
{
    [Serializable]
    public class NotAllowedOperationException : AppException
    {
        public NotAllowedOperationException(string message, Exception innerException = null) : base(message, innerException)
        {
        }

        public NotAllowedOperationException(string message, params object[] messageArgs)
            : base(string.Format(message,messageArgs))
        {
        }

        public override string ErrorCode
        {
            get { return ErrorCodes.OPRTATION_IS_NOT_ALLOWED; }
        }
    }
}