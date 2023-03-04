using System.Collections.Generic;

namespace QB.MatchThree.Matching
{
    public interface IMatcher
    {
        List<PuzzleTileMatch> CheckForMatches(PuzzleTileGrid puzzleTileGrid);
    }
}