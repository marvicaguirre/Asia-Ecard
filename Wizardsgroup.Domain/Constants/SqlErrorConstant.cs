namespace Wizardsgroup.Domain.Constants
{
    public static class SqlErrorConstant
    {
        public const string UpdateRecordConflicted = "update statement conflicted with the column reference";
        public const string DeleteConflicted = "delete statement conflicted with the reference constraint";
        public const string DuplicateKey = "cannot insert duplicate key";

    }
}
