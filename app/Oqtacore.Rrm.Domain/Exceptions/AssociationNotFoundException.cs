using System;

namespace Oqtacore.Rrm.Domain.Exceptions
{
    [Serializable]
    public class AssociationNotFoundException : AppException
    {
        public AssociationNotFoundException(string message, Exception innerException = null)
            : base(message, innerException)
        {
        }

        public override string ErrorCode
        {
            get { return ErrorCodes.ASSOCIATION_NOT_FOUND; }
        }
    }
}