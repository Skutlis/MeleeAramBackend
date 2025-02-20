using System;
using MeleeAram.webapi.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeleeAram.webapi.DataContext;

public class MaContext : DbContext
{
    private string _connectionString;
    public MaContext(DbContextOptions<MaContext> options) : base(options)
    {
        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        _connectionString = configuration.GetValue<string>("ConnectionStrings:DefaultConnectionString")!;
        this.Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
        optionsBuilder.UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GameMode>()
            .HasKey(gm => gm.Id);

        modelBuilder.Entity<BannedChampion>()
            .HasKey(bm => bm.Id);

        modelBuilder.Entity<Champion>()
            .HasKey(c => c.Id);

        modelBuilder.Entity<OwnedChamps>()
            .HasKey(oc => oc.Id);

        modelBuilder.Entity<Player>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<GameMode>()
            .HasMany(gm => gm.BannedChampions)
            .WithOne(bc => bc.GameMode);

        modelBuilder.Entity<BannedChampion>()
            .HasOne(bc => bc.GameMode)
            .WithMany(gm => gm.BannedChampions);

        modelBuilder.Entity<Champion>()
            .HasMany(c => c.BannedChampions)
            .WithOne(bc => bc.Champion);

        modelBuilder.Entity<BannedChampion>()
            .HasOne(bc => bc.Champion)
            .WithMany(c => c.BannedChampions);

        modelBuilder.Entity<Champion>()
            .HasMany(c => c.OwnedChamps)
            .WithOne(oc => oc.Champion);

        modelBuilder.Entity<OwnedChamps>()
            .HasOne(oc => oc.Champion)
            .WithMany(c => c.OwnedChamps);

        modelBuilder.Entity<Player>()
            .HasMany(p => p.OwnedChamps)
            .WithOne(oc => oc.Player);

        modelBuilder.Entity<OwnedChamps>()
            .HasOne(oc => oc.Player)
            .WithMany(p => p.OwnedChamps);


    }
}



