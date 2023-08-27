using System.Drawing;

namespace Qkmaxware.Games.Data;

public abstract class Suit {
    public abstract string Name();
    public abstract string Symbol();
    public abstract Color Colour();
}

public class Spades : Suit {

    public static readonly Spades Instance = new Spades();
    private Spades() {}

    public override Color Colour() => Color.Black;

    public override string Name() => "Spades";

    public override string Symbol() => "♠";
}

public class Clubs : Suit {

    public static readonly Clubs Instance = new Clubs();
    private Clubs() {}

    public override Color Colour() => Color.Black;

    public override string Name() => "Clubs";

    public override string Symbol() => "♣";
}

public class Hearts : Suit {

    public static readonly Hearts Instance = new Hearts();
    private Hearts() {}

    public override Color Colour() => Color.Red;

    public override string Name() => "Hearts";

    public override string Symbol() => "♥";
}

public class Diamonds : Suit {

    public static readonly Diamonds Instance = new Diamonds();
    private Diamonds() {}

    public override Color Colour() => Color.Red;

    public override string Name() => "Diamonds";

    public override string Symbol() => "♦";
}