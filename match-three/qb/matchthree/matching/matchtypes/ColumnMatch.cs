
using System.Collections.Generic;

namespace QB.MatchThree.Matching.MatchTypes
{
    public class ColumnMatch : IMatchType
    {
        readonly int boardHeight; 

        public ColumnMatch(int boardHeight){
            this.boardHeight = boardHeight;
        }

        public bool IsValidForMatchedIndexes(List<Index2D> matchedIndexes)
        {
            if(matchedIndexes == null || matchedIndexes.Count != boardHeight) return false;

            int x = matchedIndexes[0].x;
            for (int j = 1; j < matchedIndexes.Count; j++)
            {
                if (matchedIndexes[j].x != x){
                    return false;
                }
            }
            return true;
        }
    }
}

