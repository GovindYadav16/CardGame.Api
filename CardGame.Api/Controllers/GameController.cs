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

        // Constructor initializes the GameManager
        public GameController()
        {
            _gameManager = new GameManager();
        }

        /// <summary>
        /// Starts the game by initializing the game state and dealing the cards.
        /// </summary>
        /// <returns>Returns a success message: "Game started! Cards have been dealt."</returns>
        [HttpGet("start")]
        public IActionResult StartGame()
        {
            return Ok("Game started! Cards have been dealt.");
        }

        /// <summary>
        /// Retrieves the current hand of the human player.
        /// This includes the cards assigned to the human player at the start of the game.
        /// </summary>
        /// <returns>Returns a list of cards assigned to the human player's hand.</returns>
        [HttpGet("hand")]
        public IActionResult GetPlayerHand()
        {
            var hand = _gameManager.GetPlayerHand();
            return Ok(hand);
        }

        /// <summary>
        /// With this endpoint we can play the cards.And you can enter the card details
        /// </summary>
        /// <param name="input">The card's rank and suit that the human player wishes to play.</param>
        /// <returns> HumanCard, ComputerCard, Result Message</returns>
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
