using System;

namespace Oqtacore.Rrm.Domain.Exceptions
{
    [Serializable]
    public class UnknownException : AppException
    {
        public UnknownException(string message, Exception innerException=null) : base(message, innerException)
        {
        }


        public override string ErrorCode
        {
            get { return ErrorCodes.UNKNOWN; }
        }
    }
}