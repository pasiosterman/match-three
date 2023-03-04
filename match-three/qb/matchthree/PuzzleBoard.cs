using System.Collections.Generic;
using QB.MatchThree.Matching;

namespace QB.MatchThree
{
    public class PuzzleBoard
    {
        public int ComboCount { get { return Combos.Count; } }
        public List<List<PuzzleBoardTile>> AllMatches { get; } = new List<List<PuzzleBoardTile>>();
        public List<PuzzleTileMatch> Combos { get; } = new List<PuzzleTileMatch>();
        public PuzzleTileGrid Board { get; }
        private List<PuzzleBoardTile> Traversed { get; } = new List<PuzzleBoardTile>();
        private List<IMatchType> MatchTypes { get; } = new List<IMatchType>();

        public PuzzleBoard(PuzzleTileGrid board)
        {
            Board = board;
        }

        public void FillBoardWithNewTiles()
        {
            for (int y = 0; y < Board.Rows; y++)
            {
                for (int x = 0; x < Board.Columns; x++)
                {
                    PuzzleBoardTile newTile = new PuzzleBoardTile(x, y, PuzzleTileColorHelper.GetRandomTileColor());
                    Board[y, x] = newTile;
                }
            }
        }

        public void SwapTilePlaces(PuzzleBoardTile firstTile, PuzzleBoardTile secondTile)
        {
            SwapTilePlaces(firstTile, secondTile);
        }

        public void SwapTilePlaces(Index2D from, Index2D to)
        {
            if(!IsValidBoardIndex(from))
                throw new System.Exception(message: "Invalid [from] board index " + from);

            if(!IsValidBoardIndex(to))
                throw new System.Exception(message: "Invalid [to] board index " + to);

            PuzzleBoardTile fromTile = Board[from.y, from.x];
            PuzzleBoardTile toTile = Board[to.y, to.x];

            fromTile.Index = to;
            toTile.Index = from;

            Board[to.y, to.x] = fromTile;
            Board[from.y, from.x] = toTile;
        }

        public void AssignNewColorToMatchedTiles()
        {
            List<PuzzleBoardTile> matched = GetAllMatchedTiles();
            for (int i = 0; i < matched.Count; i++)
                matched[i].TileColor = PuzzleTileColorHelper.GetRandomTileColor();
        }

        public void MoveMatchedTilesToTheTop()
        {
            List<PuzzleBoardTile> matched = GetAllMatchedTiles();
            for (int y = 0; y < Board.Rows; y++)
            {
                for (int x = 0; x < Board.Columns; x++)
                {
                    PuzzleBoardTile it = GetTileWithIndex(new Index2D(x, y));
                    if (!matched.Contains(it))
                        continue;

                    for (int h = y + 1; h < Board.Rows; h++)
                    {
                        PuzzleBoardTile it2 = GetTileWithIndex(new Index2D(x, h));
                        if (!matched.Contains(it2))
                        {
                            SwapTilePlaces(it, it2);
                            int index = matched.IndexOf(it);
                        }
                    }
                }
            }
        }

        public void CheckForCombos()
        {
            for (int i = 0; i < AllMatches.Count; i++)
            {
                PuzzleTileMatch newMatch = new PuzzleTileMatch(
                    AllMatches[i].Count, 
                    AllMatches[i][0].TileColor, 
                    GetMatchType(AllMatches[i])
                );
                Combos.Add(newMatch);
            }
        }

        public void CheckForMatches()
        {
            Traversed.Clear();
            AllMatches.Clear();

            for (int y = 0; y < Board.Rows; y++)
            {
                for (int x = 0; x < Board.Columns; x++)
                {
                    PuzzleBoardTile tile = GetTileWithIndex(new Index2D(x, y));
                    if (Traversed.Contains(tile))
                        continue;

                    List<List<PuzzleBoardTile>> matchGroup = new List<List<PuzzleBoardTile>>();
                    TraverseTile(tile, matchGroup);

                    if (matchGroup.Count > 0)
                    {
                        List<PuzzleBoardTile> matchedTiles = new List<PuzzleBoardTile>();
                        for (int i = 0; i < matchGroup.Count; i++)
                        {
                            for (int j = 0; j < matchGroup[i].Count; j++)
                            {
                                if (!matchedTiles.Contains(matchGroup[i][j]))
                                    matchedTiles.Add(matchGroup[i][j]);
                            }
                        }
                        AllMatches.Add(matchedTiles);
                    }
                }
            }
        }

        public string GetCombosString()
        {
            string matchString = "";
            matchString += "Combo count: " + Combos.Count + "\n";

            for (int i = 0; i < Combos.Count; i++)
            {
                matchString += "[";
                matchString += "Combo#" + i;
                matchString += " Tile count: " + Combos[i].TileCount;
                matchString += " Tile color: " + PuzzleTileColorHelper.GetTileColorName(Combos[i].TileColor);
                matchString += " Match type: " + Combos[i].MatchType.ToString();
                matchString += "]\n";
            }
            return matchString;
        }

        public string GetBoardString()
        {
            string boardString = "\n";
            for (int y = 0; y < Board.Rows; y++)
            {
                for (int x = 0; x < Board.Columns; x++)
                {
                    boardString = "[" + PuzzleTileColorHelper.GetTileColorLetter(Board[y, x].TileColor) + "]" + boardString;
                }
                boardString = "\n" + boardString;
            }
            return boardString;
        }

        public bool IsValidBoardIndex(Index2D index)
        {
            if (index.x < 0 || index.x >= Board.Columns)
                return false;

            if (index.y < 0 || index.y >= Board.Rows)
                return false;

            return true;
        }

        private PuzzleBoardTile GetTileWithIndex(Index2D index)
        {
            if(!IsValidBoardIndex(index))
                return null;

            return Board[index.y, index.x];
        }

        private List<PuzzleBoardTile> GetAllMatchedTiles()
        {
            List<PuzzleBoardTile> matched = new List<PuzzleBoardTile>();
            for (int i = 0; i < AllMatches.Count; i++)
            {
                for (int j = 0; j < AllMatches[i].Count; j++)
                {
                    matched.Add(AllMatches[i][j]);
                }
            }
            return matched;
        }

        public void ClearMatches()
        {
            AllMatches.Clear();
        }

        public void ClearCombos()
        {
            Combos.Clear();
        }

        private IMatchType GetMatchType(List<PuzzleBoardTile> matchedTiles)
        {
            List<Index2D> matchedIndexes = MatchedIndexesFromMatchedTiles(matchedTiles);
            for (int i = 0; i < MatchTypes.Count; i++)
            {
                if(MatchTypes[i].IsValidForMatchedIndexes(matchedIndexes)){
                    return MatchTypes[i];
                }
            }
            return null;
        }

        private void TraverseTile(PuzzleBoardTile traverseTile, List<List<PuzzleBoardTile>> matchGroup)
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

            if (!Traversed.Contains(traverseTile))
                Traversed.Add(traverseTile);
        }

        private void TraverseDown(PuzzleBoardTile tile, List<List<PuzzleBoardTile>> matchGroup, List<PuzzleBoardTile> match)
        {
            PuzzleBoardTile tileBelow = GetTileWithIndex(tile.Index.DownIndex);
            if (tileBelow != null && !HasTileAlreadyBeenTraversed(tileBelow) && tile.HasMatchingColor(tileBelow))
            {
                match.Add(tileBelow);
                TraverseDown(tileBelow, matchGroup, match);
                Traversed.Add(tileBelow);

                List<PuzzleBoardTile> vertical = new List<PuzzleBoardTile>() { tileBelow };
                TraverseLeft(tileBelow, matchGroup, vertical);
                TraverseRight(tileBelow, matchGroup, vertical);
            }
            else
            {
                if (match.Count > 2 && !matchGroup.Contains(match))
                    matchGroup.Add(match);
            }
        }

        private void TraverseUp(PuzzleBoardTile tile, List<List<PuzzleBoardTile>> matchGroup, List<PuzzleBoardTile> match)
        {
            PuzzleBoardTile tileAbove = GetTileWithIndex(tile.Index.UpIndex);
            if (tileAbove != null && !HasTileAlreadyBeenTraversed(tileAbove) && tile.HasMatchingColor(tileAbove))
            {
                match.Add(tileAbove);
                TraverseUp(tileAbove, matchGroup, match);
                Traversed.Add(tileAbove);

                List<PuzzleBoardTile> vertical = new List<PuzzleBoardTile>() { tileAbove };
                TraverseLeft(tileAbove, matchGroup, vertical);
                TraverseRight(tileAbove, matchGroup, vertical);
            }
            else
            {
                if (match.Count > 2 && !matchGroup.Contains(match))
                    matchGroup.Add(match);
            }
        }

        private void TraverseLeft(PuzzleBoardTile tile, List<List<PuzzleBoardTile>> matchGroup, List<PuzzleBoardTile> match)
        {
            PuzzleBoardTile tileLeft = GetTileWithIndex(tile.Index.LeftIndex);
            if (tileLeft != null && !HasTileAlreadyBeenTraversed(tileLeft) && tile.HasMatchingColor(tileLeft))
            {
                match.Add(tileLeft);
                TraverseLeft(tileLeft, matchGroup, match);
                Traversed.Add(tileLeft);

                List<PuzzleBoardTile> vertical = new List<PuzzleBoardTile>() { tileLeft };
                TraverseUp(tileLeft, matchGroup, vertical);
                TraverseDown(tileLeft, matchGroup, vertical);
            }
            else
            {
                if (match.Count > 2 && !matchGroup.Contains(match))
                    matchGroup.Add(match);
            }
        }

        private void TraverseRight(PuzzleBoardTile tile, List<List<PuzzleBoardTile>> matchGroup, List<PuzzleBoardTile> match)
        {
            PuzzleBoardTile tileRight = GetTileWithIndex(tile.Index.RightIndex);
            if (tileRight != null && !HasTileAlreadyBeenTraversed(tileRight) && tile.HasMatchingColor(tileRight))
            {
                match.Add(tileRight);
                TraverseRight(tileRight, matchGroup, match);
                Traversed.Add(tileRight);

                List<PuzzleBoardTile> vertical = new List<PuzzleBoardTile>() { tileRight };
                TraverseUp(tileRight, matchGroup, vertical);
                TraverseDown(tileRight, matchGroup, vertical);
            }
            else
            {
                if (match.Count > 2 && !matchGroup.Contains(match))
                    matchGroup.Add(match);

            }
        }

        private bool HasTileAlreadyBeenTraversed(PuzzleBoardTile tile)
        {
            return Traversed.Contains(tile);
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