using Juno.Application.DTOs.AuthDtos;
using Juno.Application.DTOs.UserDtos;
using Juno.Application.Interfaces;
using Juno.Domain.Entities;
using Juno.Domain.Exceptions;
using Juno.Domain.Interfaces;
using Juno.Domain.Validators;

namespace Juno.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public AuthService(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }
        public async Task<LoginResponseDto> Login(LoginRequestDto request)
        {
            var normalized = request.LoginOrEmail.Trim().ToLower();
            var login = await _userRepository.GetByLoginOrEmail(normalized);

            if(login == null || !BCrypt.Net.BCrypt.Verify(request.Password, login.PasswordHash))
                throw new BusinessRuleException("Login ou senha inválidos");

            var token = _tokenService.GenerateToken(login);

            return new LoginResponseDto(
                login.Id,
                login.Name,
                login.Email,
                token,
                DateTime.UtcNow.AddHours(1)
                ); 
        }

        public async Task<UserDetailsDto> Register(RegisterDto request)
        {
            var normalizedLogin = request.Login.Trim().ToLowerInvariant();
            var normalizedEmail = request.Email.Trim().ToLowerInvariant();

            LengthValidation(request);
            PasswordValidator.PasswordValidation(request.Password);
            if (!EmailValidator.IsValidEmail(request.Email))
                throw new BusinessRuleException("Email inválido");

            if (EmailValidator.IsValidEmail(request.Login))
                throw new BusinessRuleException("Login não pode ser um email");

            if (await _userRepository.ExistsByLogin(normalizedLogin))
                throw new BusinessRuleException("Login já cadastrado no sistema");

            if (await _userRepository.ExistsByEmail(normalizedEmail))
                throw new BusinessRuleException("Email já cadastrado no sistema");

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            User register = new User(request.Name.Trim(), normalizedLogin, normalizedEmail, passwordHash, string.IsNullOrWhiteSpace(request.PhotoUrl) ? null : request.PhotoUrl.Trim());
            await _userRepository.Add(register);

            return new UserDetailsDto
           (
                register.Id,
                register.Name,
                register.Login,
                register.Email,
                register.Status,
                register.CreatedAt
            );

        }

        private static void LengthValidation(RegisterDto request)
        {
            var normalizedName = request.Name.Trim();
            var normalizedLogin = request.Login.Trim();
            var normalizedEmail = request.Email.Trim();
            var normalizedPhotoUrl = string.IsNullOrWhiteSpace(request.PhotoUrl) ? null : request.PhotoUrl.Trim();

            if (normalizedName?.Length > 100)
                throw new BusinessRuleException("Nome muito longo");
            if (normalizedLogin?.Length > 50)
                throw new BusinessRuleException("Login muito longo");
            if (normalizedEmail?.Length > 200)
                throw new BusinessRuleException("Email muito longo");
            if (request.Password?.Length > 200)
                throw new BusinessRuleException("Senha muito longa");
            if (normalizedPhotoUrl?.Length > 500)
                throw new BusinessRuleException("Imagem invalida por favor entre em contato com o suporte ou tente uma outra imagem");
        }
    }
}
