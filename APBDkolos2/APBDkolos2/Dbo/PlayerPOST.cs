using System.ComponentModel.DataAnnotations;

namespace APBDkolos2.Dbo;

public class PlayerPOST
{
    [Required]
    public string firstName { get; set; }
    [Required]
    public string lastName { get; set; }
    [Required]
    public DateTime birthDate { get; set; }
    [Required]
    public ICollection<MatchesDetailsPOST> matches { get; set; }
    
}

public class MatchesDetailsPOST
{
    [Required]
    public int matchId { get; set; }
    [Required]
    public int MVPs { get; set; }
    [Required]
    public decimal rating { get; set; }
}