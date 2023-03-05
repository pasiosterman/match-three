using System.Collections.Generic;
using QB.MatchThree.Matching;

namespace QB.MatchThree.Matching.Matchers
{
    public class MatchGroup
    {
        public readonly List<List<PuzzleBoardTile>> Tiles;
        public readonly HashSet<PuzzleBoardTile> Traversed;

        public MatchGroup(HashSet<PuzzleBoardTile> traversed)
        {
            Tiles = new List<List<PuzzleBoardTile>>();
            Traversed = traversed;
        }

        public bool HasTileAlreadyBeenTraversed(PuzzleBoardTile tile)
        {
            return Traversed.Contains(tile);
        }
    }
}