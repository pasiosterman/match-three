using NUnit.Framework;
using System.Collections.Generic;

namespace QB.MatchThree.Matching.MatchTypes
{
    [TestFixture]
    public class FourMatchTests
    {
        IMatchType match = new FourMatch();
            
        [Test]
        public void FourMatchIsValidForColumOfIndexes()
        {
            List<Index2D> indexList = createListWithThreeIndexes();
            Assert.That(match.IsValidForMatchedIndexes(indexList), Is.True);
        }

        [Test]
        public void FourMatchIsNotValidForNonColumOfIndexes()
        {
            List<Index2D> indexList = createListWithThreeIndexes();
            indexList.RemoveAt(0);
            Assert.That(match.IsValidForMatchedIndexes(indexList), Is.False);
        }

        [Test]
        public void FourMatchReturnsFalseForEmptyLists()
        {
            Assert.That(match.IsValidForMatchedIndexes(new List<Index2D>()), Is.False);
        }

        [Test]
        public void FourMatchReturnsFalseForNullLists()
        {
            Assert.That(match.IsValidForMatchedIndexes(null), Is.False);
        }

        private static List<Index2D> createListWithThreeIndexes()
        {
            List<Index2D> indexes = new List<Index2D>();
            indexes.Add(new Index2D(0,0));
            for (int i = 1; i < 4; i++)
            {
                indexes.Add(indexes[i - 1].RightIndex);
            }
            return indexes;
        }
    }
}

