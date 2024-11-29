using CardGame.Api.Application;
using CardGame.Api.Core;

namespace CardGame.Api.Test
{
    public class GameManagerTests
    {
        [Fact]
        public void GameManager_ShouldDealCardsCorrectly()
        {
            // Arrange
            var gameManager = new GameManager();

            // Act
            var humanHand = gameManager.GetPlayerHand();

            // Assert
            Assert.Equal(26, humanHand.Count); // Human should have 26 cards
        }

        [Fact]
        public void PlayTurn_ShouldThrowException_IfCardNotInHand()
        {
            // Arrange
            var gameManager = new GameManager();

            // Invalid card selection (card not in hand)
            string invalidRank = "Abc";
            string invalidSuit = "Suit";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => gameManager.PlayTurn(invalidRank, invalidSuit));
        }

        [Fact]
        public void PlayTurn_ShouldReturnValidResult_WhenHumanPlaysValidCard()
        {
            // Arrange
            var gameManager = new GameManager();
            var humanHand = gameManager.GetPlayerHand();
            var firstCard = humanHand.First();

            // Act
            var result = gameManager.PlayTurn(firstCard.Rank, firstCard.Suit);

            // Assert
            Assert.Equal(firstCard.Rank, result.HumanCard.Rank);
            Assert.Equal(firstCard.Suit, result.HumanCard.Suit);
            Assert.Contains("wins", result.Message); // Winner message should be returned
        }

        [Fact]
        public void PlayTurn_ShouldDetermineWinner_Correctly()
        {
            // Arrange
            var gameManager = new GameManager();
            var humanHand = gameManager.GetPlayerHand();
            var firstCard = humanHand.First();

            // Mock scenario: Human plays a card, and we check if the logic determines the winner
            string rank = firstCard.Rank;
            string suit = firstCard.Suit;

            // Act
            var result = gameManager.PlayTurn(rank, suit);

            // Assert
            Assert.NotNull(result.HumanCard);
            Assert.NotNull(result.ComputerCard);
            Assert.Contains("wins", result.Message); // Ensure winner is determined
        }
    }
}