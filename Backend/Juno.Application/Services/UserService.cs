using Juno.Application.DTOs.UserDtos;
using Juno.Application.Exceptions;
using Juno.Application.Interfaces;
using Juno.Domain.Entities;
using Juno.Domain.Enums.UserEnums;
using Juno.Domain.Exceptions;
using Juno.Domain.Interfaces;
using Juno.Domain.Validators;

namespace Juno.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        private async Task<User> GetSupportUserById(Guid id)
        {
            var user = await _userRepository.GetById(id);
            if (user == null)
                throw new NotFoundException("Usuário não encontrado");
            return user;
        }

        public async Task<UserDto> GetUserById(Guid id)
        {
            var user = await GetSupportUserById(id);
            return new UserDto(user.Id, user.Name, user.Status);
        }

        public async Task<UserDetailsDto> GetUserDetailsById(Guid id)
        {
            var user = await GetSupportUserById(id);
            return new UserDetailsDto(user.Id, user.Name, user.Login, user.Email, user.Status, user.CreatedAt);
        }

        public async Task Update(Guid id, string? name, string? login, string? email)
        {
            var user = await GetSupportUserById(id);

            var normalizedName = string.IsNullOrWhiteSpace(name) ? null : name.Trim();
            var normalizedLogin = string.IsNullOrWhiteSpace(login) ? null : login.Trim().ToLowerInvariant();
            var normalizedEmail = string.IsNullOrWhiteSpace(email) ? null : email.Trim().ToLowerInvariant();

            if (normalizedName?.Length > 100)
                throw new BusinessRuleException("Nome muito longo");

            if (normalizedLogin?.Length > 50)
                throw new BusinessRuleException("Login muito longo");

            if (normalizedEmail?.Length > 200)
                throw new BusinessRuleException("Email muito longo");

            if (normalizedEmail != null && !EmailValidator.IsValidEmail(normalizedEmail))
                throw new BusinessRuleException("Email inválido");
            if(normalizedLogin != null && EmailValidator.IsValidEmail(normalizedLogin))
                throw new BusinessRuleException("Login não pode estar no formato de email");

            if(normalizedLogin != null && await _userRepository.ExistsByLoginExceptId(normalizedLogin, id))
                throw new BusinessRuleException("Login já cadastrado no sistema");
            if(normalizedEmail != null && await _userRepository.ExistsByEmailExceptId(normalizedEmail, id))
                throw new BusinessRuleException("Email já cadastrado no sistema");

            user.Update(normalizedName, normalizedLogin, normalizedEmail);
            await _userRepository.SaveChangesAsync();
        }

        public async Task UpdatePasswords(Guid id, string newPassword)
        {
            if (newPassword.Length > 200)
                throw new BusinessRuleException("Senha muito longa");

            PasswordValidator.PasswordValidation(newPassword);

            var user = await GetSupportUserById(id);
            var newPasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);

            if (BCrypt.Net.BCrypt.Verify(newPassword, user.PasswordHash))
                throw new BusinessRuleException("Nova senha não pode ser igual a senha atual");

            user.UpdatePassword(newPasswordHash);
            await _userRepository.SaveChangesAsync();
        }

        public async Task UpdatePhotoUrl(Guid id, string? photoUrl)
        {
            var normalizedPhotoUrl = string.IsNullOrWhiteSpace(photoUrl) ? null : photoUrl.Trim();
            if (normalizedPhotoUrl?.Length > 500)
                throw new BusinessRuleException("Imagem invalida por favor entre em contato com o suporte ou tente uma outra imagem");

            var user = await GetSupportUserById(id);
            user.UpdatePhotoUrl(normalizedPhotoUrl);
            await _userRepository.SaveChangesAsync();
        }

        public async Task UpdateStatus(Guid id, UserStatus status)
        {
            var user = await GetSupportUserById(id);
            if (status == UserStatus.Active)
                user.Activate();
            else if (status == UserStatus.Inactive)
                user.Inactive();
            else if (status == UserStatus.Suspended)
                user.Suspended();
            await _userRepository.SaveChangesAsync();
        }
    }
}
