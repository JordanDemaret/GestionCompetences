using GestionCompetences.Application.Common.Hasher;
using Konscious.Security.Cryptography;

using System.Security.Cryptography;
using System.Text;

namespace GestionCompetences.Infrastructure.Services
{
    public class PasswordHasherService : IPasswordHasher
    {
        public string Hash(string motDePasseEnClair)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(16);
            byte[] hash = Compute(motDePasseEnClair, salt);

            return $"{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
        }

        public bool Verify(string motDePasseEnClair, string motDePasseHache)
        {
            var parts = motDePasseHache.Split('.');
            if (parts.Length != 2) return false;

            byte[] salt = Convert.FromBase64String(parts[0]);
            byte[] hashAttendu = Convert.FromBase64String(parts[1]);

            byte[] hashCalcule = Compute(motDePasseEnClair, salt);

            return CryptographicOperations.FixedTimeEquals(hashCalcule, hashAttendu);
        }

        private static byte[] Compute(string motDePasse, byte[] salt)
        {
            using var argon2 = new Argon2id(Encoding.UTF8.GetBytes(motDePasse))
            {
                Salt = salt,
                DegreeOfParallelism = 2,
                Iterations = 4,
                MemorySize = 65536
            };

            return argon2.GetBytes(32);
        }

    }
}
