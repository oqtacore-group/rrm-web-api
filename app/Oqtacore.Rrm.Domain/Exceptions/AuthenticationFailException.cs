using System;

namespace Oqtacore.Rrm.Domain.Exceptions
{
    [Serializable]
    public class AuthenticationFailException : AppException
    {
        public AuthenticationFailException(string message, Exception innerException = null)
            : base(message, innerException)
        {
        }

        public override string ErrorCode
        {
            get { return ErrorCodes.AUTHENTICATION_FAILED; }
        }

    }

}