using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services.Utils
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
