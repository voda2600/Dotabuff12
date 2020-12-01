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
            byte[] hashedPas = Convert.FromBase64String(hashedPassword);
            byte[] data = Encoding.ASCII.GetBytes(password);
            byte[] result;
            SHA512 shaM = new SHA512Managed();
            result = shaM.ComputeHash(data);
            return ByteArraysEqual(hashedPas, result);
        }

        private static bool ByteArraysEqual(byte[] p_BytesLeft, byte[] p_BytesRight)
        {
            if (p_BytesLeft.Length != p_BytesRight.Length)
                return false;

            var length = p_BytesLeft.Length;

            for (int i = 0; i < length; i++)
            {
                if (p_BytesLeft[i] != p_BytesRight[i])
                    return false;
            }

            return true;
        }
    }
}
