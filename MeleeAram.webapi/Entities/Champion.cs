using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeleeAram.webapi.Entities;

public class Champion : IAgEntities
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Name { get; set; }
    public string Key { get; set; }
    public string Attack { get; set; }
    public string Image { get; set; }
    public string[] Tags { get; set; }
    [NotMapped]
    public virtual IEnumerable<BannedChampion> BannedChampions { get; set; }
    [NotMapped]
    public virtual IEnumerable<OwnedChamps> OwnedChamps { get; set; }

    public void Update(IAgEntities entity)
    {
        throw new NotImplementedException();
    }
}
