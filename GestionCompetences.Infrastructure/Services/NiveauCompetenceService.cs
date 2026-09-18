using GestionCompetences.Application.Common.Results;
using GestionCompetences.Application.Competence.Errors;
using GestionCompetences.Application.Competence.Query;
using GestionCompetences.Application.Competence.Repository;
using GestionCompetences.Entitie.Competence;

namespace GestionCompetences.Infrastructure.Services
{
    public class NiveauCompetenceService : INiveauCompetenceRepository
    {
        private readonly CompetenceDBContext _dBContext;

        public NiveauCompetenceService(CompetenceDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public Result<IEnumerable<NiveauCompetence>> Handle(GetNiveauCompetenceListe query)
        {
            try
            {
                return Result<IEnumerable<NiveauCompetence>>.Success(_dBContext.Niveau.AsEnumerable());
            }
            catch(Exception)
            {
                return Result<IEnumerable<NiveauCompetence>>.Failure(NiveauCompetenceErrors.NiveauCompetencesException);
            }
        }
    }
}
