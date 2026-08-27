using Juno.Domain.Arguments.OfficeArguments;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Juno.Domain.Entities
{
    public class Client
    {
        public Guid Id { get; private set; }
        public Guid CreatedByUserId { get; private set; }
        public Guid? OfficeId { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string? Email { get; private set; }
        public string? PhoneNumber { get; private set; }
        public string? DocumentNumber { get; private set; }
        public bool Status { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private Client() { }

        public Client(ClientCreation data)
        {
            #region[normalization]
            var name = data.Name.Trim();
            var email = string.IsNullOrEmpty(data.Email) ? null : data.Email;
            var phone = string.IsNullOrWhiteSpace(data.PhoneNumber) ? null : new string(data.PhoneNumber.Where(char.IsDigit).ToArray());
            var document = string.IsNullOrWhiteSpace(data.DocumentNumber) ? null : new string(data.DocumentNumber.Where(char.IsDigit).ToArray());
            #endregion



            Id = Guid.NewGuid();
            CreatedByUserId = data.UserId;
            OfficeId = data.OfficeId;
            Name = data.Name;
            Email = data.Email;
            PhoneNumber = data.PhoneNumber;
            DocumentNumber = data.DocumentNumber;
            Status = true;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
