using System;
using AramGeddon.webapi.ExternalAPI.ResponseObjects;
using AutoMapper;
using MeleeAram.webapi.DTOs;
using MeleeAram.webapi.Entities;
using MeleeAram.webapi.ExternalAPI.ResponseObjects;

namespace MeleeAram.webapi.Utility;

public class Mappers : Profile
{

    public Mappers()
    {

        CreateMap<Player, GetPlayerDTO>();
        CreateMap<ChampionApplicationData, Champion>();
    }

}
