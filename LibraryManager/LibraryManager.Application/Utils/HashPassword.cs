
namespace LibraryManager.Application.Utils
{
    public static class HashPassword
    {
        private const int WorkFactor = 12;

        public static string CreatePasswordHash(string password)
            => BCrypt.Net.BCrypt.HashPassword(password, WorkFactor);


        public static bool VerifyPassword(string passwordSubmited, string hashPassword)
            => BCrypt.Net.BCrypt.Verify(passwordSubmited, hashPassword);
    }
}
