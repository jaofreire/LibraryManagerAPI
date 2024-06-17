
using System.Text;

namespace LibraryManager.Api.Commons
{
    public class HashPassword
    {
        private const int WorkFactor = 12;

        public string CreatePasswordHash(string password)
            => BCrypt.Net.BCrypt.HashPassword(password, WorkFactor);


        public bool VerifyPassword(string passwordSubmited, string hashPassword)
            => BCrypt.Net.BCrypt.Verify(passwordSubmited, hashPassword);

    }
}
