using System.ComponentModel.DataAnnotations;

namespace APBDkolos2.Models;

public class Map
{
    [Key] public int MapId { get; set; }
    [MaxLength(100)] public string Name { get; set; }
    [MaxLength(100)] public string Type { get; set; }
    public ICollection<Match> Matches { get; set; }
}