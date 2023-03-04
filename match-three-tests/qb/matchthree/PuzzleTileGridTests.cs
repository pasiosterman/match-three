using NUnit.Framework;

namespace QB.MatchThree
{
    [TestFixture]
    public class PuzzleTileGridTests
    {
        [Test]
        public void createsPuzzleGridWithCorrectAmountOfRows()
        {
            PuzzleTileGrid grid = new PuzzleTileGrid(6, 5);
            Assert.AreEqual(grid.Rows, 5);
        }

        [Test]
        public void createsPuzzleGridWithCorrectAmountOfColumns()
        {
            PuzzleTileGrid grid = new PuzzleTileGrid(6, 5);
            Assert.AreEqual(grid.Columns, 6);
        }

        [Test]
        public void createsPuzzleGridResultsInCorrectAmountOfTiles()
        {
            PuzzleTileGrid grid = new PuzzleTileGrid(6, 5);
            Assert.AreEqual(30, grid.AllTiles.Length);
        }

        [Test]
        public void createCopyMethodCreatesNewInstancesOfGridAndTiles()
        {
            PuzzleTileGrid grid = new PuzzleTileGrid(6, 5);
            PuzzleTileGrid gridCopy = grid.CreateCopy();

            Assert.AreNotSame(grid, gridCopy);

            PuzzleBoardTile[] allTiles = grid.AllTiles;
            PuzzleBoardTile[] allCopiedTiles = grid.CreateCopy().AllTiles;

            for (int i = 0; i < allTiles.Length; i++)
            {
                for (int j = 0; j < allCopiedTiles.Length; j++)
                {
                    Assert.AreNotSame(allTiles[i], allCopiedTiles[j]);
                }
            }
        }
    }
}

