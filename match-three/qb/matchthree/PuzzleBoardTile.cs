using System;
using System.Collections.Generic;

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

        private static readonly Random _rand = new Random();
        public static PuzzleTileColors RandomTileColor()
        {
            return AllOrbColors[_rand.Next(AllOrbColors.Length)];
        }

        public static readonly PuzzleTileColors[] AllOrbColors = InitializeArrayOfValidOrbColors();

        private static PuzzleTileColors[] InitializeArrayOfValidOrbColors(){

            List<PuzzleTileColors> validColorsList = new List<PuzzleTileColors>();
            foreach (PuzzleTileColors entry in Enum.GetValues(typeof(PuzzleTileColors)))
            {
                if(entry != PuzzleTileColors.None){
                    validColorsList.Add(entry);
                }
            }
            return validColorsList.ToArray();
        }

        private static string[] _colorNames;
        public static string[] ColorNames
        {
            get
            {
                if (_colorNames == null)
                    _colorNames = Enum.GetNames(typeof(PuzzleTileColors)) as string[];

                return _colorNames;
            }
        }

        private static string[] _nodeLetters;
        public static string[] ColorLetters
        {
            get
            {
                if (_nodeLetters == null)
                {
                    _nodeLetters = new string[ColorNames.Length];
                    for (int i = 0; i < _nodeLetters.Length; i++)
                        _nodeLetters[i] = ColorNames[i].Substring(0, 1);
                }
                return _nodeLetters;
            }
        }

        public static string FirstLetterOfTileColorName(PuzzleTileColors orbType)
        {
            int index = (int)orbType;
            string letterStr = ColorLetters[index];
            return letterStr;
        }

        public static string NameOfTileColor(PuzzleTileColors orbType)
        {
            int index = (int)orbType;
            string colorStr = ColorNames[index];

            return colorStr;
        }
    }
}