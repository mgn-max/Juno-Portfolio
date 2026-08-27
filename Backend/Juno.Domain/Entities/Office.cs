using Juno.Domain.Arguments.OfficeArguments;
using Juno.Domain.Enums.OfficeEnums;
using Juno.Domain.Validators;

namespace Juno.Domain.Entities
{
    public class Office
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string? Email { get; private set; }
        public string? DocumentNumber { get; private set; }
        public string? PhoneNumber { get; private set; }
        public string? LogoUrl { get; private set; }
        public OfficeStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }

        #region [Address]
        public string? ZipCode { get; private set; }
        public string? Street { get; private set; }
        public string? AddressNumber { get; private set; }
        public string? Neighborhood { get; private set; }
        public string? City { get; private set; }
        public string? State { get; private set; }
        public string? Country { get; private set; }
        #endregion

        protected Office() { }

        public Office(OfficeCreationData data)
        {
            if (string.IsNullOrWhiteSpace(data.Name))
                throw new ArgumentException("O campo 'Name' é obrigatório.");
            if (!string.IsNullOrWhiteSpace(data.Email) && !EmailValidator.IsValidEmail(data.Email))
                throw new ArgumentException("O email é inválido");
            if (!string.IsNullOrWhiteSpace(data.PhoneNumber) && !PhoneNumberValidator.IsValidPhoneNumber(data.PhoneNumber))
                throw new ArgumentException("O número de telefone é inválido");

            #region [Normalized]
            var name = data.Name.Trim();
            var email = string.IsNullOrWhiteSpace(data.Email) ? null : data.Email.Trim();
            var documentNumber = string.IsNullOrWhiteSpace(data.DocumentNumber) ? null : new string(data.DocumentNumber.Where(char.IsDigit).ToArray());
            var phoneNumber = string.IsNullOrWhiteSpace(data.PhoneNumber) ? null : new string(data.PhoneNumber.Where(char.IsDigit).ToArray());
            var logoUrl = string.IsNullOrWhiteSpace(data.LogoUrl) ? null : data.LogoUrl.Trim();
            var zipCode = string.IsNullOrWhiteSpace(data.ZipCode) ? null : data.ZipCode.Trim();
            var street = string.IsNullOrWhiteSpace(data.Street) ? null : data.Street.Trim();
            var addressNumber = string.IsNullOrWhiteSpace(data.AddressNumber) ? null : data.AddressNumber.Trim();
            var neighborhood = string.IsNullOrWhiteSpace(data.Neighborhood) ? null : data.Neighborhood.Trim();
            var city = string.IsNullOrWhiteSpace(data.City) ? null : data.City.Trim();
            var state = string.IsNullOrWhiteSpace(data.State) ? null : data.State.Trim();
            var country = string.IsNullOrWhiteSpace(data.Country) ? null : data.Country.Trim();
            #endregion

            ValidateLength.Length(name, 100);
            ValidateLength.Length(email, 200);
            ValidateLength.Length(documentNumber, 50);
            ValidateLength.Length(phoneNumber, 15);
            ValidateLength.Length(logoUrl, 500);

            #region [AddressLength]
            ValidateLength.Length(zipCode, 15);
            ValidateLength.Length(street, 255);
            ValidateLength.Length(addressNumber, 20);
            ValidateLength.Length(neighborhood, 100);
            ValidateLength.Length(city, 100);
            ValidateLength.Length(state, 50);
            ValidateLength.Length(country, 60);
            #endregion

            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            DocumentNumber = documentNumber;
            PhoneNumber = phoneNumber;
            LogoUrl = logoUrl;
            Status = OfficeStatus.Active;
            CreatedAt = DateTime.UtcNow;

            #region [AddressCreate]
            ZipCode = zipCode;
            Street = street;
            AddressNumber = addressNumber;
            Neighborhood = neighborhood;
            City = city;
            State = state;
            Country = country;
            #endregion
        }

        public void UpdateBasicInfo(UpdateBasicInfoData data)
        {
            if (!string.IsNullOrWhiteSpace(data.Email) && !EmailValidator.IsValidEmail(data.Email))
                throw new ArgumentException("O email é inválido");
            if (!string.IsNullOrWhiteSpace(data.PhoneNumber) && !PhoneNumberValidator.IsValidPhoneNumber(data.PhoneNumber))
                throw new ArgumentException("O número de telefone é inválido");

            ValidateLength.Length(data.Email, 200);
            ValidateLength.Length(data.DocumentNumber, 50);
            ValidateLength.Length(data.PhoneNumber, 15);
            ValidateLength.Length(data.LogoUrl, 500);
            if (!string.IsNullOrWhiteSpace(data.Name))
            {
                ValidateLength.Length(data.Name, 100);
                Name = data.Name.Trim();
            }
            Email = string.IsNullOrWhiteSpace(data.Email) ? null : data.Email.Trim();
            DocumentNumber = string.IsNullOrWhiteSpace(data.DocumentNumber) ? null : new string(data.DocumentNumber.Where(char.IsDigit).ToArray());
            PhoneNumber = string.IsNullOrWhiteSpace(data.PhoneNumber) ? null : new string(data.PhoneNumber.Where(char.IsDigit).ToArray());
            LogoUrl = string.IsNullOrWhiteSpace(data.LogoUrl) ? null : data.LogoUrl.Trim();
        }
        public void UpdateAddress(AddressUpdateData data)
        {
            ValidateLength.Length(data.ZipCode, 15);
            ValidateLength.Length(data.Street, 255);
            ValidateLength.Length(data.AddressNumber, 20);
            ValidateLength.Length(data.Neighborhood, 100);
            ValidateLength.Length(data.City, 100);
            ValidateLength.Length(data.State, 50);
            ValidateLength.Length(data.Country, 60);

            ZipCode = string.IsNullOrWhiteSpace(data.ZipCode) ? null : data.ZipCode.Trim();
            Street = string.IsNullOrWhiteSpace(data.Street) ? null : data.Street.Trim();
            AddressNumber = string.IsNullOrWhiteSpace(data.AddressNumber) ? null : data.AddressNumber.Trim();
            Neighborhood = string.IsNullOrWhiteSpace(data.Neighborhood) ? null : data.Neighborhood.Trim();
            City = string.IsNullOrWhiteSpace(data.City) ? null : data.City.Trim();
            State = string.IsNullOrWhiteSpace(data.State) ? null : data.State.Trim();
            Country = string.IsNullOrWhiteSpace(data.Country) ? null : data.Country.Trim();
        }
    }
}
