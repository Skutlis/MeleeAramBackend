using System;

namespace MeleeAram.webapi.DTOs;


public class GetPlayerDTO
{
    public string SummonerName { get; set; }
    public string GamerTag { get; set; }
}

public class PostConnectResponseDTO
{
    public string SummonerName { get; set; }
    public string GamerTag { get; set; }
    public bool WasInDb { get; set; }
}


