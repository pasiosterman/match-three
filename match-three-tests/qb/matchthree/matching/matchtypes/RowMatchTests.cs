using NUnit.Framework;
using System.Collections.Generic;
using System;

namespace QB.MatchThree.Matching.MatchTypes
{
    [TestFixture]
    public class RowMatchTests
    {
        const int TEST_ROW_WITH = 6;
        IMatchType match = new RowMatch(TEST_ROW_WITH);

        [Test]
        public void RowMatchIsValidForColumOfIndexes()
        {
            List<Index2D> indexList = createRowFromIndex(TEST_ROW_WITH);
            Assert.That(match.IsValidForMatchedIndexes(indexList), Is.True);
        }

        [Test]
        public void RowMatchIsNotValidForNonColumOfIndexes()
        {
            List<Index2D> indexList = createRowFromIndex(TEST_ROW_WITH);
            indexList[0] = new Index2D(0, 1);
            Assert.That(match.IsValidForMatchedIndexes(indexList), Is.False);
        }

        [Test]
        public void RowMatchReturnsFalseForEmptyLists()
        {
            Assert.That(match.IsValidForMatchedIndexes(new List<Index2D>()), Is.False);
        }

        [Test]
        public void RowMatchReturnsFalseForNullLists()
        {
            Assert.That(match.IsValidForMatchedIndexes(null), Is.False);
        }

        private static List<Index2D> createRowFromIndex(int height)
        {
            List<Index2D> indexes = new List<Index2D>();
            indexes.Add(new Index2D(0,0));
            for (int i = 1; i < height; i++)
            {
                indexes.Add(indexes[i - 1].RightIndex);
            }
            return indexes;
        }
    }
}

