using MeleeAram.webapi.Utility.ExternalAPI;
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
    public async Task TestGetChampionMasteriesReturnsListOfChampMasteryObject()
    {
        List<ChampionMastery> masteries = await _api.GetChampionMasteries(_testPuuid);

        Assert.That(masteries.Count() > 0);




    }
}
