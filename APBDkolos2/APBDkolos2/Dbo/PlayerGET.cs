namespace APBDkolos2.Dbo;

public class PlayerGET
{
    public int PlayerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public ICollection<MatchDetailsGET> matches { get; set; }
}

public class MatchDetailsGET
{
    public string Tournament { get; set; }
    public string Map { get; set; }
    public DateTime Date { get; set; }
    public int MVPs { get; set; }
    public decimal Rating { get; set; }
    public int Team1Score { get; set; }
    public int Team2Score { get; set; }
}