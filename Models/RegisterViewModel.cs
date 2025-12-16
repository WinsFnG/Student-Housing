using System.ComponentModel.DataAnnotations;
namespace Student_housing.Models
{
    public class RegisterViewModel
    {
        [Required]       
        public string Username { get; set; } = default!;
        [Required]
        public string Email { get; set; } = default!;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set;  } = default!;
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match!")]
        public string ConfirmPassword { get; set; } = default!;
    }
}
