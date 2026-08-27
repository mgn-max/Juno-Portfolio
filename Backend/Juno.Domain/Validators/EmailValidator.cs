using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Juno.Domain.Validators
{
    public class EmailValidator
    {
        public static bool IsValidEmail(string email)
        {
            try
            {
                var mailAddress = new MailAddress(email);
                return mailAddress.Address == email;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
