using Juno.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Juno.Domain.Validators
{
    public class ValidateLength
    {
        public static void Length(string? value, int max)
        {
            if (!string.IsNullOrWhiteSpace(value) && value.Length > max)
                throw new BusinessRuleException($"numero de caracteres do campo '{value}' excedido para esse campo, maximo permitido é {max}");
        }
    }
}
