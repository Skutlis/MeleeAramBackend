using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeleeAram.webapi.Entities;

public class Player : IAgEntities
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string GamerTag { get; set; }
    public string Puuid { get; set; }
    [NotMapped]
    public virtual IEnumerable<OwnedChamps> OwnedChamps { get; set; }
    [NotMapped]
    public virtual IEnumerable<BanList> BanLists { get; set; }


    public void Update(IAgEntities entity)
    {
        throw new NotImplementedException();
    }
}
