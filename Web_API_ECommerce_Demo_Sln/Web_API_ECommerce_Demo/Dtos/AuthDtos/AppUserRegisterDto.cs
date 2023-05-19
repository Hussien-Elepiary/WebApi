using System.ComponentModel.DataAnnotations;

namespace Web_API_ECommerce_Demo.Dtos.AuthDtos
{
    public class AppUserRegisterDto
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        [RegularExpression("^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[@$!%*#?&^])[A-Za-z\\d@$!%*#?&^]{10,}$",
            ErrorMessage = "requires at least one uppercase letter, at least one lowercase letter, at least one digit, at least one special character, 10 chracters"
            )]
        public string PassWord { get; set; }
    }
}
