using System.ComponentModel.DataAnnotations;

namespace Student_housing.Models
{
    public class LogInViewModel
    {
        [Required]
        public string Username { get; set; } = default!;
        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = default!;
    }
}
