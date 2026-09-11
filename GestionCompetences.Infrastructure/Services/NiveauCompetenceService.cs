using GestionCompetences.Application.Common.CommandQuerySeparation;
using GestionCompetences.Application.Common.Results;
using GestionCompetences.Application.Competence.Commande;
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
            catch
            {
                return Result<IEnumerable<NiveauCompetence>>.Failure(NiveauCompetenceErrors.NiveauCompetencesException);
            }
        }

        public Result Handle(AddNiveauCompetence command)
        {
            try
            {
                if (_dBContext.Niveau.Any(nc => nc.Label == command.Label))
                    return Result.Failure(NiveauCompetenceErrors.NiveauCompetencesLabelExiste);

                _dBContext.Niveau.Add(new NiveauCompetence()
                {
                    Label = command.Label
                });
                _dBContext.SaveChanges();
                return Result.Success(); 
            }
            catch
            {
                return Result.Failure(NiveauCompetenceErrors.NiveauCompetencesException);
            }
        }

        public Result Handle(ModifierNiveauCompetence command)
        {
            try
            {
                NiveauCompetence? niveau = _dBContext.Niveau.FirstOrDefault(nc => nc.Id == command.Id);

                if (niveau is null)
                    return Result.Failure(NiveauCompetenceErrors.NiveauCompetencesPasTrouver);
                if (niveau.Label == command.Label)
                    return Result.Failure(NiveauCompetenceErrors.NiveauCompetencesPasModifier);
                if (_dBContext.Niveau.Any(nc => nc.Label == command.Label))
                    return Result.Failure(NiveauCompetenceErrors.NiveauCompetencesLabelExiste);

                niveau.Label = command.Label;
                _dBContext.SaveChanges();
                return Result.Success();

            }
            catch
            {
                return Result.Failure(NiveauCompetenceErrors.NiveauCompetencesException);
            }
            
        }
    }
}
