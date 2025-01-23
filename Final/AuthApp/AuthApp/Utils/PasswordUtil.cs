
namespace AuthApp.Utils
{
    public static class PasswordUtil
    {
        public static string HashPassword(string password)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            return passwordHash;
        }

        public static bool VerifyPassword(string passwordPlain, string passwordHash)
        {
            bool verify = BCrypt.Net.BCrypt.Verify(passwordPlain, passwordHash);
            return verify;
        }
    }
}
