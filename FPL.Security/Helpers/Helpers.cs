using System;
using System.Security.Cryptography;
using System.Text;

namespace Application.Security.Helpers
{
    public class Helpers
    {
        public enum ApplicationTypes
        {
            JavaScript = 0,
            NativeConfidential = 1
        };

        public static class Helper
        {
            public static string GetHash(string input)
            {
                HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();

                byte[] byteValue = Encoding.UTF8.GetBytes(input);

                byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);

                return Convert.ToBase64String(byteHash);
            }
        }
    }
}
