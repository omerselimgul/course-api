using Core.InputOutputModels.Users;
using Core.Utilities.Results;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
	[ApiController]
	[Route("api/authentication")]
	public class AuthenticationController : ControllerBase
	{
		private readonly IServiceManager _service;

		public AuthenticationController(IServiceManager service)
		{
			_service = service;
		}

		[HttpPost]
		//[ServiceFilter(typeof(ValidationFilterAttribute))]
		public async Task<IResult> RegisterUser([FromBody]UserForRegistrationModel userForRegistrationModel)
		{
			var result = await _service
				.AuthenticationService
				.RegisterUser(userForRegistrationModel);
			if (!result.Succeeded)
			{
				var errorMessage = String.Empty;
				foreach(var error in result.Errors)
				{
					errorMessage += error.Description + "\n";
				}
				return new ErrorResult(errorMessage);
			}
			return new SuccessResult("İşleminiz başarıyla gerçekleşti");
			 
		}

		[HttpPost("login")]
		//[ServiceFilter(typeof(ValidationFilterAttribute))]
		public async Task<IDataResult<TokenModel>> Authenticate([FromBody]UserForAuthenticationModel user)
		{
            if (!await _service.AuthenticationService.ValidateUser(user))
                return new ErrorDataResult<TokenModel>("Giriş yapılamadı."); // 401 durumunu temsil eden ErrorDataResult kullanılıyor.

            var tokenDto = await _service
				.AuthenticationService
				.CreateToken(populateExp: true);

            return new SuccessDataResult<TokenModel>(tokenDto);


        }

        [HttpPost("refresh")]
		//[ServiceFilter(typeof(ValidationFilterAttribute))]
		public async Task<IDataResult<TokenModel>> Refresh([FromBody]TokenModel tokenModel)
		{
			var tokenModelToReturn = await _service
            .AuthenticationService
            .RefreshToken(tokenModel);

            //return Ok(tokenModelToReturn);
            return new SuccessDataResult<TokenModel>(tokenModelToReturn);


        }

	}
}
