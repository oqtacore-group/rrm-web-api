using System;

namespace Oqtacore.Rrm.Domain.Exceptions
{
    [Serializable]
    public class NotFoundException : AppException
    {
        public NotFoundException(string message, Exception innerException = null) : base(message, innerException)
        {
        }

        public override string ErrorCode
        {
            get { return ErrorCodes.ENTITY_NOT_FOUND; }
        }
    }
}