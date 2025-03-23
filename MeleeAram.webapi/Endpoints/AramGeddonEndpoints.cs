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


    #endregion


    #region Champion endpoints



    #endregion

    #region Gamemode endpoints

    #endregion

}
