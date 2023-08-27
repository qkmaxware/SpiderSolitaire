using Difficulty = System.Int32;

namespace Spider.Data;

public class StatisticsRow {
    public int GamesPlayed {get; set;}
    public int Victories  {get; set;}
    public int Losses() => GamesPlayed - Victories;

    public double FastestTimeSeconds {get; set;}
    public double SumOfTimeSeconds {get; set;}
    public double AverageTimeSeconds() => Victories > 0 ? SumOfTimeSeconds / Victories : 0;
}

public class Statistics {

    public Dictionary<Difficulty, StatisticsRow>? All {get; set;}

    public void AddVictory(Difficulty difficulty, TimeSpan elapsed) {
        if (ReferenceEquals(All, null))
            All = new Dictionary<Difficulty, StatisticsRow>();

        StatisticsRow? row;
        if (!All.TryGetValue(difficulty, out row)) {
            row = new StatisticsRow();
            row.FastestTimeSeconds = 0;
            row.SumOfTimeSeconds = 0;
            All[difficulty] = row;
        }
        
        var seconds = elapsed.TotalSeconds;
        if (row.GamesPlayed == 0 || seconds < row.FastestTimeSeconds) {
            row.FastestTimeSeconds = seconds;
        }

        row.GamesPlayed++;
        row.Victories++;
    }
    public void AddForfeit(Difficulty difficulty) {
        if (ReferenceEquals(All, null))
            All = new Dictionary<Difficulty, StatisticsRow>();

        StatisticsRow? row;
        if (!All.TryGetValue(difficulty, out row)) {
            row = new StatisticsRow();
            row.FastestTimeSeconds = 0;
            row.SumOfTimeSeconds = 0;
            All[difficulty] = row;
        }

        row.GamesPlayed++;
    }
}