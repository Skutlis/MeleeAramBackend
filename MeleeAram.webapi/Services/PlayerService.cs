using System;
using AramGeddon.webapi.ExternalAPI;
using MeleeAram.webapi.ExternalAPI;
using MeleeAram.webapi.Utility;

namespace AramGeddon.webapi.Services;

public class PlayerService
{
    public async Task<Payload<string>> GetPuuid(ILeagueApi leagueApi, string summonerName, string gamerTag)
    {
        Payload<string> response = await leagueApi.GetPuuidBySummonerName(summonerName, gamerTag);

        if (!response.success)
        {
            return new Payload<string> { success = false, StatusMessage = response.StatusMessage };
        }

        return new Payload<string> { Data = response.Data };
    }

}
