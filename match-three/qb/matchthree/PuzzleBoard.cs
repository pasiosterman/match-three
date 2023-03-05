// using System.Collections.Generic;
// using QB.MatchThree.Matching;

// namespace QB.MatchThree
// {
//     public class PuzzleBoard
//     {
//         public PuzzleTileGrid Board { get; }
//         private List<IMatchType> MatchTypes { get; } = new List<IMatchType>();

//         public PuzzleBoard(PuzzleTileGrid board)
//         {
//             Board = board;
//         }

//         public void FillBoardWithNewTiles()
//         {
//             for (int y = 0; y < Board.Rows; y++)
//             {
//                 for (int x = 0; x < Board.Columns; x++)
//                 {
//                     PuzzleBoardTile newTile = new PuzzleBoardTile(x, y, PuzzleBoardTile.RandomTileColor());
//                     Board[y, x] = newTile;
//                 }
//             }
//         }

//         public void SwapTilePlaces(PuzzleBoardTile firstTile, PuzzleBoardTile secondTile)
//         {
//             SwapTilePlaces(firstTile, secondTile);
//         }

//         public void SwapTilePlaces(Index2D from, Index2D to)
//         {
//             if (!IsValidBoardIndex(from))
//                 throw new System.Exception(message: "Invalid [from] board index " + from);

//             if (!IsValidBoardIndex(to))
//                 throw new System.Exception(message: "Invalid [to] board index " + to);

//             PuzzleBoardTile fromTile = Board[from.y, from.x];
//             PuzzleBoardTile toTile = Board[to.y, to.x];

//             fromTile.Index = to;
//             toTile.Index = from;

//             Board[to.y, to.x] = fromTile;
//             Board[from.y, from.x] = toTile;
//         }

//         // public void AssignNewColorToMatchedTiles()
//         // {
//         //     List<PuzzleBoardTile> matched = GetAllMatchedTiles();
//         //     for (int i = 0; i < matched.Count; i++)
//         //         matched[i].TileColor = PuzzleBoardTile.RandomTileColor();
//         // }

//         // public void MoveMatchedTilesToTheTop()
//         // {
//         //     List<PuzzleBoardTile> matched = GetAllMatchedTiles();
//         //     for (int y = 0; y < Board.Rows; y++)
//         //     {
//         //         for (int x = 0; x < Board.Columns; x++)
//         //         {
//         //             PuzzleBoardTile it = GetTileWithIndex(new Index2D(x, y));
//         //             if (!matched.Contains(it))
//         //                 continue;

//         //             for (int h = y + 1; h < Board.Rows; h++)
//         //             {
//         //                 PuzzleBoardTile it2 = GetTileWithIndex(new Index2D(x, h));
//         //                 if (!matched.Contains(it2))
//         //                 {
//         //                     SwapTilePlaces(it, it2);
//         //                     int index = matched.IndexOf(it);
//         //                 }
//         //             }
//         //         }
//         //     }
//         // }

//         // public string GetCombosString()
//         // {
//         //     string matchString = "";
//         //     matchString += "Combo count: " + Combos.Count + "\n";

//         //     for (int i = 0; i < Combos.Count; i++)
//         //     {
//         //         matchString += "[";
//         //         matchString += "Combo#" + i;
//         //         matchString += " Tile count: " + Combos[i].TileCount;
//         //         matchString += " Tile color: " + PuzzleBoardTile.NameOfTileColor(Combos[i].TileColor);
//         //         matchString += " Match type: " + Combos[i].MatchType.ToString();
//         //         matchString += "]\n";
//         //     }
//         //     return matchString;
//         // }

//         public string GetBoardString()
//         {
//             return Board.ToString();
//         }

//         public bool IsValidBoardIndex(Index2D index)
//         {
//             if (index.x < 0 || index.x >= Board.Columns)
//                 return false;

//             if (index.y < 0 || index.y >= Board.Rows)
//                 return false;

//             return true;
//         }

//         private PuzzleBoardTile GetTileWithIndex(Index2D index)
//         {
//             if (!IsValidBoardIndex(index))
//                 return null;

//             return Board[index.y, index.x];
//         }

//         // private List<PuzzleBoardTile> GetAllMatchedTiles()
//         // {
//         //     List<PuzzleBoardTile> matched = new List<PuzzleBoardTile>();
//         //     for (int i = 0; i < AllMatches.Count; i++)
//         //     {
//         //         for (int j = 0; j < AllMatches[i].Count; j++)
//         //         {
//         //             matched.Add(AllMatches[i][j]);
//         //         }
//         //     }
//         //     return matched;
//         // }

//         private IMatchType GetMatchType(List<PuzzleBoardTile> matchedTiles)
//         {
//             List<Index2D> matchedIndexes = MatchedIndexesFromMatchedTiles(matchedTiles);
//             for (int i = 0; i < MatchTypes.Count; i++)
//             {
//                 if (MatchTypes[i].IsValidForMatchedIndexes(matchedIndexes))
//                 {
//                     return MatchTypes[i];
//                 }
//             }
//             return null;
//         }

//         public List<PuzzleTileMatch> CheckBoardForMatches()
//         {
//             List<List<PuzzleBoardTile>> matches = findAllMatchedTilesFromBoard();
//             List<PuzzleTileMatch> combos = new List<PuzzleTileMatch>();
//             for (int i = 0; i < matches.Count; i++)
//             {
//                 PuzzleTileMatch newMatch = new PuzzleTileMatch(
//                     matches[i].Count,
//                     matches[i][0].TileColor,
//                     GetMatchType(matches[i])
//                 );
//                 combos.Add(newMatch);
//             }
//             return combos;
//         }

//         private List<List<PuzzleBoardTile>> findAllMatchedTilesFromBoard()
//         {
//             List<List<PuzzleBoardTile>> allMatches = new List<List<PuzzleBoardTile>>();
//             List<PuzzleBoardTile> traversed = new List<PuzzleBoardTile>();

//             for (int y = 0; y < Board.Rows; y++)
//             {
//                 for (int x = 0; x < Board.Columns; x++)
//                 {
//                     PuzzleBoardTile tile = GetTileWithIndex(new Index2D(x, y));
//                     if (traversed.Contains(tile))
//                         continue;

//                     MatchGroup matchGroup = new MatchGroup();
//                     TraverseTile(tile, matchGroup);

//                     if (matchGroup.Tiles.Count > 0)
//                     {
//                         List<PuzzleBoardTile> matchedTiles = new List<PuzzleBoardTile>();
//                         for (int i = 0; i < matchGroup.Tiles.Count; i++)
//                         {
//                             for (int j = 0; j < matchGroup.Tiles[i].Count; j++)
//                             {
//                                 if (!matchedTiles.Contains(matchGroup.Tiles[i][j]))
//                                     matchedTiles.Add(matchGroup.Tiles[i][j]);
//                             }
//                         }
//                         allMatches.Add(matchedTiles);
//                     }
//                 }
//             }
//             return allMatches;
//         }

//         public void TraverseTile(PuzzleBoardTile traverseTile, MatchGroup matchGroup)
//         {
//             List<PuzzleBoardTile> horizontal = new List<PuzzleBoardTile>() { traverseTile };
//             TraverseRight(traverseTile, matchGroup, horizontal);
//             TraverseLeft(traverseTile, matchGroup, horizontal);

//             List<PuzzleBoardTile> vertical = new List<PuzzleBoardTile>() { traverseTile };
//             TraverseUp(traverseTile, matchGroup, vertical);
//             TraverseDown(traverseTile, matchGroup, vertical);

//             //First tile should not be added to traversed list before traversal has been completed due to how it would cause problems with circular traversal.
//             //I.E two columns of 3 red tiles would only clear the second column because 7th traversal cannot be completed due to 1th being already in tarversed list.
//             //[5][4]
//             //[6][3]
//             //[1][2]

//             if (!matchGroup.Traversed.Contains(traverseTile))
//                 matchGroup.Traversed.Add(traverseTile);
//         }

//         private void TraverseDown(PuzzleBoardTile tile, MatchGroup matchGroup, List<PuzzleBoardTile> match)
//         {
//             PuzzleBoardTile tileBelow = GetTileWithIndex(tile.Index.DownIndex);
//             if (tileBelow != null && !matchGroup.HasTileAlreadyBeenTraversed(tileBelow) && tile.HasMatchingColor(tileBelow))
//             {
//                 match.Add(tileBelow);
//                 TraverseDown(tileBelow, matchGroup, match);
//                 matchGroup.Traversed.Add(tileBelow);

//                 List<PuzzleBoardTile> vertical = new List<PuzzleBoardTile>() { tileBelow };
//                 TraverseLeft(tileBelow, matchGroup, vertical);
//                 TraverseRight(tileBelow, matchGroup, vertical);
//             }
//             else
//             {
//                 if (match.Count > 2 && !matchGroup.Tiles.Contains(match))
//                     matchGroup.Tiles.Add(match);
//             }
//         }

//         private void TraverseUp(PuzzleBoardTile tile, MatchGroup matchGroup, List<PuzzleBoardTile> match)
//         {
//             PuzzleBoardTile tileAbove = GetTileWithIndex(tile.Index.UpIndex);
//             if (tileAbove != null && !matchGroup.HasTileAlreadyBeenTraversed(tileAbove) && tile.HasMatchingColor(tileAbove))
//             {
//                 match.Add(tileAbove);
//                 TraverseUp(tileAbove, matchGroup, match);
//                 matchGroup.Traversed.Add(tileAbove);

//                 List<PuzzleBoardTile> vertical = new List<PuzzleBoardTile>() { tileAbove };
//                 TraverseLeft(tileAbove, matchGroup, vertical);
//                 TraverseRight(tileAbove, matchGroup, vertical);
//             }
//             else
//             {
//                 if (match.Count > 2 && !matchGroup.Tiles.Contains(match))
//                     matchGroup.Tiles.Add(match);
//             }
//         }

//         private void TraverseLeft(PuzzleBoardTile tile, MatchGroup matchGroup, List<PuzzleBoardTile> match)
//         {
//             PuzzleBoardTile tileLeft = GetTileWithIndex(tile.Index.LeftIndex);
//             if (tileLeft != null && !matchGroup.HasTileAlreadyBeenTraversed(tileLeft) && tile.HasMatchingColor(tileLeft))
//             {
//                 match.Add(tileLeft);
//                 TraverseLeft(tileLeft, matchGroup, match);
//                 matchGroup.Traversed.Add(tileLeft);

//                 List<PuzzleBoardTile> vertical = new List<PuzzleBoardTile>() { tileLeft };
//                 TraverseUp(tileLeft, matchGroup, vertical);
//                 TraverseDown(tileLeft, matchGroup, vertical);
//             }
//             else
//             {
//                 if (match.Count > 2 && !matchGroup.Tiles.Contains(match))
//                     matchGroup.Tiles.Add(match);
//             }
//         }

//         private void TraverseRight(PuzzleBoardTile tile, MatchGroup matchGroup, List<PuzzleBoardTile> match)
//         {
//             PuzzleBoardTile tileRight = GetTileWithIndex(tile.Index.RightIndex);
//             if (tileRight != null && !matchGroup.HasTileAlreadyBeenTraversed(tileRight) && tile.HasMatchingColor(tileRight))
//             {
//                 match.Add(tileRight);
//                 TraverseRight(tileRight, matchGroup, match);
//                 matchGroup.Traversed.Add(tileRight);

//                 List<PuzzleBoardTile> vertical = new List<PuzzleBoardTile>() { tileRight };
//                 TraverseUp(tileRight, matchGroup, vertical);
//                 TraverseDown(tileRight, matchGroup, vertical);
//             }
//             else
//             {
//                 if (match.Count > 2 && !matchGroup.Tiles.Contains(match))
//                     matchGroup.Tiles.Add(match);

//             }
//         }

//         private static List<Index2D> MatchedIndexesFromMatchedTiles(List<PuzzleBoardTile> matchedTiles)
//         {
//             List<Index2D> matchedIndexes = new List<Index2D>();
//             for (int i = 0; i < matchedTiles.Count; i++)
//             {
//                 matchedIndexes.Add(matchedTiles[i].Index);
//             }
//             return matchedIndexes;
//         }


//     }
// }