using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace MeleeAram.webapi.ExternalAPI.ResponseObjects;

// Root object representing the entire API response
public class DDragonChampionResponse
{
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("format")]
    public string Format { get; set; }

    [JsonPropertyName("version")]
    public string Version { get; set; }

    [JsonPropertyName("data")]
    public Dictionary<string, ChampionData> ChampionData { get; set; }
}

// Represents individual champion data (e.g., Aatrox, Ahri)
public class ChampionData
{
    [JsonPropertyName("version")]
    public string Version { get; set; }

    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("key")]
    public string Key { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("blurb")]
    public string Blurb { get; set; }

    [JsonPropertyName("info")]
    public ChampionInfo Info { get; set; }

    [JsonPropertyName("image")]
    public ChampionImage Image { get; set; }

    [JsonPropertyName("tags")]
    public List<string> Tags { get; set; }

    [JsonPropertyName("partype")]
    public string Partype { get; set; }

    [JsonPropertyName("stats")]
    public ChampionStats Stats { get; set; }
}

