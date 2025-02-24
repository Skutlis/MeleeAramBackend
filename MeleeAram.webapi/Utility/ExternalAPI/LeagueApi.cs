using System;

namespace MeleeAram.webapi.Utility.ExternalAPI;

public class LeagueApi
{
    static HttpClient client = new HttpClient();
    static string RIOT_API_KEY { get; set; }
    static string RIOT_SUMMONER_API { get; set; }
    static string RIOT_CHAMP_MASTERY_API { get; set; }
    static string DDRAGON_API { get; set; }

    public LeagueApi()
    {
        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        RIOT_API_KEY = configuration.GetValue<string>("RIOT_API_KEY")!;
        RIOT_SUMMONER_API = configuration.GetValue<string>("RIOT_SUMMONER_API")!;
        RIOT_CHAMP_MASTERY_API = configuration.GetValue<string>("RIOT_CHAMP_MASTERY_API")!;
        DDRAGON_API = configuration.GetValue<string>("DDRAGON_API")!;
    }

    public async Task<List<ChampionMastery>> GetChampionMasteries(string Puuid)
    {
        List<ChampionMastery> chMasteries = new List<ChampionMastery>();
        string url = RIOT_CHAMP_MASTERY_API + "/" + Puuid + "?api_key=" + RIOT_API_KEY;
        Console.WriteLine(url);
        Uri reqUri = new Uri(url);
        HttpResponseMessage response = await client.GetAsync(reqUri);
        if (response.IsSuccessStatusCode)
        {
            chMasteries = await response.Content.ReadFromJsonAsync<List<ChampionMastery>>(); // Try to remove fields in ChampionMastery that we dont need(?)
        }

        return chMasteries;
    }
}

