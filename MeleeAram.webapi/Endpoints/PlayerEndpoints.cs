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

public static class PlayerEndpoints
{
    private static PlayerService playerService = new PlayerService();
    private static ChampionService _championService = new ChampionService();

    public static void ConfigurePlayerEndpoints(this WebApplication app)
    {
        var player = app.MapGroup("/player");

        player.MapPost("/", Connect);


    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public static async Task<IResult> Connect(IAgRepository<Player> playerRepo, IAgRepository<Champion> championRepo, IAgRepository<OwnedChamp> ownedChampsRepo, IMapper mapper, ILeagueApi leagueApi, GetPlayerDTO connection)
    {
        string summonerName = connection.SummonerName;
        string gamerTag = connection.GamerTag;

        bool playerExistInDb = true;
        Expression<Func<Player, bool>> findPlayerExpr = p => p.GamerTag == gamerTag && p.SummonerName == summonerName;
        Player player = await playerRepo.GetEntityByExpr(findPlayerExpr);
        if (player == null)
        {
            playerExistInDb = false;
            Payload<string> puuidResponse = await playerService.GetPuuid(leagueApi, summonerName, gamerTag);
            if (!puuidResponse.success)
            {
                if (puuidResponse.StatusMessage == "NotFound") return TypedResults.NotFound();
                else return TypedResults.InternalServerError();
            }

            player = mapper.Map<Player>(connection);
            player.Puuid = puuidResponse.Data;

            Player insertedPlayer = await playerRepo.CreateEntity(player);
            if (insertedPlayer == null) return TypedResults.InternalServerError("Failed to initilizer new player in the system");

            Payload<List<ChampionMasteryDTO>> champMasteryResponse = await _championService.GetChampionMasteries(leagueApi, mapper, player.Puuid);

            if (champMasteryResponse.success)
            {
                List<string> championIds = champMasteryResponse.Data.Select(ch => ch.ChampionId).ToList();
                Expression<Func<Champion, bool>> masteryToChampsExpr = c => championIds.Contains(c.Key);
                IEnumerable<Champion> masteryChamps = await championRepo.GetEntitiesByExpr(masteryToChampsExpr);
                IEnumerable<OwnedChamp> ownedChamps = masteryChamps.Select(ch => new OwnedChamp() { ChampionId = ch.Id, PlayerId = player.Id });

                foreach (OwnedChamp c in ownedChamps) await ownedChampsRepo.CreateEntity(c);
            }

        }

        PostConnectResponseDTO requestResponse = mapper.Map<PostConnectResponseDTO>(player);
        requestResponse.WasInDb = playerExistInDb;
        return TypedResults.Ok(requestResponse);
    }



}
