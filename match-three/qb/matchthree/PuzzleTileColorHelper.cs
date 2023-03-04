using System;

namespace QB.MatchThree
{
    public static class PuzzleTileColorHelper
    {
        public static readonly PuzzleTileColors[] AllOrbColors = new PuzzleTileColors[]
        {
            PuzzleTileColors.Blue, PuzzleTileColors.Green, PuzzleTileColors.Red,
            PuzzleTileColors.Yellow, PuzzleTileColors.Purple, PuzzleTileColors.Purple
        };

        private static readonly Random _rand = new Random();
        public static PuzzleTileColors GetRandomTileColor()
        {
            return AllOrbColors[_rand.Next(AllOrbColors.Length)];
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

        public static string GetTileColorLetter(PuzzleTileColors orbType)
        {
            int index = (int)orbType;
            string letterStr = ColorLetters[index];
            return letterStr;
        }

        public static string GetTileColorName(PuzzleTileColors orbType)
        {
            int index = (int)orbType;
            string colorStr = ColorNames[index];

            return colorStr;
        }
    }
}