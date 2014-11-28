namespace Wizardsgroup.Core.Web.Constants
{
    public static class ErrorMessage
    {
        public const string UnauthorizedAccess = "UnauthorizedAccess";
        public const string LoginPageNotFound = "The controller for path '{0}/login.aspx' was not found or does not implement IController.";
        public const string UpdateRecordConflicted = "update statement conflicted with the column reference";
        public const string DeleteConflicted = "delete statement conflicted with the reference constraint";
        public const string DuplicateKey = "cannot insert duplicate key";
        public const string NullObject = "Object must not be null";
    }

}