using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPIRestaurant.Core.Application.Dtos.Account;
using WebAPIRestaurant.Core.Domain.Settings;
using WebAPIRestaurant.Infrastructure.Identity.Entities;
using System.Security.Cryptography;
using WebAPIRestaurant.Core.Application.Enums;
using WebAPIRestaurant.Core.Application.Interfaces.Services;

namespace WebAPIRestaurant.Infrastructure.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JWTSettings _jwtSettings;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JWTSettings> jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {

            AuthenticationResponse response = new();

            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No Accounts registered with this user name: {request.UserName}";
                return response;
            }

            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No Accounts registered with this email: {request.UserName}";
                return response;
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Invalid credentials for {request.UserName}";
                return response;
            }

            if (!user.EmailConfirmed)
            {
                response.HasError = true;
                response.Error = $"Account no confirmed for {request.UserName}";
                return response;
            }

            JwtSecurityToken jwtSecurity = await GenerateJWToken(user);

            response.Id = user.Id;
            response.Email = user.Email;
            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            response.Roles = rolesList.ToList();
            response.UserName = user.UserName;
            response.Name = user.Name;
            response.LastName = user.LastName;
            response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurity);
            var refreshToken = GenerateRefreshToken();
            response.RefreshToken = refreshToken.Token;

            return response;
        }

        public async Task<RegisterResponse> RegisterWaiterAsync(RegisterRequest request)
        {
            RegisterResponse response = new();
            response.HasError = false;

            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);

            if (userWithSameUserName != null)
            {
                response.HasError = true;
                response.Error = $"UserName {request.UserName} is already taken";
                return response;
            }

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);

            if (userWithSameEmail != null)
            {
                response.HasError = true;
                response.Error = $"Email {request.Email} is already taken";
                return response;
            }

            var user = new ApplicationUser
            {
                Name = request.Name,
                LastName = request.LastName,
                Email = request.Email,
                EmailConfirmed = true,
                UserName = request.UserName,
            };


            var result = await _userManager.CreateAsync(user, request.Password);
            if(result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.Waiter.ToString());

            }
            else
            {
                response.HasError = true;
                response.Error = $"An error occurred trying to register this user.";
                return response;
            }
            return response;
        }

        public async Task<RegisterResponse> RegisterAdministratorAsync(RegisterRequest request)
        {
            RegisterResponse response = new();
            response.HasError = false;

            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);

            if (userWithSameUserName != null)
            {
                response.HasError = true;
                response.Error = $"UserName {request.UserName} is already taken";
                return response;
            }

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);

            if (userWithSameEmail != null)
            {
                response.HasError = true;
                response.Error = $"Email {request.Email} is already taken";
                return response;
            }

            var user = new ApplicationUser
            {
                Name = request.Name,
                LastName = request.LastName,
                Email = request.Email,
                EmailConfirmed = true,
                UserName = request.UserName,
            };


            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.Administrator.ToString());

            }
            else
            {
                response.HasError = true;
                response.Error = $"An error occurred trying to register this user.";
                return response;
            }
            return response;
        }

        #region "Private Methods"
        private async Task<JwtSecurityToken> GenerateJWToken(ApplicationUser user)
        {
            var userClaim = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();
            foreach(var role in roles)
            {
                roleClaims.Add(new Claim("roles", role));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaim)
            .Union(roleClaims);

            var symmectricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmectricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken
                (
                    issuer: _jwtSettings.Issuer,
                    audience: _jwtSettings.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                    signingCredentials: signingCredentials

                );

            return jwtSecurityToken;
        }

        private RefreshToken GenerateRefreshToken()
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow
            };
        }

        private string RandomTokenString()
        {
            using var rngCrytoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCrytoServiceProvider.GetBytes(randomBytes);
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }

        #endregion
    }
}
