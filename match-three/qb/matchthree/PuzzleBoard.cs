using System.Collections.Generic;
using QB.MatchThree.Matching;

namespace QB.MatchThree
{
    public class PuzzleBoard
    {
        public PuzzleTileGrid Board { get; }
        public IMatcher Matcher { get; }

        public PuzzleBoard(PuzzleTileGrid board, IMatcher matcher)
        {
            Board = board;
            Matcher = matcher;
        }

        public void FillBoardWithNewTiles()
        {
            Board.FillGridWithRandomTiles();
        }

        public void SwapTilePlaces(PuzzleBoardTile firstTile, PuzzleBoardTile secondTile)
        {
            SwapTilePlaces(firstTile.Index, secondTile.Index);
        }

        public void SwapTilePlaces(Index2D from, Index2D to)
        {
            if (!IsValidBoardIndex(from))
                throw new System.Exception(message: "Invalid [from] board index " + from);

            if (!IsValidBoardIndex(to))
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
            // List<PuzzleBoardTile> matched = GetAllMatchedTiles();
            // for (int i = 0; i < matched.Count; i++)
            //     matched[i].TileColor = PuzzleBoardTile.RandomTileColor();
        }

        public void MoveMatchedTilesToTheTop()
        {
            // List<PuzzleBoardTile> matched = GetAllMatchedTiles();
            // for (int y = 0; y < Board.Rows; y++)
            // {
            //     for (int x = 0; x < Board.Columns; x++)
            //     {
            //         PuzzleBoardTile it = GetTileWithIndex(new Index2D(x, y));
            //         if (!matched.Contains(it))
            //             continue;

            //         for (int h = y + 1; h < Board.Rows; h++)
            //         {
            //             PuzzleBoardTile it2 = GetTileWithIndex(new Index2D(x, h));
            //             if (!matched.Contains(it2))
            //             {
            //                 SwapTilePlaces(it, it2);
            //                 int index = matched.IndexOf(it);
            //             }
            //         }
            //     }
            // }
        }

        public string GetCombosString()
        {
            string matchString = "";
            // matchString += "Combo count: " + Combos.Count + "\n";

            // for (int i = 0; i < Combos.Count; i++)
            // {
            //     matchString += "[";
            //     matchString += "Combo#" + i;
            //     matchString += " Tile count: " + Combos[i].TileCount;
            //     matchString += " Tile color: " + PuzzleBoardTile.NameOfTileColor(Combos[i].TileColor);
            //     matchString += " Match type: " + Combos[i].MatchType.ToString();
            //     matchString += "]\n";
            // }
            return matchString;
        }

        public string GetBoardString()
        {
            return Board.ToString();
        }

        public bool IsValidBoardIndex(Index2D index)
        {
            return Board.IsValidBoardIndex(index);
        }

        private PuzzleBoardTile GetTileWithIndex(Index2D index)
        {
            return Board.GetTileWithIndex(index);
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