using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeleeAram.webapi.Entities;

public class OwnedChamps : IMaEntities
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int ChampionId { get; set; }
    public int PlayerId { get; set; }
    [NotMapped]
    public Champion Champion { get; set; }
    [NotMapped]
    public Player Player { get; set; }
    public void Update(IMaEntities entity)
    {
        throw new NotImplementedException();
    }
}
