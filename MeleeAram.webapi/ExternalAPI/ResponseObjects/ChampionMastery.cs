using System;

namespace MeleeAram.webapi.ExternalAPI.ResponseObjects;


public class ChampionMastery
{
    public string Puuid { get; set; }
    public int ChampionId { get; set; }
    public int ChampionLevel { get; set; }
    public int ChampionPoints { get; set; }
    public long LastPlayTime { get; set; }
    public int ChampionPointsSinceLastLevel { get; set; }
    public int ChampionPointsUntilNextLevel { get; set; }
    public int MarkRequiredForNextLevel { get; set; }
    public int TokensEarned { get; set; }
    public int ChampionSeasonMilestone { get; set; }
    public NextSeasonMilestone NextSeasonMilestone { get; set; }
}

public class NextSeasonMilestone
{
    public Dictionary<string, int> RequireGradeCounts { get; set; }
    public int RewardMarks { get; set; }
    public bool Bonus { get; set; }
    public int TotalGamesRequires { get; set; }
}
