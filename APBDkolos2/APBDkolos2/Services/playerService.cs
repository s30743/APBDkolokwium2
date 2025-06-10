using APBDkolos2.Data;
using APBDkolos2.Dbo;
using APBDkolos2.Exceptions;
using APBDkolos2.Models;
using Microsoft.EntityFrameworkCore;

namespace APBDkolos2.Services;

public class playerService : IplayerService
{
    private readonly DatabaseContext _context;

    public playerService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<PlayerGET> GetPlayerInfo(int playerId)
    {
        var res = await _context.Players.Where(p => p.PlayerId == playerId).Select(p => new PlayerGET
        {
            PlayerId = p.PlayerId,
            FirstName = p.FirstName,
            LastName = p.LastName,
            BirthDate = p.BirthDate,
            matches = p.PlayerMatches.Select(pm => new MatchDetailsGET
            {
                Tournament = pm.Match.Tournament.Name,
                Map = pm.Match.Map.Name,
                Date = pm.Match.MatchDate,
                MVPs = pm.MVPs,
                Rating = pm.Rating,
                Team1Score = pm.Match.Team1Score,
                Team2Score = pm.Match.Team2Score
            }).ToList()
        }).FirstOrDefaultAsync();
        

        if (res == null)
        {
            throw new NotFoundEx($"Gracz o Id == {playerId} nie istnieje");
            
        }
        return res;
    }
    
    

    public async Task addNewPlayer(PlayerPOST playerPOST)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // ogolem mozna sprawdzic czy ten gracz juz istnieje, nie sprawdzalem bo nie ma o to prosby w poleceniu

            var newPlayer = new Player
            {
                FirstName = playerPOST.firstName,
                LastName = playerPOST.lastName,
                BirthDate = playerPOST.birthDate,
            };
            await _context.Players.AddAsync(newPlayer);
            
            await _context.SaveChangesAsync();

            foreach (var match in playerPOST.matches)
            {
                var MatchCheck = await _context.Matches.
                    FirstOrDefaultAsync(m => m.MatchId == match.matchId);
                
                if (MatchCheck == null)
                {
                    throw new NotFoundEx($"Mecz o podanym ID == {match.matchId} nie istnieje");
                }
                
                
                if (MatchCheck.BestRating < match.rating)
                {
                    MatchCheck.BestRating = match.rating; 
                    _context.Matches.Update(MatchCheck); 
                }

                var newPlayerMatch = new PlayerMatch
                {
                    MatchId = match.matchId,
                    PlayerId = newPlayer.PlayerId,
                    MVPs = match.MVPs,
                    Rating = match.rating,
                };
                await _context.PlayerMatches.AddAsync(newPlayerMatch);
            }
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}