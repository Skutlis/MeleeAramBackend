using MeleeAram.webapi.ExternalAPI;
using MeleeAram.webapi.ExternalAPI.ResponseObjects;
using MeleeAram.webapi.Utility;

using Microsoft.Extensions.Configuration;

namespace MeleeAram.Tests;

public class LeagueAPITest
{
    private string _testPuuid { get; set; }
    private LeagueApi _api { get; set; }

    [SetUp]
    public void Setup()
    {
        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        _testPuuid = configuration.GetValue<string>("TESTPUUID")!;
        _api = new LeagueApi();
    }
    [Test]
    public async Task TestGetChampionMasteriesIsNotEmptyt()
    {
        Payload<List<ChampionMastery>> result = await _api.GetChampionMasteries(_testPuuid);
        if (result.success)
        {
            Assert.That(result.Data.Count() > 0);
        }
        else
        {
            Assert.Fail();
        }
    }

    [Test]
    public async Task TestGetLatestDDragonVersionReturnsGoodResponse()
    {
        Payload<string> result = await _api.GetLatestDdragonApiVersion();

        Assert.That(result.success && result.Data.Length > 0);
    }

    [Test]
    public async Task TestGetDDragonChampionData()
    {
        Payload<DDragonChampionResponse> result = await _api.GetDDragonChampionData();
        if (result.success)
        {
            Assert.That(result.Data.ChampionData.Keys.Count() > 0, result.Data.ChampionData.ToString());

        }
        else
        {
            Assert.Fail(result.Data.ToString());
        }

    }

    [Test]
    public async Task TestGetPuuidReturnsPuuid()
    {
        Payload<string> result = await _api.GetPuuidBySummonerName("Gankèren", "GANKS");

        if (result.success)
        {
            Assert.That(result.Data.Length > 0);
        }
        else
        {
            Assert.Fail();
        }
    }



}
