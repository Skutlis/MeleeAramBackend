using System;
using AramGeddon.webapi.ExternalAPI.ResponseObjects;
using MeleeAram.webapi.ExternalAPI.ResponseObjects;
using MeleeAram.webapi.Utility;

namespace AramGeddon.webapi.ExternalAPI;

public interface ILeagueApi
{
    public Task<Payload<List<ChampionMastery>>> GetChampionMasteries(string Puuid);
    public Task<Payload<string>> GetPuuidBySummonerName(string summonerName, string tag);
    public Task<Payload<string>> GetLatestDdragonApiVersion();
    public Task<Payload<ChampionApplicationDataColleciton>> GetDDragonChampionData();

}
