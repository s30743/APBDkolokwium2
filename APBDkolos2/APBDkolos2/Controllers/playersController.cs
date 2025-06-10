using APBDkolos2.Dbo;
using APBDkolos2.Exceptions;
using APBDkolos2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APBDkolos2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class playersController : ControllerBase
    {
        
        private readonly IplayerService _playerService;

        public playersController(IplayerService playerService)
        {
            _playerService = playerService;
        }


        [HttpGet("{id}/matches")]
        public async Task<IActionResult> GetMatchesById(int id)
        {
            try
            {
                var res = await _playerService.GetPlayerInfo(id);
                return Ok(res);
            }
            catch (NotFoundEx e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddNewPlayer([FromBody] PlayerPOST playerPOST)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _playerService.addNewPlayer(playerPOST);
            }
            catch (NotFoundEx e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Created("", playerPOST);
        }
    }
}
