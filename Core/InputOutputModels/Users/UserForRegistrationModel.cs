using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.InputOutputModels.Users
{
    public record UserForRegistrationModel
    {
        public string? FirstName { get; init; }
        public string? LastName { get; init; }

        [Required(ErrorMessage = "UserName is required")]
        public string? UserName { get; init; }
        public string? Email { get; init; }
       
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; init; }
        public string? EducatorAvesisLink { get; init; }
        public ICollection<string> Roles { get; init; } = new List<string>();

        public UserForRegistrationModel()
        {
            Password = "User123";
        }
    }
}
