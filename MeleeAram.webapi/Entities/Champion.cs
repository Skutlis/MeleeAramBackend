using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeleeAram.webapi.Entities;

public class Champion : IMaEntities
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Name { get; set; }
    public string Attack { get; set; }
    public string Image { get; set; }
    public string[] Tag { get; set; }
    [NotMapped]
    public IEnumerable<BannedChampion> BannedChampions { get; set; }
    [NotMapped]
    public IEnumerable<OwnedChamps> OwnedChamps { get; set; }

    public void Update(IMaEntities entity)
    {
        throw new NotImplementedException();
    }
}
