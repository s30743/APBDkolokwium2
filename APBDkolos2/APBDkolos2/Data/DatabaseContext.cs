using APBDkolos2.Models;
using Microsoft.EntityFrameworkCore;

namespace APBDkolos2.Data;

public class DatabaseContext : DbContext
{
    public DbSet<Map> Maps { get; set; }
    public DbSet<Tournament> Tournaments { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<Match> Matches { get; set; }
    public DbSet<PlayerMatch> PlayerMatches { get; set; }
    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Map>().HasData(new List<Map>()
        {
            new Map { MapId = 1, Name = "Inferno", Type = "Competitive"},
            new Map { MapId = 2, Name = "Mirage", Type = "For Noobs"},
        });
        modelBuilder.Entity<Tournament>().HasData(new List<Tournament>()
        {
            new Tournament { TournamentId = 1, Name = "CS2 Summer Cup", StartDate = DateTime.Parse("2025-07-02T15:00:00"), EndDate = DateTime.Parse("2025-07-02T15:00:00")},
           
        });
        modelBuilder.Entity<Player>().HasData(new List<Player>()
        {
            new Player { PlayerId = 1, FirstName = "Alex", LastName = "Smith", BirthDate = DateTime.Parse("2000-05-20") }
        });
        modelBuilder.Entity<Match>().HasData(new List<Match>()
        {
            new Match { MatchId = 1, TournamentId = 1, MapId = 1, MatchDate = DateTime.Parse("2025-07-02T15:00:00"),Team1Score = 16,Team2Score = 12},
            new Match { MatchId = 2, TournamentId = 1, MapId = 2, MatchDate = DateTime.Parse("2025-07-02T15:00:00"),Team1Score = 10,Team2Score = 16},
        });
        modelBuilder.Entity<PlayerMatch>().HasData(new List<PlayerMatch>()
        {
            new PlayerMatch
                { MatchId = 1, PlayerId = 1, MVPs = 3, Rating = 1.25m},
            new PlayerMatch
                { MatchId = 2, PlayerId = 1, MVPs = 2, Rating = 1.10m},
        });

    }
}