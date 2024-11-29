using CardGame.Api.Application;
using CardGame.Api.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CardGame.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly GameManager _gameManager;

        public GameController()
        {
            _gameManager = new GameManager();
        }

        [HttpGet("start")]
        public IActionResult StartGame()
        {
            return Ok("Game started! Cards have been dealt.");
        }

        [HttpGet("hand")]
        public IActionResult GetPlayerHand()
        {
            var hand = _gameManager.GetPlayerHand();
            return Ok(hand);
        }

        [HttpPost("play-turn")]
        public IActionResult PlayTurn([FromBody] CardInputModel input)
        {
            try
            {
                var result = _gameManager.PlayTurn(input.Rank, input.Suit);

                return Ok(new
                {
                    HumanCard = result.HumanCard.ToString(),
                    ComputerCard = result.ComputerCard.ToString(),
                    Message = result.Message
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }

}
