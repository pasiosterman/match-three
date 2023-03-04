
using System.Collections.Generic;

namespace QB.MatchThree.Matching.MatchTypes
{
    public class CrossMatch : IMatchType
    {
        public bool IsValidForMatchedIndexes(List<Index2D> matchedIndexes)
        {
            if (matchedIndexes == null || matchedIndexes.Count != 5) return false;

            Index2D leftMostIndex = findLeftMostIndex(matchedIndexes);
            Index2D[] indexesForCrossShape = createCrossShapeArrayOfIndexes(leftMostIndex.RightIndex);

            bool hasAllCrossIndexes = true;
            for (int i = 0; i < indexesForCrossShape.Length; i++)
            {
                if (!matchedIndexes.Contains(indexesForCrossShape[i]))
                {
                    hasAllCrossIndexes = false;
                    break;
                }
            }
            return hasAllCrossIndexes;
        }

        private List<Index2D> createMatchIndexListFromMatchedTiles(List<PuzzleBoardTile> matchedTiles)
        {
            List<Index2D> matchIndexes = new List<Index2D>();
            for (int i = 0; i < matchedTiles.Count; i++)
                matchIndexes.Add(matchedTiles[i].Index);

            return matchIndexes;
        }

        private Index2D findLeftMostIndex(List<Index2D> matchIndexes)
        {
            Index2D leftMostIndex = matchIndexes[0];
            for (int i = 0; i < matchIndexes.Count; i++)
            {
                if (leftMostIndex.x > matchIndexes[i].x)
                    leftMostIndex = matchIndexes[i];
            }
            return leftMostIndex;
        }

        private Index2D[] createCrossShapeArrayOfIndexes(Index2D crossCenter)
        {
            Index2D[] indexesForCrossShape = new Index2D[]
            {
                    crossCenter,
                    crossCenter.RightIndex,
                    crossCenter.UpIndex,
                    crossCenter.DownIndex
            };
            return indexesForCrossShape;
        }
    }
}

