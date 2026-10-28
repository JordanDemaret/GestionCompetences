using GestionCompetences.Domain.Enum;
using GestionCompetences.Entitie.Competence;
using GestionCompetences.Enum;

namespace GestionCompetences.API.Dto
{
    public record CompetenceDto (Guid Id, string Nom, string? Note,DateTime DateDeDebut,Visibilite Visibilite,
                                    Statut Statut, int Position, Guid CategorieId,string CategorieNom,
                                    Guid  NiveauId, string NiveauLibelle,Guid? certificatId, string NomCertificat)
    {

        /// <summary>
        /// Mappe une entité <see cref="CompetenceUtilisateur"/> avec ses relations vers un <see cref="CompetenceDto"/>.
        /// </summary>
        /// <param name="competence">L'entité compétence source incluant ses entités liées (catégorie, niveau, certificat).</param>
        /// <returns>Une nouvelle instance de <see cref="CompetenceDto"/>.</returns>
        public static CompetenceDto From(CompetenceUtilisateur competence)
            => new(competence.Id, competence.Nom, competence.Note, competence.DateDeDebut,
                    competence.Visibilite, competence.Statut, competence.Position,
                    competence.CategorieId,competence.Categorie.Nom,
                    competence.NiveauId, competence.Niveau.Label,
                    competence.CertificatId,
                    competence.CertificatObtenu?.NomCertificat ?? "");
    }
}
