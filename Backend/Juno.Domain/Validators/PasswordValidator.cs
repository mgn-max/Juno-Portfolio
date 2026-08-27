using Juno.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Juno.Domain.Validators
{
    public class PasswordValidator
    {
        public static void PasswordValidation(string password)
        {
            string message = "A senha deve conter pelo menos uma letra maiúscula, uma letra minúscula e um número.";

            if (password.Length < 8)
            {
                throw new BusinessRuleException("A senha deve ter no minimo 8 caracteres");
            }

            bool temRegras = password.Any(char.IsUpper) && password.Any(char.IsLower) && password.Any(char.IsDigit);

            if (!temRegras)
            {
                throw new BusinessRuleException(message);
            }
        }
    }
}
