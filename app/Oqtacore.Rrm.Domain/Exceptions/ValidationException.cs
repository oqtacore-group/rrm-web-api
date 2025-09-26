using System;

namespace Oqtacore.Rrm.Domain.Exceptions
{
    [Serializable]
    public class ValidationException : AppException
    {
        public ValidationException(string message, Exception innerException = null) : base(message, innerException)
        {
        }

        public override string ErrorCode
        {
            get { return ErrorCodes.VALIDATION_FAILED; }
        }

        public string[] ErrorMessages { get; set; }
    }

}