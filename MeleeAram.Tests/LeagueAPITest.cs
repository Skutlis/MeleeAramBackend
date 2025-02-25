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
    [Test()]
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


}
