using System.Collections;
using System.Collections.Generic;

public class ScoreComparer : Comparer<PlayerScore>
{
    public override int Compare(PlayerScore x, PlayerScore y)
    {
        return y.Score.CompareTo(x.Score);
    }
}

