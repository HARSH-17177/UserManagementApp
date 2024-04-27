using System.ComponentModel.DataAnnotations;

namespace UserRoleManagementFrontend.Models
{
    public class AuthenticationRequest
    {

        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string password { get; set; }
    }
}
