using NUnit.Framework;
using System.Collections.Generic;
using System;

namespace QB.MatchThree.Matching.MatchTypes
{
    [TestFixture]
    public class ColumnMatchTests
    {
        const int TEST_COLUMN_HEIGHT = 5;
        IMatchType match = new ColumnMatch(TEST_COLUMN_HEIGHT);

        [Test]
        public void ColumnMatchIsValidForColumOfIndexes()
        {
            List<Index2D> indexList = createColumFromIndex(TEST_COLUMN_HEIGHT);
            Assert.That(match.IsValidForMatchedIndexes(indexList), Is.True);
        }

        [Test]
        public void ColumnMatchIsNotValidForNonColumOfIndexes()
        {
            List<Index2D> indexList = createColumFromIndex(TEST_COLUMN_HEIGHT);
            indexList[0] = new Index2D(1, 0);
            Assert.That(match.IsValidForMatchedIndexes(indexList), Is.False);
        }

        [Test]
        public void ColumnMatchReturnsFalseForEmptyLists()
        {
            Assert.That(match.IsValidForMatchedIndexes(new List<Index2D>()), Is.False);
        }

        [Test]
        public void ColumnMatchReturnsFalseForNullLists()
        {
            Assert.That(match.IsValidForMatchedIndexes(null), Is.False);
        }

        private static List<Index2D> createColumFromIndex(int height)
        {
            List<Index2D> indexes = new List<Index2D>();
            indexes.Add(new Index2D(0,0));
            for (int i = 1; i < height; i++)
            {
                indexes.Add(indexes[i - 1].UpIndex);
            }
            return indexes;
        }
    }
}

