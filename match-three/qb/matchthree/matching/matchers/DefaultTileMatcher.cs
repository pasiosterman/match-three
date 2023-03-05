
using System.Collections.Generic;
using QB.MatchThree.Matching.MatchTypes;

namespace QB.MatchThree.Matching.Matchers
{
    public class DefaultTileMatcher : IMatcher
    {
        readonly IMatchType[] matchTypes;
        readonly PuzzleTileGrid tileGrid;

        public DefaultTileMatcher(PuzzleTileGrid puzzleTileGrid)
        {
            tileGrid = puzzleTileGrid;
            matchTypes = new IMatchType[] {
                new RowMatch(tileGrid.Columns),
                new ColumnMatch(tileGrid.Rows),
                new CrossMatch(),
                new LinkedMatch(),
                new FourMatch(),
                new ThreeMatch()
            };
        }

        public List<PuzzleTileMatch> CheckBoardForMatches()
        {
            List<List<PuzzleBoardTile>> matches = findAllMatchedTilesFromBoard();
            List<PuzzleTileMatch> combos = new List<PuzzleTileMatch>();
            for (int i = 0; i < matches.Count; i++)
            {
                PuzzleTileMatch newMatch = new PuzzleTileMatch(
                    matches[i].Count,
                    matches[i][0].TileColor,
                    GetMatchType(matches[i])
                );
                combos.Add(newMatch);
            }
            return combos;
        }

        
        public List<List<PuzzleBoardTile>> findAllMatchedTilesFromBoard()
        {
            List<List<PuzzleBoardTile>> allMatches = new List<List<PuzzleBoardTile>>();
            HashSet<PuzzleBoardTile> traversed = new HashSet<PuzzleBoardTile>();

            for (int y = 0; y < tileGrid.Rows; y++)
            {
                for (int x = 0; x < tileGrid.Columns; x++)
                {
                    PuzzleBoardTile tile = tileGrid[y, x];
                    if (traversed.Contains(tile))
                        continue;

                    MatchGroup matchGroup = new MatchGroup(traversed);
                    TraverseTile(tile, matchGroup);

                    if (matchGroup.Tiles.Count > 0)
                    {
                        List<PuzzleBoardTile> matchedTiles = new List<PuzzleBoardTile>();
                        for (int i = 0; i < matchGroup.Tiles.Count; i++)
                        {
                            for (int j = 0; j < matchGroup.Tiles[i].Count; j++)
                            {
                                if (!matchedTiles.Contains(matchGroup.Tiles[i][j]))
                                    matchedTiles.Add(matchGroup.Tiles[i][j]);
                            }
                        }
                        allMatches.Add(matchedTiles);
                    }
                }
            }
            return allMatches;
        }

        public void TraverseTile(PuzzleBoardTile traverseTile, MatchGroup matchGroup)
        {
            List<PuzzleBoardTile> horizontal = new List<PuzzleBoardTile>() { traverseTile };
            TraverseRight(traverseTile, matchGroup, horizontal);
            TraverseLeft(traverseTile, matchGroup, horizontal);

            List<PuzzleBoardTile> vertical = new List<PuzzleBoardTile>() { traverseTile };
            TraverseUp(traverseTile, matchGroup, vertical);
            TraverseDown(traverseTile, matchGroup, vertical);

            //First tile should not be added to traversed list before traversal has been completed due to how it would cause problems with circular traversal.
            //I.E two columns of 3 red tiles would only clear the second column because 7th traversal cannot be completed due to 1th being already in tarversed list.
            //[5][4]
            //[6][3]
            //[1][2]

            if (!matchGroup.Traversed.Contains(traverseTile))
                matchGroup.Traversed.Add(traverseTile);
        }

        private void TraverseDown(PuzzleBoardTile tile, MatchGroup matchGroup, List<PuzzleBoardTile> match)
        {
            PuzzleBoardTile tileBelow = tileGrid.GetTileWithIndex(tile.Index.DownIndex);
            if (tileBelow != null && !matchGroup.HasTileAlreadyBeenTraversed(tileBelow) && tile.HasMatchingColor(tileBelow))
            {
                match.Add(tileBelow);
                TraverseDown(tileBelow, matchGroup, match);
                matchGroup.Traversed.Add(tileBelow);

                List<PuzzleBoardTile> vertical = new List<PuzzleBoardTile>() { tileBelow };
                TraverseLeft(tileBelow, matchGroup, vertical);
                TraverseRight(tileBelow, matchGroup, vertical);
            }
            else
            {
                if (match.Count > 2 && !matchGroup.Tiles.Contains(match))
                    matchGroup.Tiles.Add(match);
            }
        }

        private void TraverseUp(PuzzleBoardTile tile, MatchGroup matchGroup, List<PuzzleBoardTile> match)
        {
            PuzzleBoardTile tileAbove = tileGrid.GetTileWithIndex(tile.Index.UpIndex);
            if (tileAbove != null && !matchGroup.HasTileAlreadyBeenTraversed(tileAbove) && tile.HasMatchingColor(tileAbove))
            {
                match.Add(tileAbove);
                TraverseUp(tileAbove, matchGroup, match);
                matchGroup.Traversed.Add(tileAbove);

                List<PuzzleBoardTile> vertical = new List<PuzzleBoardTile>() { tileAbove };
                TraverseLeft(tileAbove, matchGroup, vertical);
                TraverseRight(tileAbove, matchGroup, vertical);
            }
            else
            {
                if (match.Count > 2 && !matchGroup.Tiles.Contains(match))
                    matchGroup.Tiles.Add(match);
            }
        }

        private void TraverseLeft(PuzzleBoardTile tile, MatchGroup matchGroup, List<PuzzleBoardTile> match)
        {
            PuzzleBoardTile tileLeft = tileGrid.GetTileWithIndex(tile.Index.LeftIndex);
            if (tileLeft != null && !matchGroup.HasTileAlreadyBeenTraversed(tileLeft) && tile.HasMatchingColor(tileLeft))
            {
                match.Add(tileLeft);
                TraverseLeft(tileLeft, matchGroup, match);
                matchGroup.Traversed.Add(tileLeft);

                List<PuzzleBoardTile> vertical = new List<PuzzleBoardTile>() { tileLeft };
                TraverseUp(tileLeft, matchGroup, vertical);
                TraverseDown(tileLeft, matchGroup, vertical);
            }
            else
            {
                if (match.Count > 2 && !matchGroup.Tiles.Contains(match))
                    matchGroup.Tiles.Add(match);
            }
        }

        private void TraverseRight(PuzzleBoardTile tile, MatchGroup matchGroup, List<PuzzleBoardTile> match)
        {
            PuzzleBoardTile tileRight = tileGrid.GetTileWithIndex(tile.Index.RightIndex);
            if (tileRight != null && !matchGroup.HasTileAlreadyBeenTraversed(tileRight) && tile.HasMatchingColor(tileRight))
            {
                match.Add(tileRight);
                TraverseRight(tileRight, matchGroup, match);
                matchGroup.Traversed.Add(tileRight);

                List<PuzzleBoardTile> vertical = new List<PuzzleBoardTile>() { tileRight };
                TraverseUp(tileRight, matchGroup, vertical);
                TraverseDown(tileRight, matchGroup, vertical);
            }
            else
            {
                if (match.Count > 2 && !matchGroup.Tiles.Contains(match))
                    matchGroup.Tiles.Add(match);
            }
        }

        private IMatchType GetMatchType(List<PuzzleBoardTile> matchedTiles)
        {
            List<Index2D> matchedIndexes = MatchedIndexesFromMatchedTiles(matchedTiles);
            for (int i = 0; i < matchTypes.Length; i++)
            {
                if (matchTypes[i].IsValidForMatchedIndexes(matchedIndexes))
                {
                    return matchTypes[i];
                }
            }
            return null;
        }

        private static List<Index2D> MatchedIndexesFromMatchedTiles(List<PuzzleBoardTile> matchedTiles)
        {
            List<Index2D> matchedIndexes = new List<Index2D>();
            for (int i = 0; i < matchedTiles.Count; i++)
            {
                matchedIndexes.Add(matchedTiles[i].Index);
            }
            return matchedIndexes;
        }
    }
}