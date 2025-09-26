namespace Oqtacore.Rrm.Domain.Exceptions
{
    [Serializable]
    public abstract class AppException : ApplicationException
    {
        protected AppException(string message, Exception innerException = null)
            : base(message, innerException)
        {
        }

        public abstract string ErrorCode { get; }
    }
}