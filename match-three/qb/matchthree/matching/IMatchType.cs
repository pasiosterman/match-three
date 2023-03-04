using System.Collections.Generic;

namespace QB.MatchThree.Matching
{
    /// <summary>
    /// Interface for checking types of matches e.g three of same, row, column, etc
    /// </summary>
    public interface IMatchType
    {
        bool IsValidForMatchedIndexes(List<Index2D> matchedIndexes);
    }
}