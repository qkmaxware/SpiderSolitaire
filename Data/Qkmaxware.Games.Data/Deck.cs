using System.Collections;
using System.Drawing;

namespace Qkmaxware.Games.Data;

public class Deck : List<PlayingCard> {
    public Deck() {}
    public Deck(params PlayingCard[] cards) : base(cards.Length) {
        this.AddRange(cards);
    }


    public static Deck Empty() => new Deck();

    public static Deck Standard() {
        var suits = new Suit[]{
            Spades.Instance,
            Clubs.Instance,
            Hearts.Instance,
            Diamonds.Instance
        };
        var cards = new PlayingCard[52];
        var index = 0;
        foreach (var suit in suits) {
            foreach (var rank in Enum.GetValues<CardRank>()) {
                var card = new PlayingCard(suit, rank);
                cards[index++] = card;
            }
        }
        Deck d = new Deck(cards);
        return d;
    }

    private static Random rng = new Random();
    public void Shuffle() {
        int n = this.Count;  
        while (n > 1) {  
            n--;  
            int k = rng.Next(n + 1);  
            var value = this[k];  
            this[k] = this[n];  
            this[n] = value;  
        }  
    }

    public PlayingCard? Take() {
        if (this.Count < 1)
            return null;

        var card = this[this.Count - 1];
        RemoveAt(this.Count - 1);
        return card;
    }
}