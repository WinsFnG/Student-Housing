using System.Security.Cryptography;
using System.Text;

namespace Student_housing.PasswordServices
{
    public static class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            
            // Creating a SHA256 engine to hash the password
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(password);//converting password to byte array
                var hashbytes = sha256.ComputeHash(bytes);// This takes the byte representation of the password and runs SHA256 on it.
                return Convert.ToBase64String(hashbytes);// converting hash byte array to string
            }
        }
        public static bool VerifyPassword(string enteredPassword, string storedHash) // Compares entered password with stored hash (for login erorrs)
        {
            var enteredHash = HashPassword(enteredPassword);
            return enteredHash == storedHash;
        }
    }
}
