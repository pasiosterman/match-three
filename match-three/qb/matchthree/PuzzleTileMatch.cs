using QB.MatchThree.Matching;

namespace QB.MatchThree
{
    public class PuzzleTileMatch
    {
        public int TileCount { get; private set; }
        public PuzzleTileColors TileColor { get; set; }
        public IMatchType MatchType {get; private set; }
        public PuzzleTileMatch(int count, PuzzleTileColors tileColor, IMatchType matchType){
            TileCount = count;
            TileColor = tileColor;
            MatchType = matchType;
        }
    }
}