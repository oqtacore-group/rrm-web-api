
namespace Oqtacore.Rrm.Domain.Exceptions
{
    public class ErrorCodes
    {
        public const string UNKNOWN                = "0000";

        public const string VALIDATION_FAILED       = "0010";
        public const string ENTITY_NOT_FOUND       = "0011";
        public const string ENTITY_IS_DUPLICATE = "0012";
        public const string ASSOCIATION_NOT_FOUND = "0013";
        public const string AUTHENTICATION_FAILED = "0014";
        public const string AUTHORIZATION_FAIL = "0015";
        public const string OPRTATION_IS_NOT_ALLOWED = "0016";
        public const string CHILD_ENTITY_IS_ORPHAN = "0017";
        public const string DATABASE_INTEGRATION_VAIOLATED_ON_DELETE = "0018";
    }
}
