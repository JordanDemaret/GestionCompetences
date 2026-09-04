using GestionCompetences.Application.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionCompetences.Application.Utilisateur
{
    public class UtilisateurErrors
    {
        public static Error UtilisationInfoAuthException => Error.Create("Utilisateur.InfoAuthException", "Email ou mot des passe incorrect.");
        public static Error UtilisateurException => Error.Create("Utilisateur.Exception", "Une exception est survenue.");
        public static Error UtilisateurEmailException => Error.Create("Utilisateur.EmailException", "L'email existe déjà.");

    }
}
