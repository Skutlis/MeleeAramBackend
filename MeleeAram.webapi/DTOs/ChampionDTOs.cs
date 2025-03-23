using System;
using AramGeddon.webapi.Enums;

namespace AramGeddon.webapi.DTOs;

public class GetChampionsDTO
{
    public string Name { get; set; }
    public string Key { get; set; }
    public string Attack { get; set; }
    public string Image { get; set; }
    public string[] Tags { get; set; }
}
public class ChampionCollectionDTO
{
    List<GetChampionsDTO> Champions { get; set; }
}

public class ChampionMasteryDTO
{
    public string ChampionId { get; set; }
    public int ChampionPoints { get; set; }
}

public class UpdateChampAction
{
    public string Key { get; set; }
    public UpdateChampion updateChampion { get; set; }
}
