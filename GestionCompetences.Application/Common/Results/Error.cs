using System;
using System.Collections.Generic;
using System.Text;

namespace GestionCompetences.Application.Common.Results
{
    /// <summary>
    /// Représente une erreur fonctionnelle ou technique avec un code et une description.
    /// </summary>
    public class Error
    {
        public static Error None => new Error(string.Empty, string.Empty);
        /// <summary>
        /// Crée une nouvelle instance d'<see cref="Error"/> avec un code et une description.
        /// </summary>
        /// <param name="code">Le code identifiant l'erreur.</param>
        /// <param name="description">Le message descriptif de l'erreur.</param>
        /// <returns>Une nouvelle instance d'<see cref="Error"/>.</returns>
        public static Error Create(string code, string description)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(code);
            ArgumentException.ThrowIfNullOrWhiteSpace(description);

            return new Error(code, description);
        }

        public string Code { get; }
        public string Description { get; }

        private Error(string code, string description)
        {
            Code = code;
            Description = description;
        }

        /// <summary>
        /// Renvoie une représentation textuelle de l'erreur au format "Code : Description".
        /// </summary>
        /// <returns>La chaîne décrivant l'erreur.</returns>
        public override string ToString()
        {
            return $"{Code} : {Description}";
        }
    }
}
