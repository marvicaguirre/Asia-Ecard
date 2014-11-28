using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Mail;

namespace Wizardsgroup.Utilities.Helpers
{
    public static class EmailHelper
    {
        public static string LastErrorMessage { get; set; }

        public static void Send(List<string> recipients, string subject, string body)
        {
            if (recipients.Count > 0)
            {
                var mail = new MailMessage();

                // TODO: temporarily set the email sender. There is an issue with decoding the email when
                // email comes from the web.config.
                mail.From = new MailAddress("someemail@theEmail.com");
                
                foreach (var recipient in recipients)
                {
                    if (recipient != null)
                    {
                        if (recipient.Trim() != string.Empty)
                            mail.To.Add(new MailAddress(recipient));
                    }
                }

                if (mail.To.Count > 0)
                {
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = false;

                    try
                    {
                        new SmtpClient().Send(mail);

                        LastErrorMessage = "";
                    }
                    catch (Exception ex)
                    {
                        var exception = ex;

                        if (ex.InnerException != null)
                        {
                            exception = ex.InnerException;

                            while (exception.InnerException != null)
                            {
                                exception = exception.InnerException;
                            }
                        }

                        LastErrorMessage = exception.Message;
                    }
                }
            }
        }


        public static void Send(string recipients, string subject, string body)
        {
            if (recipients != string.Empty)
            {
                var mail = new MailMessage();



                mail.From = new MailAddress(ConfigurationManager.AppSettings["NotifierEmail"].ToString());

                mail.To.Add(new MailAddress(recipients));
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = false;
                try
                {
                    new SmtpClient().Send(mail);
                    LastErrorMessage = "";
                }
                catch (Exception ex)
                {
                    var exception = ex;

                    if (ex.InnerException != null)
                    {
                        exception = ex.InnerException;

                        while (exception.InnerException != null)
                        {
                            exception = exception.InnerException;
                        }
                    }
                    LastErrorMessage = exception.Message;
                }
            }
        }
    }
}
