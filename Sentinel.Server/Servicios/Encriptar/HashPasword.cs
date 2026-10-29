using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;

namespace Sentinel.Server.Servicios.Encriptar
{
    public class HashPasword : IHashPassword
    {
        public void CrearClaveHash(string password, out byte[] hash, out byte[] salt)
        {
            using var hmac = new HMACSHA512();
            salt = hmac.Key;
            hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        public bool VerificarClaveHash(string password, byte[] hash, byte[] salt)
        {
            using var hmac = new HMACSHA512(salt);
            var computed = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computed.SequenceEqual(hash);
        }
    }
}
