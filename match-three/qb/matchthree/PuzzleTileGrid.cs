namespace QB.MatchThree
{
    public class PuzzleTileGrid
    {
        private PuzzleBoardTile[,] _boardArray = new PuzzleBoardTile[0, 0];

        public PuzzleBoardTile this[int y, int x]
        {
            get { return _boardArray[y, x]; }
            set { _boardArray[y, x] = value; }
        }

        public PuzzleTileGrid(int colums, int rows)
        {
            _boardArray = new PuzzleBoardTile[rows, colums];
            FillGridWithEmptyTiles();
        }

        public PuzzleTileGrid CreateCopy()
        {
            PuzzleTileGrid newBoard = new PuzzleTileGrid(Columns, Rows);
            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < Columns; x++)
                {
                    newBoard[y, x] = new PuzzleBoardTile(x, y, _boardArray[y, x].TileColor);
                }
            }
            return newBoard;
        }

        public void FillGridWithRandomTiles()
        {
            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < Columns; x++)
                {
                    PuzzleBoardTile newTile = new PuzzleBoardTile(x, y, PuzzleTileColorHelper.GetRandomTileColor());
                    this[y, x] = newTile;
                }
            }
        }

        public void FillGridWithEmptyTiles()
        {
            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < Columns; x++)
                {
                    PuzzleBoardTile newTile = new PuzzleBoardTile(x, y, PuzzleTileColors.None);
                    this[y, x] = newTile;
                }
            }
        }

        public int Rows { get { return _boardArray.GetLength(0); } }
        public int Columns { get { return _boardArray.GetLength(1); } }

        public PuzzleBoardTile[] AllTiles
        {
            get
            {
                PuzzleBoardTile[] tiles = new PuzzleBoardTile[Columns * Rows];
                for (int y = 0; y < Rows; y++)
                {
                    for (int x = 0; x < Columns; x++)
                    {
                        tiles[x + (y * Columns)] = _boardArray[y, x];
                    }
                }
                return tiles;
            }
        }

    }
}