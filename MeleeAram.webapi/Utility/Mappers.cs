using System;
using AramGeddon.webapi.DTOs;
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
        // Player maps
        CreateMap<Player, GetPlayerDTO>();
        CreateMap<Player, PostConnectResponseDTO>();
        CreateMap<GetPlayerDTO, Player>();

        // Champions maps
        CreateMap<ChampionApplicationData, Champion>();
        CreateMap<ChampionMastery, ChampionMasteryDTO>();
        CreateMap<Champion, GetChampionsDTO>();
    }



}
