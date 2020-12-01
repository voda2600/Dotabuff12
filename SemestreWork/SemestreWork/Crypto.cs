using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SemestreWork
{
    public class Crypto
    {
        public static string HashPassword(string password)
        {
            byte[] data = Encoding.ASCII.GetBytes(password);
            byte[] result;
            SHA512 shaM = new SHA512Managed();
            result = shaM.ComputeHash(data);
            return Convert.ToBase64String(result);

        }
        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {

            return hashedPassword == (password);
        }

    }
}
