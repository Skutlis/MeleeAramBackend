using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace MeleeAram.webapi.Entities;

public class BannedChampion : IAgEntities
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int ChampionId { get; set; }
    public int GameModeId { get; set; }
    [NotMapped]
    public virtual BanList GameMode { get; set; }
    [NotMapped]
    public virtual Champion Champion { get; set; }


    public void Update(IAgEntities entity)
    {
        throw new NotImplementedException();
    }
}
