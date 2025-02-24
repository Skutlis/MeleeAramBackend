using System;
using AutoMapper;
using MeleeAram.webapi.DTOs;
using MeleeAram.webapi.Entities;

namespace MeleeAram.webapi.Utility;

public class Mappers : Profile
{

    public Mappers()
    {

        CreateMap<Player, GetPlayerDTO>();
    }

}
