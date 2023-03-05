using System;
using System.Collections.Generic;
using NUnit.Framework;
using QB.MatchThree.Matching.MatchTypes;

namespace QB.MatchThree.Matching.Matchers
{
    [TestFixture]
    public class DefaultTileMatcherTests
    {

        [Test]
        public void TileGridWithFiveRowMatchesReturnsCorrectResult()
        {
            int[,] testBoard = new int[,]{
                { 1, 1, 1, 1, 1, 1 },
                { 2, 2, 2, 2, 2, 2 },
                { 3, 3, 3, 3, 3, 3 },
                { 4, 4, 4, 4, 4, 4 },
                { 5, 5, 5, 5, 5, 5 }
            };

            PuzzleTileGrid puzzleTileGrid = PuzzleTileGrid.BuildFrom2DIntegerArray(testBoard);
            DefaultTileMatcher defaultTileMatcher = new DefaultTileMatcher(puzzleTileGrid);
            List<PuzzleTileMatch> matches = defaultTileMatcher.CheckBoardForMatches();

            Assert.That(matches, Is.Not.Empty);
            Assert.That(matches, Has.Exactly(5).Items);
            
            for (int i = 0; i < matches.Count; i++)
            {
                Assert.That(matches[i].MatchType, Is.TypeOf<RowMatch>());
                Assert.That(matches[i].TileCount, Is.EqualTo(6));
            }
        }

        [Test]
        public void TileGridWithTenThreeMatchesReturnsCorrectResult()
        {
            int[,] testBoard = new int[,]{
                { 1, 1, 1, 2, 2, 2 },
                { 2, 2, 2, 1, 1, 1 },
                { 1, 1, 1, 2, 2, 2 },
                { 2, 2, 2, 1, 1, 1 },
                { 1, 1, 1, 2, 2, 2 }
            };

            PuzzleTileGrid puzzleTileGrid = PuzzleTileGrid.BuildFrom2DIntegerArray(testBoard);
            DefaultTileMatcher defaultTileMatcher = new DefaultTileMatcher(puzzleTileGrid);
            List<PuzzleTileMatch> matches = defaultTileMatcher.CheckBoardForMatches();

            Assert.That(matches, Is.Not.Empty);
            Assert.That(matches, Has.Exactly(10).Items);
            
            for (int i = 0; i < matches.Count; i++)
            {
                Assert.That(matches[i].MatchType, Is.TypeOf<ThreeMatch>());
                Assert.That(matches[i].TileCount, Is.EqualTo(3));
            }
        }

        [Test]
        public void TileGridWithTwoCrossMatchesReturnsCorrectResult()
        {
            int[,] testBoard = new int[,]{
                { 1, 2, 1, 2, 4, 2 },
                { 2, 2, 2, 1, 3, 1 },
                { 1, 2, 1, 1, 2, 1 },
                { 3, 4, 3, 2, 2, 2 },
                { 1, 3, 1, 1, 2, 1 }
            };

            PuzzleTileGrid puzzleTileGrid = PuzzleTileGrid.BuildFrom2DIntegerArray(testBoard);
            DefaultTileMatcher defaultTileMatcher = new DefaultTileMatcher(puzzleTileGrid);
            List<PuzzleTileMatch> matches = defaultTileMatcher.CheckBoardForMatches();

            Assert.That(matches, Is.Not.Empty);
            Assert.That(matches, Has.Exactly(2).Items);
            
            for (int i = 0; i < matches.Count; i++)
            {
                Assert.That(matches[i].MatchType, Is.TypeOf<CrossMatch>());
                Assert.That(matches[i].TileCount, Is.EqualTo(5));
            }
        }

        [Test]
        public void TraverseTileMatchOfThreeCorrectly()
        {
            int[,] testBoard = new int[,]{
                { 2, 2, 2, 2, 2, 2 },
                { 2, 2, 2, 2, 2, 2 },
                { 2, 2, 2, 2, 2, 2 },
                { 2, 2, 2, 2, 2, 2 },
                { 1, 1, 1, 2, 2, 2 }
            };

            PuzzleTileGrid puzzleTileGrid = PuzzleTileGrid.BuildFrom2DIntegerArray(testBoard);
            PuzzleBoardTile bottomLeftTile = puzzleTileGrid[4, 0];
            DefaultTileMatcher defaultTileMatcher = new DefaultTileMatcher(puzzleTileGrid);

            MatchGroup matchGroup = new MatchGroup(new HashSet<PuzzleBoardTile>());
            defaultTileMatcher.TraverseTile(bottomLeftTile, matchGroup);

            Assert.That(matchGroup.Traversed, Has.Exactly(3).Items);
            Assert.That(matchGroup.Tiles, Has.Exactly(1).Items);
        }

        [Test]
        public void TraverseTileCorrectlyHandlesLoops()
        {
            int[,] testBoard = new int[,]{
                { 2, 2, 2, 2, 2, 2 },
                { 2, 2, 2, 2, 2, 2 },
                { 1, 1, 1, 2, 2, 2 },
                { 1, 2, 1, 2, 2, 2 },
                { 1, 1, 1, 2, 2, 2 }
            };

            PuzzleTileGrid puzzleTileGrid = PuzzleTileGrid.BuildFrom2DIntegerArray(testBoard);
            PuzzleBoardTile bottomLeftTile = puzzleTileGrid[4, 0];
            DefaultTileMatcher defaultTileMatcher = new DefaultTileMatcher(puzzleTileGrid);

            MatchGroup matchGroup = new MatchGroup(new HashSet<PuzzleBoardTile>());
            defaultTileMatcher.TraverseTile(bottomLeftTile, matchGroup);
            Assert.That(matchGroup.Traversed, Has.Exactly(8).Items);
            Assert.That(matchGroup.Tiles, Has.Exactly(4).Items);

            for (int i = 0; i < matchGroup.Tiles.Count; i++)
            {
                Assert.That(matchGroup.Tiles[i], Has.Exactly(3).Items);
            }
        }

        [Test]
        public void TraverseTileCorrectlyTraversesRows()
        {
            int[,] testBoard = new int[,]{
                { 1, 1, 1, 1, 1, 1 },
                { 2, 2, 2, 2, 2, 2 },
                { 3, 3, 3, 3, 3, 2 },
                { 4, 4, 4, 4, 4, 2 },
                { 5, 5, 5, 5, 5, 5 }
            };

            PuzzleTileGrid puzzleTileGrid = PuzzleTileGrid.BuildFrom2DIntegerArray(testBoard);
            PuzzleBoardTile topLeftTile = puzzleTileGrid[0, 0];
            DefaultTileMatcher defaultTileMatcher = new DefaultTileMatcher(puzzleTileGrid);

            MatchGroup matchGroup = new MatchGroup(new HashSet<PuzzleBoardTile>());
            defaultTileMatcher.TraverseTile(topLeftTile, matchGroup);
            Assert.That(matchGroup.Traversed, Has.Exactly(6).Items);
            Assert.That(matchGroup.Tiles, Has.Exactly(1).Items);
        }

        [Test]
        public void TraverseTileCorrectlyTraversesColumns()
        {
            int[,] testBoard = new int[,]{
                { 1, 2, 2, 2, 2, 2 },
                { 1, 2, 2, 2, 2, 2 },
                { 1, 2, 2, 2, 2, 2 },
                { 1, 2, 2, 2, 2, 2 },
                { 1, 2, 2, 2, 2, 2 }
            };

            PuzzleTileGrid puzzleTileGrid = PuzzleTileGrid.BuildFrom2DIntegerArray(testBoard);
            PuzzleBoardTile topLeftTile = puzzleTileGrid[0, 0];
            DefaultTileMatcher defaultTileMatcher = new DefaultTileMatcher(puzzleTileGrid);

            MatchGroup matchGroup = new MatchGroup(new HashSet<PuzzleBoardTile>());
            defaultTileMatcher.TraverseTile(topLeftTile, matchGroup);
            Assert.That(matchGroup.Traversed, Has.Exactly(5).Items);
            Assert.That(matchGroup.Tiles, Has.Exactly(1).Items);
        }

        public void TraverseTileTraversesLShapeCorrectly()
        {
            int[,] testBoard = new int[,]{
                { 1, 1, 1, 2, 2, 1 },
                { 2, 2, 1, 2, 2, 1 },
                { 1, 1, 1, 2, 2, 1 },
                { 1, 2, 2, 2, 2, 1 },
                { 1, 1, 1, 1, 1, 1 }
            };

            PuzzleTileGrid puzzleTileGrid = PuzzleTileGrid.BuildFrom2DIntegerArray(testBoard);
            PuzzleBoardTile bottomLeftTile = puzzleTileGrid[0, 0];
            DefaultTileMatcher defaultTileMatcher = new DefaultTileMatcher(puzzleTileGrid);

            MatchGroup matchGroup = new MatchGroup(new HashSet<PuzzleBoardTile>());
            defaultTileMatcher.TraverseTile(bottomLeftTile, matchGroup);

            Assert.That(matchGroup.Traversed, Has.Exactly(18).Items);
            Assert.That(matchGroup.Tiles, Has.Exactly(2).Items);
        }
    }
}