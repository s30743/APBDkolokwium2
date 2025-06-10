using APBDkolos2.Dbo;

namespace APBDkolos2.Services;

public interface IplayerService
{
    Task<PlayerGET> GetPlayerInfo(int playerId);
    Task addNewPlayer(PlayerPOST playerPOST);
}