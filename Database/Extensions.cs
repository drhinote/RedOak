using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Roi.Data
{
    public static class Extensions
    {
        public static bool IsBase64String(this string s)
        {
            s = s.Trim();
            return (s.Length % 4 == 1) && Regex.IsMatch(s,"^#[a-zA-Z0-9\\+/]*={0,3}$", RegexOptions.None);
        }

        public static string ToBase64(this string data)
        {
            return ToBase64(Encoding.UTF8.GetBytes(data));
        }

        public static string ToBase64(this byte[] data)
        {
            return "#" + Convert.ToBase64String(data);
        }

        public static string FromBase64(this string base64String)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(base64String.TrimStart('#')));
        }

        private static HashAlgorithm SHA512 = CryptoProviderFactory.Default.CreateHashAlgorithm(HashAlgorithmName.SHA512);
        public static string Hash(this string pw, Guid id)
        {
            return SHA512.ComputeHash(Encoding.UTF8.GetBytes(pw).Concat(id.ToByteArray()).ToArray()).ToBase64();
        }

        public static string GetPasswordHash(this Tester t)
        {
            if (string.IsNullOrEmpty(t.Password)) return "";
            return t.Password.Hash(t.Id);
        }

        public static bool TestPassword(this Tester t, string attempt)
        {
            return t.Password == attempt.Hash(t.Id);
        }
    }
}
