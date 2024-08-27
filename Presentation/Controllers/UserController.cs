using AutoMapper;
using Core.InputOutputModels.Users;
using Core.Utilities.Results;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IServiceManager _service;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;



        public UserController(UserManager<User> userManager, IServiceManager service, IMapper mapper)
        {
            _service = service;
            _userManager = userManager;
            _mapper = mapper;

        }
        [HttpGet]
        public async Task<IDataResult<List<UserInfoOutputModel>>> GetAllUsersAsync()
        {
            var users = _userManager.Users.ToList();
            var usersOutputModel = _mapper.Map<List<UserInfoOutputModel>>(users);

            return new SuccessDataResult<List<UserInfoOutputModel>>(usersOutputModel);

        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneUserByIdAsync([FromRoute] Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                return NotFound(); // Kullanıcı bulunamazsa 404 Not Found döndür
            }

            var userOutputModel = _mapper.Map<UserInfoOutputModel>(user);

            return Ok(new SuccessDataResult<UserInfoOutputModel>(userOutputModel));
        }


        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(string userId, [FromBody] UserInfoInputModel userUpdateModel)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound(); // Kullanıcı bulunamazsa 404 Not Found döndür
            }
            UpdateUser(user, userUpdateModel);

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return StatusCode(200, new SuccessResult()); // Başarılı güncelleme durumu
            }
            else
            {
                return BadRequest(result.Errors); // Güncelleme başarısız olursa hataları döndür
            }
        }

        private static void UpdateUser(User user, UserInfoInputModel userUpdateModel)
        {
            var userProperties = typeof(User).GetProperties();
            var inputModelProperties = typeof(UserInfoInputModel).GetProperties();

            foreach (var inputModelProp in inputModelProperties)
            {
                var value = inputModelProp.GetValue(userUpdateModel);
                if (value != null)
                {
                    var userProp = userProperties.FirstOrDefault(p => p.Name == inputModelProp.Name && p.PropertyType == inputModelProp.PropertyType);
                    if (userProp != null)
                    {
                        userProp.SetValue(user, value);
                    }
                }
            }
        }
    }
}
