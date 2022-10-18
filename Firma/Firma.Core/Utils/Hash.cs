#region References
using System.Security.Cryptography;
using System.Text; 
#endregion

namespace Firma.Core.Utils
{
    public class Hash
    {
        public static string Encritys(string persona)
        {
            using SHA384 sha384Hash = SHA384.Create();
            //From String to byte array
            byte[] sourceBytes = Encoding.UTF8.GetBytes(persona);
            byte[] hashBytes = sha384Hash.ComputeHash(sourceBytes);
            string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);
            return hash;
        }
    }
}
