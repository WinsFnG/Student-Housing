namespace Student_housing.Models

{
    public class User
    {
        public int Id { get; set; }

        public string UserName { get; set; } = default!;

        public string Email { get; set; } = default!;

        // hash so its no a key or plain text
        public string PasswordHash { get; set; } = default!;

    }
}
