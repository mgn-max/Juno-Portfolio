namespace Juno.Domain.Validators
{
    public class PhoneNumberValidator
    {
        public static bool IsValidPhoneNumber(string? phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return true;

            var digits = new string(phoneNumber.Where(char.IsDigit).ToArray());

            if (digits.Length < 8 || digits.Length > 15)
                return false;

            return true;
        }
    }
}
