
using System.Collections.Generic;

namespace QB.MatchThree.Matching.MatchTypes
{
    /// <summary>
    /// Matches with more than four matching tiles.
    /// </summary>
    public class LinkedMatch : IMatchType
    {
        public bool IsValidForMatchedIndexes(List<Index2D> matchedIndexes)
        {
            return matchedIndexes != null && matchedIndexes.Count > 4;
        }
    }
}

