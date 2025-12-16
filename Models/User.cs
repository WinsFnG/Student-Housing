namespace Student_housing.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;

        public string Role { get; set; } = "Tenant";   // Tenant role given by default
        public bool IsBanned { get; set; } = false;
        public bool AcceptedTerms { get; set; } = false;
    }
}
