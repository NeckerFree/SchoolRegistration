using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using SR.Models.DTOs;
using SR.DataAccess;
using SR.Models;
using SR.BusinessRules;
using SR.Models.Models;

namespace SR.MinApi.EndPoints;

public static class DTOEvaluationEndpoints
{
    public static void MapDTOEvaluationEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/DTOEvaluation");

        group.MapGet("/{id}/{starts}", async ([AsParameters] EvalFilter evalFilter , SchoolContext db) =>
        {
            IQueryable<DTOEvaluation> evaluationsByCourse;
            using (var brEvaluation = new BREvaluation(db))
            {
                evaluationsByCourse = brEvaluation.GetEvaluations(evalFilter.Id, evalFilter.Stars);
            }

            return evaluationsByCourse;
        })
        .WithName("GetEvaluationsFiltered");
    }
}
