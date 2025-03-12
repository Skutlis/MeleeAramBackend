using System;
using MeleeAram.webapi.ExternalAPI.ResponseObjects;

namespace AramGeddon.webapi.ExternalAPI.ResponseObjects;

public class ChampionApplicationData
{
    public string Key { get; set; }

    public string Name { get; set; }

    public string Title { get; set; }

    // Ref Image.full in DDragonChampionInfo.cs
    public string Image { get; set; }

    public List<string> Tags { get; set; }

    public ChampionApplicationData(ChampionData response)
    {
        Key = response.Key;
        Name = response.Name;
        Title = response.Title;
        Image = response.Image.Full;
        Tags = response.Tags;
    }
}

public class ChampionApplicationDataColleciton
{
    public Dictionary<string, ChampionApplicationData> ChampionData { get; set; } = new Dictionary<string, ChampionApplicationData>();

    public ChampionApplicationDataColleciton(Dictionary<string, ChampionData> response)
    {
        foreach (string championName in response.Keys)
        {
            ChampionData[championName] = new ChampionApplicationData(response[championName]);
        }
    }
}