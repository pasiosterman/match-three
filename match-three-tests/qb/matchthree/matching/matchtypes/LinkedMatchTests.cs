using NUnit.Framework;
using System.Collections.Generic;

namespace QB.MatchThree.Matching.MatchTypes
{
    [TestFixture]
    public class LinkedMatchTests
    {
        const int TEST_COLUMN_HEIGHT = 5;
        LinkedMatch match = new LinkedMatch();

        [Test]
        public void LinkedMatchIsValidForLShapedMatch()
        {
            List<Index2D> indexList = createLshapedMatch();   
            Assert.That(match.IsValidForMatchedIndexes(indexList), Is.True);
        }

        [Test]
        public void LinkedMatchIsNotValidForLessThanFiveMatches()
        {
            List<Index2D> indexList = createRowshapedMatch(3);
            Assert.That(match.IsValidForMatchedIndexes(indexList), Is.False);

            indexList = createRowshapedMatch(4);
            Assert.That(match.IsValidForMatchedIndexes(indexList), Is.False);
        }

        [Test]
        public void LinkedMatchReturnsFalseForEmptyLists()
        {
            Assert.That(match.IsValidForMatchedIndexes(new List<Index2D>()), Is.False);
        }

        [Test]
        public void LinkedMatchReturnsFalseForNullLists()
        {
            Assert.That(match.IsValidForMatchedIndexes(null), Is.False);
        }

        private static List<Index2D> createLshapedMatch()
        {
            List<Index2D> indexes = new List<Index2D>();
            indexes.Add(new Index2D(0,0));
            indexes.Add(indexes[0].RightIndex);
            indexes.Add(indexes[1].RightIndex);
            indexes.Add(indexes[0].UpIndex);
            indexes.Add(indexes[3].UpIndex);
            return indexes;
        }

        private static List<Index2D> createRowshapedMatch(int size)
        {
            List<Index2D> indexes = new List<Index2D>();
            indexes.Add(new Index2D(0,0));
            for (int i = 1; i < size; i++)
            {
                indexes.Add(indexes[i - 1].RightIndex);
            }
            return indexes;
        }
    }
}

