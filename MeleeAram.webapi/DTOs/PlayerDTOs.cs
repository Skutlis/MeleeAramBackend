using System;

namespace MeleeAram.webapi.DTOs;

public class GetPlayerDTO
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string GamerTag { get; set; }
    public string Puuid { get; set; }
}
