using Qkmaxware.Games.Data;
using Spider.Data;

namespace Spider.Data;

public enum Facing {
    FaceUp,
    FaceDown,
}
public class FaceCard {
    public PlayingCard Card {get; private set;}
    public Facing Facing {get; private set;}

    public FaceCard(FaceCard other) {
        this.Facing = other.Facing;
        this.Card = other.Card;
    }
    public FaceCard(Facing facing, PlayingCard card) {
        this.Card = card;
        this.Facing = facing;
    }

    public void Expose() {
        this.Facing = Facing.FaceUp;
    }
}
public class Pile {
    private List<FaceCard> cards = new List<FaceCard>();
    public void Add(FaceCard card) {
        this.cards.Add(card);
    }
    public void AddFaceUp(PlayingCard card) {
        this.cards.Add(new FaceCard(Facing.FaceUp, card));
    }
    public void AddFaceDown(PlayingCard card) {
        this.cards.Add(new FaceCard(Facing.FaceDown, card));
    }

    public int IndexOf(FaceCard card) {
        return this.cards.IndexOf(card);
    }

    public void RemoveAll(IEnumerable<FaceCard> cards) {
        foreach (var card in cards) {
            this.cards.Remove(card);
        }
    }

    public int CardCount => cards.Count;
    public FaceCard this[int index] => cards[index];
    public FaceCard? Top => cards.Count > 0 ? cards[cards.Count - 1] : null;
    public FaceCard? Pop() {
        var card = Top;
        if (cards.Count > 0)
            cards.RemoveAt(cards.Count - 1);
        return card;
    }
}

public class SpiderSolitaire {
    public List<PlayingCard> AllCards {get; private set;}
    public Pile[] Piles = new Pile[10];
    public Deck Stack = new Deck();
    private Pile?[] scorePiles = new Pile?[8];

    public SpiderSolitaire(SpiderSolitaire other) {
        // Clone all cards
        this.AllCards = new List<PlayingCard>(other.AllCards);

        // Clone piles
        for (var i = 0; i < Piles.Length; i++) {
            var pile = new Pile();
            var pile2Clone = other.Piles[i];
            for (var j = 0; j < pile2Clone.CardCount; j++) {
                var card2Clone = pile2Clone[j];
                pile.Add(new FaceCard(card2Clone));
            }
            Piles[i] = pile;
        }

        // Clone stack
        this.Stack.AddRange(other.Stack);

        // Clone score piles
        for (var i = 0; i < scorePiles.Length; i++) {
            var pile2Clone = other.scorePiles[i];
            if (ReferenceEquals(pile2Clone, null)) {
                scorePiles[i] = null;
            } else {
                var pile = new Pile();
                for (var j = 0; j < pile2Clone.CardCount; j++) {
                    var card2Clone = pile2Clone[j];
                    pile.Add(new FaceCard(card2Clone));
                }
                scorePiles[i] = pile;
            }
        }
    }
    public SpiderSolitaire() {
        // 2 Standard decks
        var cards = Deck.Standard();
        cards.AddRange(Deck.Standard());
        cards.Shuffle();

        var cards_remaining = new Queue<PlayingCard>(cards);
        var all_cards = new List<PlayingCard>(cards);
        this.AllCards = all_cards;

        // Configure "piles"
        for (var i = 0; i < Piles.Length; i++) {
            Piles[i] = new Pile();
        }
        for (var i = 0; i < Piles.Length; i++) {
            var count = i < 4 ? 5 : 4;
            for (var j = 0; j < count; j++) {
                var card = cards_remaining.Dequeue();
                if (!ReferenceEquals(card, null))
                    Piles[i].AddFaceDown(card);
            }
            {
                var card = cards_remaining.Dequeue();
                if (!ReferenceEquals(card, null))
                    Piles[i].AddFaceUp(card);
            }
        }

        // Configure stack
        Stack.AddRange(cards_remaining); // add remaining cards
    }
    public void AddToScore(List<FaceCard> cards) {
        for (var i = 0; i < scorePiles.Length; i++) {
            if (ReferenceEquals(scorePiles[i], null)) {
                Pile p = new Pile();
                cards.Reverse();
                foreach (var card in cards) {
                    p.AddFaceUp(card.Card);
                }
                scorePiles[i] = p;
            }
        }
    }
    public Pile? GetScorePile(int i) {
        if (i >= 0 && i < this.scorePiles.Length) {
            return this.scorePiles[i];
        } else {
            return null;
        }
    }

    public bool IsGameOver() {
        foreach (var pile in this.Piles) {
            if (pile.CardCount > 0)
                return false;
        }
        foreach (var score in this.scorePiles) {
            if (ReferenceEquals(score, null) || score.CardCount <= 0) {
                return false;
            }
        }
        return true;
    }
}