using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeleeAram.webapi.Entities;

public class GameMode : IMaEntities
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Name { get; set; }
    [NotMapped]
    public IEnumerable<BannedChampion> BannedChampions { get; set; }

    public void Update(IMaEntities entity)
    {
        throw new NotImplementedException();
    }
}
