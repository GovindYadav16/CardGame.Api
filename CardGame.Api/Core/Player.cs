namespace CardGame.Api.Core
{
    public class Player
    {
        public string Name { get; }
        public List<Card> Hand { get; }

        public Player(string name)
        {
            Name = name;
            Hand = new List<Card>();
        }

        public void AddCard(Card card)
        {
            Hand.Add(card);
        }

        public Card PlayCard()
        {
            if (Hand.Count == 0)
                throw new InvalidOperationException("No cards left to play.");
            var card = Hand[0];
            Hand.RemoveAt(0);
            return card;
        }
    }
}
