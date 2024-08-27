using AutoMapper;
using Core.InputOutputModels.Users;
using Entities.Exceptions;
using Entities.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using Services.Contracts;
using Services.Contracts.Users;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
	public class AuthenticationManager : IAuthenticationService
	{
		private readonly ILoggerService _logger;
		private readonly IMapper _mapper;
		private readonly UserManager<User> _userManager;
		private readonly IConfiguration _configuration;

		private User? _user;

		public AuthenticationManager(
			ILoggerService logger,
			IMapper mapper, 
			UserManager<User> userManager, 
			IConfiguration configuration)
		{
			_logger= logger;
			_mapper = mapper;
			_userManager = userManager;
			_configuration = configuration;
		}

		public async Task<TokenModel> CreateToken(bool populateExp)
		{
			var signinCredentails = GetSigninCredentials();
			var claims = await GetClaims();
			var tokenOptions = GenerateTokenOptions(signinCredentails, claims);

			var refreshToken = GenerateRefreshToken();
			_user.RefreshToken = refreshToken;
		

			if(populateExp)
				_user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
			 
			await _userManager.UpdateAsync(_user);

			var accessToken =  new JwtSecurityTokenHandler().WriteToken(tokenOptions);
         

            var roles = await _userManager.GetRolesAsync(_user);
            var rolesList = roles.ToList();
            return new TokenModel()
			{
				AccessToken = accessToken,
				RefreshToken = refreshToken,
				FirstName = _user.FirstName,
				LastName = _user.LastName,
				UserId = _user.Id,
				UserName = _user.UserName,
				EMail = _user.Email,
				PhoneNumber = _user.PhoneNumber,
				EducatorAvesisLink= _user.EducatorAvesisLink,
				Roles = rolesList

			};
		}

		public async Task<IdentityResult> RegisterUser(UserForRegistrationModel userForRegistrationModel)
		{
			var user = _mapper.Map<User>(userForRegistrationModel);
			var result = await _userManager.CreateAsync(user,userForRegistrationModel.Password);

			if  (result.Succeeded)
				await _userManager.AddToRolesAsync(user, userForRegistrationModel.Roles);

			if (userForRegistrationModel.Roles.Any(x => x.ToLower().Equals("educator")))
				EmailSend(userForRegistrationModel);
            
			return result;
			
		}

		public async Task<bool> ValidateUser(UserForAuthenticationModel userForAuthModel)
		{
			_user = await _userManager.FindByNameAsync(userForAuthModel.UserName);
			var result = (_user != null && await _userManager.CheckPasswordAsync(_user, userForAuthModel.Password));
			if(!result)
			{
				_logger.LogWarning($"{nameof(ValidateUser)} : Kimlik doğrulama başarısız oldu. Yanlış kullanıcı adı veya şifre.");
			}
			return result;
		}


		//private methods
		private SigningCredentials GetSigninCredentials()
		{
			var jwtSettings = _configuration.GetSection("JwtSettings");
			var key = Encoding.UTF8.GetBytes(jwtSettings["secretKey"]);
			var secret = new SymmetricSecurityKey(key);
			return new SigningCredentials(secret,SecurityAlgorithms.HmacSha256);
		}
		private async Task<List<Claim>> GetClaims()
		{
			var claims = new List<Claim>()
			{
				new Claim(ClaimTypes.Name,_user.UserName)
			};
			var roles = await _userManager
				.GetRolesAsync(_user);
			foreach (var role in roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, role));
			}
			return claims;
		}

		private JwtSecurityToken GenerateTokenOptions(SigningCredentials signinCredentails,
			List<Claim> claims)
		{
			var jwtSettings = _configuration.GetSection("JwtSettings");
			var tokenOptions = new JwtSecurityToken(
				issuer: jwtSettings["validIssuer"],
				audience: jwtSettings["validAudience"],
				claims: claims,
				expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
				signingCredentials: signinCredentails);
			return tokenOptions;
		}

		private string GenerateRefreshToken()
		{
			var randomNumber = new byte[32];
			using(var rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(randomNumber);
				return Convert.ToBase64String(randomNumber);
			}
		}
		
		private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
		{
			var jwtSettings = _configuration.GetSection("JwtSettings");
			var secretKey = jwtSettings["secretKey"];

			var tokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,
				ValidIssuer = jwtSettings["validIssuer"],
				ValidAudience = jwtSettings["validAudience"],
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			SecurityToken securityToken;

			var principal = tokenHandler.ValidateToken(token,tokenValidationParameters, out securityToken);

			var jwtSecurityToken = securityToken as JwtSecurityToken;

			if(jwtSecurityToken is null || 
				!jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
				 StringComparison.InvariantCultureIgnoreCase)) 
			{
				throw new SecurityTokenException("Invalid token.");
			}
			
			return principal;

		}

		public async Task<TokenModel> RefreshToken(TokenModel tokenDto)
		{
			var principal = GetPrincipalFromExpiredToken(tokenDto.AccessToken);
			var user = await _userManager.FindByNameAsync(principal.Identity.Name);

			//istisnalar
			if (user is null ||
				user.RefreshToken != tokenDto.RefreshToken ||
				user.RefreshTokenExpiryTime <= DateTime.Now)
				
				throw new RefreshTokenBadRequestException();


			_user = user;
			return await CreateToken(populateExp: false);
		}

		private async Task EmailSend(UserForRegistrationModel userForRegistrationModel)
		{

            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Marmara eğitim sistemi", "marmara_egitim_sistemi@anonymous-email-provider.com"));
                message.To.Add(new MailboxAddress("Alıcı", userForRegistrationModel.Email));
                message.Subject = "Şifreniz";

                message.Body = new TextPart("plain")
                {
                    Text = "Merhaba " + userForRegistrationModel.FirstName + " " + userForRegistrationModel.LastName + " Sisteme giriş şifreniz : User123 "
				};

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                    client.Authenticate("example@gmail.com\r\n", "password");

                    client.Send(message);
                    client.Disconnect(true);
                }

                Console.WriteLine("E-posta başarıyla gönderildi.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("E-posta gönderilirken bir hata oluştu: " + ex.Message);
            }
        }
    }
}
