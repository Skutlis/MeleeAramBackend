using System;
using System.Linq.Expressions;
using AramGeddon.webapi.DTOs;
using AramGeddon.webapi.ExternalAPI;
using AramGeddon.webapi.Services;
using AutoMapper;
using MeleeAram.webapi.DTOs;
using MeleeAram.webapi.Entities;
using MeleeAram.webapi.Repository;
using MeleeAram.webapi.Utility;
using Microsoft.AspNetCore.Mvc;

namespace AramGeddon.webapi.Endpoints;

public static class ChampionEndpoints
{

    private static ChampionService _championService = new ChampionService();
    private static PlayerService playerService = new PlayerService();

    public static void ConfigureChampionEndpoints(this WebApplication app)
    {
        var champions = app.MapGroup("/champions");
        champions.MapPut("", UpdateChampions);
        champions.MapPost("/getChampions", GetChampions);
        champions.MapGet("/all", GetAllChampions);

    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public static async Task<IResult> UpdateChampions(IAgRepository<Champion> champRepo, ILeagueApi leagueApi, IMapper mapper)
    {
        try
        {
            Payload<List<Champion>> allChampsResponse = await _championService.GetChampionsData(leagueApi, mapper);
            if (!allChampsResponse.success) return TypedResults.InternalServerError(new Payload<string>() { success = false, StatusMessage = "Could not fetch champions from riot API" }); // Return the error message from the API
            foreach (var champ in allChampsResponse.Data)
            {
                if (champRepo.Exists(c => c.Name == champ.Name))
                {
                    int id = champRepo.GetEntityByExpr(c => c.Name == champ.Name).Result.Id;
                    await champRepo.UpdateEntityById(id, champ);
                    continue;
                }
                await champRepo.CreateEntity(champ);
            }
            return TypedResults.Ok(new Payload<string>() { StatusMessage = "All Champions are up to date!" });
        }
        catch (Exception ex)
        {
            return TypedResults.InternalServerError(new Payload<string>() { success = false, StatusMessage = "An error occured while storing champions to the database" });
        }
    }

    public static async Task<IResult> GetAllChampions(IAgRepository<Champion> champRepo, IMapper mapper)
    {
        IEnumerable<Champion> champs = await champRepo.GetAll();
        return TypedResults.Ok(new Payload<IEnumerable<GetChampionsDTO>>() { Data = mapper.Map<IEnumerable<GetChampionsDTO>>(champs) });
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public static async Task<IResult> GetChampions(IAgRepository<Champion> champRepo, IAgRepository<Player> playerRepo, IAgRepository<OwnedChamp> ownedChampsRepo, IMapper mapper, GetPlayerDTO playerDTO)
    {

        Expression<Func<Player, bool>> findExistingPlayerFunc = p => p.GamerTag == playerDTO.GamerTag && p.SummonerName == playerDTO.SummonerName;
        Player player = await playerRepo.GetEntityByExpr(findExistingPlayerFunc);
        if (player == null) return TypedResults.NotFound(new Payload<List<GetChampionsDTO>>() { success = false, StatusMessage = "Player not found!" });
        Expression<Func<OwnedChamp, bool>> findChampByPlayerIdExpr = oc => oc.PlayerId == player.Id;
        IEnumerable<OwnedChamp> ownedChamps = await ownedChampsRepo.GetEntitiesByExpr(findChampByPlayerIdExpr);
        IEnumerable<GetChampionsDTO> champions = ownedChamps.Select(oc => mapper.Map<GetChampionsDTO>(oc.Champion));

        return TypedResults.Ok(new Payload<IEnumerable<GetChampionsDTO>> { Data = champions });
    }



}
