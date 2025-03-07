using System;
using System.Net.Http.Headers;
using MeleeAram.webapi.ExternalAPI.ResponseObjects;
using MeleeAram.webapi.Utility;

namespace MeleeAram.webapi.ExternalAPI;

public class LeagueApi
{
    static HttpClient client = new HttpClient();
    static string RIOT_API_KEY { get; set; }
    static string RIOT_SUMMONER_API { get; set; }
    static string RIOT_CHAMP_MASTERY_API { get; set; }
    static string DDRAGON_VERSION_API_BASE { get; set; }

    public LeagueApi()
    {
        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        RIOT_API_KEY = configuration.GetValue<string>("RIOT_API_KEY")!;
        RIOT_SUMMONER_API = configuration.GetValue<string>("RIOT_SUMMONER_API")!;
        RIOT_CHAMP_MASTERY_API = configuration.GetValue<string>("RIOT_CHAMP_MASTERY_API")!;
        DDRAGON_VERSION_API_BASE = configuration.GetValue<string>("DDRAGON_VERSION_API_BASE")!;
    }

    public async Task<Payload<List<ChampionMastery>>> GetChampionMasteries(string Puuid)
    {

        string url = RIOT_CHAMP_MASTERY_API + "/" + Puuid + "?api_key=" + RIOT_API_KEY;
        Uri reqUri = new Uri(url);
        HttpResponseMessage response = await client.GetAsync(reqUri);
        if (response.IsSuccessStatusCode)
        {
            List<ChampionMastery> championMasteries = await response.Content.ReadFromJsonAsync<List<ChampionMastery>>() ?? new List<ChampionMastery>();
            if (championMasteries.Count() > 0)
            {
                return new Payload<List<ChampionMastery>>() { Data = championMasteries }; // Success
            }

            return new Payload<List<ChampionMastery>>() { success = false, StatusMessage = $"Unsuccessful request: {response.StatusCode}" }; // Success, but no data retrieved
        }

        return new Payload<List<ChampionMastery>>() { success = false, StatusMessage = $"Unsuccessful request: {response.StatusCode}" }; // Unsuccessful request
    }

    public async Task<Payload<string>> GetLatestDdragonApiVersion()
    {
        string url = DDRAGON_VERSION_API_BASE + "/api/versions.json";
        HttpResponseMessage response = await client.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            List<string> apiVersions = await response.Content.ReadFromJsonAsync<List<string>>() ?? new List<string>();
            if (apiVersions.Count() > 0)
            {
                return new Payload<string>() { Data = apiVersions[0] }; //Success
            }
            return new Payload<string>() { success = false, StatusMessage = "Successful request but no data was retrieved" }; //Success, but no data retrieved
        }

        return new Payload<string>() { success = false, StatusMessage = $"Unsuccessful request: {response.StatusCode}" }; //Unsuccessful request
    }

    public async Task<Payload<DDragonChampionResponse>> GetDDragonChampionData()
    {
        Payload<string> requestLatestDDragonVersion = await GetLatestDdragonApiVersion();
        if (!requestLatestDDragonVersion.success)
        {
            return new Payload<DDragonChampionResponse> { success = false, StatusMessage = $"Error at latestDDragonVersionRequest: {requestLatestDDragonVersion.StatusMessage}" };
        }

        string latestDDragonVersion = requestLatestDDragonVersion.Data;

        string url = "https://ddragon.leagueoflegends.com/cdn/" + latestDDragonVersion + "/data/en_US/champion.json";

        HttpResponseMessage response = await client.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            DDragonChampionResponse championInfo = await response.Content.ReadFromJsonAsync<DDragonChampionResponse>() ?? new DDragonChampionResponse();
            Console.WriteLine(championInfo);
            if (championInfo.ChampionData.Keys.Count() > 0)
            {
                return new Payload<DDragonChampionResponse>() { Data = championInfo }; //Success
            }
            return new Payload<DDragonChampionResponse>() { Data = championInfo, success = false, StatusMessage = "Successful request but no data was retrieved" }; //Success, but no data retrieved
        }
        return new Payload<DDragonChampionResponse>() { success = false, StatusMessage = $"Could not retrieve champion data: {response.StatusCode}" };
    }

    public async Task<Payload<Dictionary<string, int>>> GetChampionToIdMap()
    {
        string url = RIOT_SUMMONER_API + $"?api_key={RIOT_API_KEY}";
        return new Payload<Dictionary<string, int>>();
    }
}

