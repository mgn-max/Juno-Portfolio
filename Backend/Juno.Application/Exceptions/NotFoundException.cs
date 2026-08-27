using System;
using System.Collections.Generic;
using System.Text;

namespace Juno.Application.Exceptions
{
    internal class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }
}
