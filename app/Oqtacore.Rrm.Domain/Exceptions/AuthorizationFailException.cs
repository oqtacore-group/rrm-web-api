using System;

namespace Oqtacore.Rrm.Domain.Exceptions
{
    [Serializable]
    public class AuthorizationFailException : AppException
    {
        public AuthorizationFailException(string message, Exception innerException = null)
            : base(message, innerException)
        {
        }

        public override string ErrorCode
        {
            get { return ErrorCodes.AUTHORIZATION_FAIL; }
        }

    }

}