using NUnit.Framework;
using System.Collections.Generic;
using System;

namespace QB.MatchThree.Matching.MatchTypes
{
    [TestFixture]
    public class ThreeMatchTests
    {
        [Test]
        public void ThreeMatchIsValidForColumOfIndexes()
        {
            List<Index2D> indexList = createListWithThreeIndexes();
            ThreeMatch match = new ThreeMatch();
            Assert.That(match.IsValidForMatchedIndexes(indexList), Is.True);
        }

        [Test]
        public void ThreeMatchIsNotValidForNonColumOfIndexes()
        {
            List<Index2D> indexList = createListWithThreeIndexes();
            indexList.RemoveAt(0);
            ThreeMatch match = new ThreeMatch();
            Assert.That(match.IsValidForMatchedIndexes(indexList), Is.False);
        }

        [Test]
        public void ThreeMatchReturnsFalseForEmptyLists()
        {
            ThreeMatch match = new ThreeMatch();
            Assert.That(match.IsValidForMatchedIndexes(new List<Index2D>()), Is.False);
        }

        [Test]
        public void ThreeMatchReturnsFalseForNullLists()
        {
            ThreeMatch match = new ThreeMatch();
            Assert.That(match.IsValidForMatchedIndexes(null), Is.False);
        }

        private static List<Index2D> createListWithThreeIndexes()
        {
            List<Index2D> indexes = new List<Index2D>();
            indexes.Add(new Index2D(0,0));
            for (int i = 1; i < 3; i++)
            {
                indexes.Add(indexes[i - 1].RightIndex);
            }
            return indexes;
        }
    }
}

