using System.ComponentModel.DataAnnotations;

namespace Web_API_ECommerce_Demo.Dtos.AuthDtos
{
    public class AppUserLoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string password { get; set; }
    }
}
