using AutoMapper;
using Core.InputOutputModels.Educators;
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

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/educators")]
    public class EducatorController : ControllerBase
    {
        private readonly IServiceManager _service;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public EducatorController(UserManager<User> userManager, IServiceManager service, IMapper mapper)
        {
            _service = service;
            _userManager = userManager;
            _mapper = mapper;

        }
        [HttpGet]
        public async Task<IDataResult<List<EducatorOutputModel>>> GetAllEducatorsAsync()
        {
            var educators = await _userManager.GetUsersInRoleAsync("educator");
            var usersOutputModel = _mapper.Map<List<EducatorOutputModel>>(educators);

            return new SuccessDataResult<List<EducatorOutputModel>>(usersOutputModel);
        }

    }
}
