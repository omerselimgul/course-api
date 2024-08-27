using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.InputOutputModels.Users
{
    public class TokenModel
    {
        public String AccessToken { get; init; }
        public String RefreshToken { get; init; }
        public string UserId { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string UserName { get; init; }
        public string EMail { get; init; }
        public string PhoneNumber { get; init; }
        public string EducatorAvesisLink {  get; init; }
        public List<string> Roles { get; set; }  // Kullanıcı rollerini tutacak alan

    }
}
