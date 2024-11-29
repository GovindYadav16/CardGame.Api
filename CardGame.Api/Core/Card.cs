namespace CardGame.Api.Core
{
    public class Card
    {
        public string Rank { get; }
        public string Suit { get; }

        public Card(string rank, string suit)
        {
            Rank = rank;
            Suit = suit;
        }

        public override string ToString() => $"{Rank} of {Suit}";
    }
}
