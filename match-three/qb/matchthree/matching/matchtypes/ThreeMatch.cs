
using System.Collections.Generic;

namespace QB.MatchThree.Matching.MatchTypes
{
    public class ThreeMatch : IMatchType
    {
        public bool IsValidForMatchedIndexes(List<Index2D> matchedIndexes)
        {
            return matchedIndexes != null && matchedIndexes.Count == 3;
        }
    }
}

