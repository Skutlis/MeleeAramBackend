using System;
using Npgsql.Replication;

namespace MeleeAram.webapi.Entities;

public interface IMaEntities
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public void Update(IMaEntities entity);


}
