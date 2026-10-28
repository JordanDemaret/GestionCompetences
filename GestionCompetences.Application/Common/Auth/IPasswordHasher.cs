using System;
using System.Collections.Generic;
using System.Text;

namespace GestionCompetences.Application.Common.Auth
{
    /// <summary>
    /// Définit le contrat pour le hachage et la vérification des mots de passe.
    /// </summary>
    public interface IPasswordHasher
    {
        /// <summary>
        /// Hache un mot de passe en clair.
        /// </summary>
        /// <param name="password">Le mot de passe en clair à hacher.</param>
        /// <returns>La chaîne contenant les itérations, le sel et l'empreinte hachée.</returns>
        string Hash(string password);
        /// <summary>
        /// Vérifie la correspondance entre un mot de passe en clair et son empreinte hachée.
        /// </summary>
        /// <param name="password">Le mot de passe en clair saisi.</param>
        /// <param name="encodedHash">L'empreinte hachée stockée.</param>
        /// <returns><c>true</c> si le mot de passe correspond, sinon <c>false</c>.</returns>
        bool Verify(string password, string? encodedHash);
    }
}
