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

        public PuzzleBoardTile GetTileWithIndex(Index2D index)
        {
            if(!IsValidBoardIndex(index))
                return null;

            return this[index.y, index.x];
        }

        public bool IsValidBoardIndex(Index2D index)
        {
            if (index.x < 0 || index.x >= Columns)
                return false;

            if (index.y < 0 || index.y >= Rows)
                return false;

            return true;
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
                    PuzzleBoardTile newTile = new PuzzleBoardTile(x, y, PuzzleBoardTile.RandomTileColor());
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

        public static PuzzleTileGrid BuildFrom2DIntegerArray(int[,] grid) {

            int Columns = grid.GetLength(1);
            int Rows = grid.GetLength(0);

            PuzzleTileGrid newBoard = new PuzzleTileGrid(Columns, Rows);
            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < Columns; x++)
                {
                    newBoard[y, x] = new PuzzleBoardTile(x, y, (PuzzleTileColors)grid[y, x] );
                }
            }
            return newBoard;
        }

        public override string ToString()
        {
            string output = "Grid: \n";
            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < Columns; x++)
                {
                    output += "[" +  this[y, x].TileColor.ToString().Substring(0,1) + "]";
                    PuzzleBoardTile newTile = new PuzzleBoardTile(x, y, PuzzleTileColors.None);
                    this[y, x] = newTile;
                }
                output += "\n";
            }
            return output;
        }
    }
}