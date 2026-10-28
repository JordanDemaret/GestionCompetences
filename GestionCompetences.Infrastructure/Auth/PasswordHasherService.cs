using GestionCompetences.Application.Common.Auth;
using System.Security.Cryptography;

namespace GestionCompetences.Infrastructure.Auth
{
    /// <summary>
    /// Service responsable du hachage sécurisé et de la vérification des mots de passe avec l'algorithme PBKDF2 (SHA-512).
    /// </summary>
    public class PasswordHasherService : IPasswordHasher
    {
        private const int SaltSize = 128;
        private const int KeySize = 64;
        private const int Iterations = 100_000;

        /// <summary>
        /// Hache un mot de passe en clair en générant un sel cryptographique aléatoire de 128 octets et 100 000 itérations PBKDF2-SHA512.
        /// </summary>
        /// <param name="motDePasseEnClair">Le mot de passe en clair à hacher.</param>
        /// <returns>La chaîne formatée "{iterations}.{saltBase64}.{hashBase64}".</returns>
        public string Hash(string motDePasseEnClair)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(motDePasseEnClair, salt, Iterations, HashAlgorithmName.SHA512,KeySize);

            return $"{Iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
        }

        /// <summary>
        /// Vérifie si un mot de passe en clair correspond à l'empreinte hachée stockée en temps constant.
        /// </summary>
        /// <param name="motDePasseEnClair">Le mot de passe en clair à comparer.</param>
        /// <param name="motDePasseHache">L'empreinte hachée au format "{iterations}.{saltBase64}.{hashBase64}".</param>
        /// <returns><c>true</c> si le mot de passe correspond, sinon <c>false</c>.</returns>
        public bool Verify(string motDePasseEnClair, string? motDePasseHache)
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
