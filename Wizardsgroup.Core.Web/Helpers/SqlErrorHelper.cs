using System;
using Wizardsgroup.Core.Web.Constants;

namespace Wizardsgroup.Core.Web.Helpers
{
    public static class SqlErrorHelper
    {
        public static string SqlErrorMessage(Exception ex)
        {
            string finalMessage = ex.Message;
            string replacement = string.Empty;
            bool hasReplacementString = false;

            Exception exception = ex;
            while (!hasReplacementString && exception != null)
            {
                finalMessage = exception.Message;
                hasReplacementString = HasReplacementString(finalMessage, out replacement);
                exception = exception.InnerException;
            }

            return hasReplacementString ? replacement : finalMessage;
        }

        private static bool HasReplacementString(string errormessage, out string replacement)
        {
            replacement = string.Empty;
            if (errormessage.ToLower().Contains(ErrorMessage.UpdateRecordConflicted.ToLower())
                || errormessage.ToLower().Contains(ErrorMessage.DeleteConflicted.ToLower())
                || errormessage.ToLower().Contains(ErrorMessage.DuplicateKey.ToLower()))
            {
                replacement = SqlErrorMessage(errormessage);
                return true;
            }
            return false;
        }

        public static string SqlErrorMessage(string errormessage)
        {
            //Sql Error Exception
            if (errormessage.ToLower().Contains(ErrorMessage.UpdateRecordConflicted.ToLower()))
            {
                return "Editing is not allowed because the table is in use.";
            }

            if (errormessage.ToLower().Contains(ErrorMessage.DeleteConflicted.ToLower()))
            {
                return "Deleting is not allowed because other records depend on the record you want to delete.";
            }

            if (errormessage.ToLower().Contains(ErrorMessage.DuplicateKey.ToLower()))
            {
                return "Record already exists.";
            }

            return errormessage;
        }


    }
}