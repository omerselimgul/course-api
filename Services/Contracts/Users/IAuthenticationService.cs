using Core.InputOutputModels.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts.Users
{
	public interface IAuthenticationService
	{
		Task<IdentityResult> RegisterUser(UserForRegistrationModel userForRegistrationModel);
		Task<bool> ValidateUser(UserForAuthenticationModel userForAuthModel);
		Task<TokenModel> CreateToken(bool populateExp);
		Task<TokenModel> RefreshToken(TokenModel tokenModel);

	}
}
