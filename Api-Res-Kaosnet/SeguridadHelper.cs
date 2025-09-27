using System.Security.Cryptography;
using System.Text;

namespace Api_Res_Kaosnet.Helpers
{
    public static class SeguridadHelper
    {
        public static string EncriptarClave(string clave)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(clave);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToHexString(hash); // .NET 5+
        }
    }
}
