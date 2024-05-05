using System.Security.Cryptography;
using System.Text;

namespace Token_based_authentication_and_middleware.Supports
{
    public class MD5HashGenerator
    {
        public static string GenerateMD5(string input)
        {
            // Create an instance of the MD5CryptoServiceProvider class
            using (MD5 md5 = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to a hexadecimal string representation.
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }
}
