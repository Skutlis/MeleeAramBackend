using System;

namespace MeleeAram.webapi.Entities;

public class Player : IMaEntities
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string GamerTag { get; set; }
    public string Puuid { get; set; }
    public IEnumerable<OwnedChamps> OwnedChamps { get; set; }


    public void Update(IMaEntities entity)
    {
        throw new NotImplementedException();
    }
}
