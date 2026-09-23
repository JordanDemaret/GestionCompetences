using GestionCompetences.Application.Common.Auth;
using System.Security.Cryptography;

namespace GestionCompetences.Infrastructure.Auth
{
    public class PasswordHasherService : IPasswordHasher
    {
        private const int SaltSize = 128;
        private const int KeySize = 64;
        private const int Iterations = 100_000;

        public string Hash(string motDePasseEnClair)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(motDePasseEnClair, salt, Iterations, HashAlgorithmName.SHA512,KeySize);

            return $"{Iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
        }

        public bool Verify(string motDePasseEnClair, string motDePasseHache)
        {
            if (motDePasseHache is null)
            {
                return false;
            }

            var parts = motDePasseHache.Split('.');
            if (parts.Length != 3) return false;

            int iterations = int.Parse(parts[0]);
            byte[] salt = Convert.FromBase64String(parts[1]);
            byte[] hashAttendu = Convert.FromBase64String(parts[2]);

            byte[] hashCalcule = Rfc2898DeriveBytes.Pbkdf2(motDePasseEnClair, salt, iterations, HashAlgorithmName.SHA512, KeySize);

            return CryptographicOperations.FixedTimeEquals(hashCalcule, hashAttendu);
        }
    }
}
