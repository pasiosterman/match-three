
using System.Collections.Generic;

namespace QB.MatchThree.Matching.MatchTypes
{
    public class RowMatch : IMatchType
    {
        readonly int boardWidth; 

        public RowMatch(int boardHeight){
            this.boardWidth = boardHeight;
        }

        public bool IsValidForMatchedIndexes(List<Index2D> matchedIndexes)
        {
            if(matchedIndexes == null || matchedIndexes.Count != boardWidth) return false;

            int y = matchedIndexes[0].y;
            for (int j = 1; j < matchedIndexes.Count; j++)
            {
                if (matchedIndexes[j].y != y){
                    return false;
                }
            }
            return true;
        }
    }
}

