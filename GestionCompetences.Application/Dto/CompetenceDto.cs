using GestionCompetences.Domain.Enum;
using GestionCompetences.Entitie.Competence;
using GestionCompetences.Enum;

namespace GestionCompetences.API.Dto
{
    public record CompetenceDto (Guid Id, string Nom, string? Note,DateTime DateDeDebut,Visibilite Visibilite,
                                    Statut Statut, int Position, Guid CategorieId,Guid  NiveauId)
    {

        public static CompetenceDto From(CompetenceUtilisateur competence)
            => new(competence.Id, competence.Nom, competence.Note, competence.DateDeDebut,
                    competence.Visibilite, competence.Statut, competence.Position,
                    competence.CategorieId, competence.NiveauId);
    }
}
