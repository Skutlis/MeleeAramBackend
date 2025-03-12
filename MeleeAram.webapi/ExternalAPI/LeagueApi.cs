using System;
using System.Net.Http.Headers;
using AramGeddon.webapi.ExternalAPI.ResponseObjects;
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

    public async Task<Payload<string>> GetPuuidBySummonerName(string summonerName, string tag)
    {
        string url = RIOT_SUMMONER_API + $"/{summonerName}" + $"/{tag}" + $"?api_key={RIOT_API_KEY}";
        Uri reqUri = new Uri(url);
        HttpResponseMessage response = await client.GetAsync(reqUri);
        if (response.IsSuccessStatusCode)
        {
            PuuidResponse puuidObject = await response.Content.ReadFromJsonAsync<PuuidResponse>() ?? new PuuidResponse();
            if (puuidObject != null)
            {
                return new Payload<string>() { Data = puuidObject.puuid }; // Success
            }

            return new Payload<string>() { success = false, StatusMessage = $"Unsuccessful request: {response.StatusCode}" }; // Success, but no data retrieved
        }

        return new Payload<string>() { success = false, StatusMessage = $"Unsuccessful request: {response.StatusCode}" }; // Unsuccessful request

    }

    public async Task<Payload<string>> GetLatestDdragonApiVersion()
    {
        string url = DDRAGON_VERSION_API_BASE + "/api/versions.json";
        Uri reqUri = new Uri(url);
        HttpResponseMessage response = await client.GetAsync(reqUri);
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

    public async Task<Payload<ChampionApplicationDataColleciton>> GetDDragonChampionData()
    {
        Payload<string> requestLatestDDragonVersion = await GetLatestDdragonApiVersion();
        if (!requestLatestDDragonVersion.success)
        {
            return new Payload<ChampionApplicationDataColleciton> { success = false, StatusMessage = $"Error at latestDDragonVersionRequest: {requestLatestDDragonVersion.StatusMessage}" };
        }

        string latestDDragonVersion = requestLatestDDragonVersion.Data;

        string url = "https://ddragon.leagueoflegends.com/cdn/" + latestDDragonVersion + "/data/en_US/champion.json";
        Uri reqUri = new Uri(url);
        HttpResponseMessage response = await client.GetAsync(reqUri);
        if (response.IsSuccessStatusCode)
        {
            DDragonChampionResponse championInfo = await response.Content.ReadFromJsonAsync<DDragonChampionResponse>() ?? new DDragonChampionResponse();
            Console.WriteLine(championInfo);
            if (championInfo.ChampionData.Keys.Count() > 0)
            {
                return new Payload<ChampionApplicationDataColleciton>() { Data = new ChampionApplicationDataColleciton(championInfo.ChampionData) }; //Success
            }
            return new Payload<ChampionApplicationDataColleciton>() { success = false, StatusMessage = "Successful request but no data was retrieved" }; //Success, but no data retrieved
        }
        return new Payload<ChampionApplicationDataColleciton>() { success = false, StatusMessage = $"Could not retrieve champion data: {response.StatusCode}" };
    }

}

