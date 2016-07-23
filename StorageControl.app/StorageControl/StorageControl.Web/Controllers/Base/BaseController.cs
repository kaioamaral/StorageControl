using StorageControl.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StorageControl.Web.Controllers.Base
{
    public class BaseController : Controller
    {
        #region [Alerts]
        public void Success(string message, bool dismissible = true)
        {
            AddAlert(AlertStyles.Success, string.Format("Sucesso! {0}", message), dismissible);
        }

        public void Error(string message, bool dismissible = true)
        {
            AddAlert(AlertStyles.Danger, string.Format("Oops! {0}", message), dismissible);
        }

        public void Warning(string message, bool dismissible = true)
        {
            AddAlert(AlertStyles.Warning, string.Format("Oops! {0}", message), dismissible);
        }

        public void Info(string message, bool dismissible = true)
        {
            AddAlert(AlertStyles.Information, message, dismissible);
        }

        public void AddAlert(string alertStyle, string message, bool dismissible = true)
        {
            var alerts = TempData.ContainsKey(Alert.TempDataKey) ?
                (List<Alert>)TempData[Alert.TempDataKey]
                : new List<Alert>();

            alerts.Add(new Alert() { AlertStyle = alertStyle, Message = message, Dismissible = dismissible });

            TempData[Alert.TempDataKey] = alerts;
        }
        #endregion

        #region [Error messages]
        public List<string> GetErrors()
        {
            var errors = new List<string>();

            foreach (var item in ModelState.Values)
            {
                foreach (var error in item.Errors)
                {
                    errors.Add(error.ErrorMessage);
                }
            }

            return errors;
        }

        public string BuildErrorMessage(List<string> errors)
        {
            string errorMessage = string.Empty;

            foreach (var error in errors)
            {
                errorMessage += error + "\n";
            }

            return errorMessage;
        }
        #endregion
    }
}