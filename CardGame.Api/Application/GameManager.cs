using CardGame.Api.Core;

namespace CardGame.Api.Application
{
    public class GameManager
    {
        private readonly Deck _deck;
        private readonly Player _humanPlayer;
        private readonly Player _computerPlayer;

        public GameManager()
        {
            _deck = new Deck();
            _humanPlayer = new Player("Human");
            _computerPlayer = new Player("Computer");
            DealCards();
        }

        private void DealCards()
        {
            for (int i = 0; i < 26; i++) // Each player gets half the deck
            {
                _humanPlayer.AddCard(_deck.DrawCard());
                _computerPlayer.AddCard(_deck.DrawCard());
            }
        }

        public List<Card> GetPlayerHand() => _humanPlayer.Hand;

        public (Card HumanCard, Card ComputerCard, string Message) PlayTurn(string rank, string suit)
        {
            // Find the human's selected card in their hand
            var humanCard = _humanPlayer.Hand.FirstOrDefault(c => c.Rank == rank && c.Suit == suit);

            if (humanCard == null)
            {
                // If the card is not found in the hand, throw an exception
                throw new ArgumentException("The selected card is not in the player's hand.");
            }

            // Remove the card from the human player's hand
            bool removed = _humanPlayer.Hand.Remove(humanCard);
            if (!removed)
            {
                throw new InvalidOperationException("Failed to remove the selected card from the human player's hand.");
            }

            // Computer's card selection
            Card computerCard;

            // Try to find a card of the same suit with a higher rank
            var humanRankIndex = Array.IndexOf(Deck.Ranks, humanCard.Rank);
            computerCard = _computerPlayer.Hand
                .Where(c => c.Suit == humanCard.Suit && Array.IndexOf(Deck.Ranks, c.Rank) > humanRankIndex)
                .OrderBy(c => Array.IndexOf(Deck.Ranks, c.Rank)) // Play the lowest card that beats human
                .FirstOrDefault();

            if (computerCard == null)
            {
                // If no card can beat, play any card of the same suit
                computerCard = _computerPlayer.Hand
                    .Where(c => c.Suit == humanCard.Suit)
                    .OrderBy(c => Array.IndexOf(Deck.Ranks, c.Rank)) // Play the lowest card of the suit
                    .FirstOrDefault();
            }

            if (computerCard == null)
            {
                // If no card of the same suit, play any card
                computerCard = _computerPlayer.Hand
                    .OrderBy(c => Array.IndexOf(Deck.Ranks, c.Rank)) // Play the lowest card overall
                    .First();
            }

            // Remove the selected card from the computer's hand
            bool computerRemoved = _computerPlayer.Hand.Remove(computerCard);
            if (!computerRemoved)
            {
                throw new InvalidOperationException("Failed to remove the selected card from the computer's hand.");
            }

            // Compare cards and determine the winner
            string winnerMessage = CompareCards(humanCard, computerCard, humanCard.Suit);

            return (humanCard, computerCard, winnerMessage);
        }





        private string CompareCards(Card humanCard, Card computerCard, string leadingSuit)
        {
            // Check if the computer followed the leading suit
            bool computerFollowedSuit = computerCard.Suit == leadingSuit;

            if (computerFollowedSuit)
            {
                // Compare ranks if both cards are of the leading suit
                var humanRankIndex = Array.IndexOf(Deck.Ranks, humanCard.Rank);
                var computerRankIndex = Array.IndexOf(Deck.Ranks, computerCard.Rank);

                if (humanRankIndex > computerRankIndex)
                    return "Human wins this round!";
                else if (computerRankIndex > humanRankIndex)
                    return "Computer wins this round!";
                else
                    return "It's a tie!";
            }
            else
            {
                // Computer didn't follow the suit, human wins
                return "Human wins this round!";
            }
        }

    }
}
