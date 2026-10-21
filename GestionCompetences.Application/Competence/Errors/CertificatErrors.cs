using GestionCompetences.Application.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionCompetences.Application.Competence.Errors
{
    public class CertificatErrors
    {
        public static readonly Error CertificatException = Error.Create("CertificatException", "Une erreur est survenue lors de la récupération des certificats.");
        public static readonly Error CertificatNonTrouver = Error.Create("CertificatNontrouver", "Le certificat n'a pas été trouvé");
    }
}
