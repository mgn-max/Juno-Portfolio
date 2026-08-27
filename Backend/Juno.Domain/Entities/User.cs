using Juno.Domain.Enums.UserEnums;
using Juno.Domain.Exceptions;
using Juno.Domain.Validators;


namespace Juno.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Login { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public UserStatus Status { get; private set; }
        public string? PhotoUrl { get; private set; }
        public DateTime CreatedAt { get; private set; }

        protected User() { }

        public User(string name, string login, string email, string passwordHash, string? photoUrl)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("O nome do usuario não pode ser vazio");
            if (string.IsNullOrWhiteSpace(login))
                throw new ArgumentException("O login do usuario não pode ser vazio");
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("O email do usuario não pode ser vazio");
            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new ArgumentException("A senha do usuario não pode ser vazia");
            
            if (!EmailValidator.IsValidEmail(email))
                throw new ArgumentException("O email do usuario não é valido");
            if(EmailValidator.IsValidEmail(login))
                throw new ArgumentException("O login do usuario não pode ser um email");

            Id = Guid.NewGuid();
            Name = name.Trim();
            Login = login.Trim().ToLower();
            Email = email.Trim().ToLower();
            PasswordHash = passwordHash;
            Status = UserStatus.Active;
            PhotoUrl = string.IsNullOrEmpty(photoUrl) ? null : photoUrl.Trim();
            CreatedAt = DateTime.UtcNow;
        }

        public void Update(string? name, string? login, string? email)
        {
            if (!string.IsNullOrWhiteSpace(name))
                Name = name.Trim();
            if (!string.IsNullOrWhiteSpace(login))
            {
                if (EmailValidator.IsValidEmail(login))
                    throw new ArgumentException("O login do usuario não pode ser um email");
                Login = login.Trim().ToLower();
            }
            if (!string.IsNullOrWhiteSpace(email))
            {
                if (!EmailValidator.IsValidEmail(email))
                    throw new ArgumentException("O email do usuario não é valido");
                Email = email.Trim().ToLower();
            }
        }

        public void UpdatePassword(string newPasswordHash)
        {
            if (string.IsNullOrWhiteSpace(newPasswordHash))
                throw new ArgumentException("A senha do usuario não pode ser vazia");
            PasswordHash = newPasswordHash;
        }

        public void Activate()
        {
            ValidateStatus(UserStatus.Active);
            Status = UserStatus.Active;
        }

        public void Inactive()
        {
            ValidateStatus(UserStatus.Inactive);
            Status = UserStatus.Inactive;
        }

        public void Suspended()
        {
            ValidateStatus(UserStatus.Suspended);
            Status = UserStatus.Suspended;
        }

        public void UpdatePhotoUrl(string? newPhotoUrl)
        {
            PhotoUrl = string.IsNullOrEmpty(newPhotoUrl) ? null : newPhotoUrl.Trim();
        }

        private void ValidateStatus(UserStatus newStatus)
        {
            if (Status == newStatus)
                throw new BusinessRuleException("O status do usuario não pode ser o mesmo");
        }

    }
}
