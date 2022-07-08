using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]

public class ScoreComparer : Comparer<PlayerScore>
{
    public override int Compare(PlayerScore x, PlayerScore y)
    {
        return y.Score.CompareTo(x.Score);
    }
}

