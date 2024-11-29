namespace CardGame.Api.Core
{
    public class Deck
    {
        public static readonly string[] Ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };
        public static readonly string[] Suits = { "Hearts", "Diamonds", "Clubs", "Spades" };

        private List<Card> _cards;

        public Deck()
        {
            _cards = new List<Card>();
            foreach (var suit in Suits)
            {
                foreach (var rank in Ranks)
                {
                    _cards.Add(new Card(rank, suit));
                }
            }

            Shuffle();
        }

        public Card DrawCard()
        {
            if (_cards.Count == 0)
                throw new InvalidOperationException("The deck is empty.");

            var card = _cards.Last();
            _cards.Remove(card);
            return card;
        }

        private void Shuffle()
        {
            var random = new Random();
            _cards = _cards.OrderBy(_ => random.Next()).ToList();
        }
    }
}
