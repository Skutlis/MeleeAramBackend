using System;
using AramGeddon.webapi.Services;
using AutoMapper;
using MeleeAram.webapi.Entities;
using MeleeAram.webapi.Repository;
using MeleeAram.webapi.Utility;
using Microsoft.AspNetCore.Mvc;

namespace AramGeddon.webapi.Endpoints;

public static class ChampionEndpoints
{

    private static ChampionService _championService = new ChampionService();

    public static void ConfigureChampionEndpoints(this WebApplication app)
    {
        var champions = app.MapGroup("/champions");
        champions.MapPost("/Champions", UpdateChampions);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public static async Task<IResult> UpdateChampions(IAgRepository<Champion> champRepo, IMapper mapper)
    {
        try
        {
            Payload<List<Champion>> allChampsResponse = await _championService.GetChampionsData(mapper);
            if (!allChampsResponse.success) return TypedResults.InternalServerError($"External API error: {allChampsResponse.StatusMessage}"); // Return the error message from the API
            foreach (var champ in allChampsResponse.Data)
            {
                if (champRepo.Exists(c => c.Name == champ.Name))
                {
                    int id = champRepo.GetEntityByColumnValue(c => c.Name == champ.Name).Result.Id;
                    await champRepo.UpdateEntityById(id, champ);
                    continue;
                }
                await champRepo.CreateEntity(champ);
            }
            return TypedResults.Ok("Updated all champions");
        }
        catch (Exception ex)
        {
            return TypedResults.InternalServerError($"Database action error: {ex.Message}");
        }

    }

}
