using Microsoft.CodeAnalysis.Scripting;
using System.Text;
using System.Security.Cryptography;
using System.Text;

namespace CWeb.Tools
{
    public class StringFunctionCustom
    {
        public string HashString(string motDePasse)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(motDePasse);
                byte[] hashBytes = sha256.ComputeHash(bytes);
                string motDePasseHache = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                return motDePasseHache;
            }
        }
    }
}
