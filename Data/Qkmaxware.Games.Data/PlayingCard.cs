using System.Drawing;

namespace Qkmaxware.Games.Data;

public class PlayingCard {
    public Suit Suit {get; private set;}
    public CardRank Rank {get; private set;}
    public PlayingCard(Suit suit, CardRank rank) {
        this.Suit = suit;
        this.Rank = rank;
    }

    public void ChangeSuit(Suit suit) {
        this.Suit = suit;
    }
    public void ChangeRank(CardRank rank) {
        this.Rank = rank;
    }

    public override string ToString() {
        return $"{Rank} of {Suit.Symbol()}'s";
    }
}