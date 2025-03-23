using System;
using AramGeddon.webapi.DTOs;
using AramGeddon.webapi.ExternalAPI;
using AramGeddon.webapi.ExternalAPI.ResponseObjects;
using AutoMapper;
using MeleeAram.webapi.Entities;
using MeleeAram.webapi.ExternalAPI;
using MeleeAram.webapi.ExternalAPI.ResponseObjects;
using MeleeAram.webapi.Utility;

namespace AramGeddon.webapi.Services;

public class ChampionService
{
    private List<string> _meeles = new List<string>();
    private void loadMelees()
    {
        // Path to your CSV file
        string filePath = "Resources/Melee.csv";

        try
        {
            // Read all lines, skip the header, and remove quotes
            _meeles = File.ReadAllLines(filePath)
                .Skip(1) // Skip the "Champion Name" header
                .Select(line => line.Trim('"')) // Remove the surrounding quotes`
                .ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading file: {ex.Message}");
        }
    }

    public async Task<Payload<List<Champion>>> GetChampionsData(ILeagueApi leagueApi, IMapper mapper)
    {
        if (_meeles.Count == 0) loadMelees();

        Payload<ChampionApplicationDataColleciton> response = await leagueApi.GetDDragonChampionData();
        if (!response.success) return new Payload<List<Champion>>() { success = false, StatusMessage = response.StatusMessage };
        List<Champion> champions = new List<Champion>();

        foreach (string championName in response.Data.ChampionData.Keys)
        {
            Champion current = mapper.Map<Champion>(response.Data.ChampionData[championName]);
            current.Attack = "Ranged";
            if (_meeles.Contains(current.Name))
            {
                current.Attack = "Melee";
            }

            champions.Add(current);
        }


        return new Payload<List<Champion>>() { Data = champions };
    }

    public async Task<Payload<List<ChampionMasteryDTO>>> GetChampionMasteries(ILeagueApi leagueApi, IMapper mapper, string puuid)
    {
        Payload<List<ChampionMastery>> response = await leagueApi.GetChampionMasteries(puuid);

        if (!response.success) return new Payload<List<ChampionMasteryDTO>>() { success = false, StatusMessage = response.StatusMessage };

        List<ChampionMasteryDTO> result = mapper.Map<List<ChampionMasteryDTO>>(response.Data);

        return new Payload<List<ChampionMasteryDTO>>() { Data = result };
    }



}
