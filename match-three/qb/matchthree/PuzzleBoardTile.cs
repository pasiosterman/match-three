namespace QB.MatchThree
{
    public class PuzzleBoardTile
    {
        public PuzzleTileColors TileColor { get; set; }
        public Index2D Index { get; set; }
        public PuzzleBoardTile() { }

        public PuzzleBoardTile(int x, int y, PuzzleTileColors tileColor)
        {
            Index = new Index2D(x, y);
            TileColor = tileColor;
        }

        public bool HasMatchingColor(PuzzleBoardTile boardTile)
        {
            if (!GetTileColorValidity() || !boardTile.GetTileColorValidity())
                return false;

            return HasMatchingColor(boardTile.TileColor);
        }

        public bool HasMatchingColor(PuzzleTileColors matchColor)
        {
            return TileColor == matchColor;
        }

        public bool GetTileColorValidity()
        {
            if (TileColor != PuzzleTileColors.None)
                return true;
            else
                return false;
        }
    }
}