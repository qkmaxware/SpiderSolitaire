using Qkmaxware.Games.Data;

namespace Spider.Data;

public interface ITheme {
    public string GetImageUrl(FaceCard card);
    public string GetFrontImageUrl(PlayingCard card);
    public string GetBackImageUrl();
}

public class Red2Theme : ITheme {
    public string GetImageUrl(FaceCard card) {
        return card.Facing == Facing.FaceUp ? GetFrontImageUrl(card.Card) : GetBackImageUrl() ;
    }

    public string GetFrontImageUrl(PlayingCard card) {
        return "images/Cards/Front/card" + (card.Suit?.Name() ?? string.Empty) + (int)card.Rank + ".png";
    }

    public string GetBackImageUrl() {
        return "images/Cards/Back/cardBack_red2.png";
    }
}