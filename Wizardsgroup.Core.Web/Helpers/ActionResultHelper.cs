using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using Wizardsgroup.Core.Web.Constants;

namespace Wizardsgroup.Core.Web.Helpers
{
    public delegate void ActionResultHandler<T>(T item);    

    public class ActionResultHelper<T> where T : class,new() 
    {
        public event ActionResultHandler<T> Method;        

        public ActionResultMessage Process(T entity, ModelStateDictionary modalState,string successMessage)
        {
            var listMessages = new List<string>();
            var actionResultMessage = new ActionResultMessage { ActionStatus = ActionStatusResult.Failed };

            if (modalState.IsValid)
            {
                try
                {
                    _InvokeMethod(entity);
                    actionResultMessage.ActionStatus = ActionStatusResult.Success;
                    listMessages.Add(successMessage);
                }
                catch (DbUpdateException ex)
                {
                    string message = SqlErrorHelper.SqlErrorMessage(ex);
                    listMessages.Add(message);
                }
                catch (DbEntityValidationException ex)
                {                    
                    var validationErrors = ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors);
                    listMessages.AddRange(validationErrors.Select(x => x.ErrorMessage));
                }
                catch (SqlException ex)
                {
                    string message = SqlErrorHelper.SqlErrorMessage(ex);
                    listMessages.Add(message);
                }
                catch (Exception ex)
                {
                    listMessages.Add(ex.Message);
                }
            }

            var allErrors = modalState.Values.SelectMany(v => v.Errors);
            listMessages.AddRange(allErrors.Select(a => a.ErrorMessage));

            actionResultMessage.Messages = listMessages;
            return actionResultMessage;

        }

        private void _InvokeMethod(T entity)
        {
            if (Method != null)
            {
                Method(entity);
            }
        }

    }
}