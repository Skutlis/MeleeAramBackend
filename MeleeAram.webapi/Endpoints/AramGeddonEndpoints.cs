using System;
using System.Linq.Expressions;
using AutoMapper;
using MeleeAram.webapi.DTOs;
using MeleeAram.webapi.Entities;
using MeleeAram.webapi.Repository;
using MeleeAram.webapi.Utility;
using Microsoft.AspNetCore.Mvc;
using AramGeddon.webapi.Services;

namespace MeleeAram.webapi.Endpoints;

public static class MA_endpoint
{


    public static void ConfigureMaEndpoint(this WebApplication app)
    {
        var AG = app.MapGroup("/");




    }

    #region Player endpoints
    [ProducesResponseType(StatusCodes.Status200OK)]
    public static async Task<IResult> Connect(AgRepository<Player> maRepository, IMapper mapper, string gamertag)
    {

        Expression<Func<Player, bool>> findPlayerExpr = p => p.GamerTag == gamertag;

        if (maRepository.Exists(findPlayerExpr))
        {
            Player match = await maRepository.GetEntityByColumnValue(findPlayerExpr);
            return TypedResults.Ok(new Payload<GetPlayerDTO>() { Data = mapper.Map<GetPlayerDTO>(match) });
        }
        return TypedResults.Ok();
    }

    #endregion


    #region Champion endpoints



    #endregion

    #region Gamemode endpoints

    #endregion

}
