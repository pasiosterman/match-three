// using System;
// using QB.MatchThree.Matching;

// namespace QB.MatchThree
// {
//     static class PuzzleMatchTypesHelper
//     {
//         // public static readonly PuzzleMatchTypes[] AllMatchTypes = new PuzzleMatchTypes[]
//         // {
//         //     PuzzleMatchTypes.Three, PuzzleMatchTypes.Four, PuzzleMatchTypes.Row,
//         //     PuzzleMatchTypes.Linked, PuzzleMatchTypes.Cross
//         // };

//         private static string[] _matchTypeNames;
//         public static string[] MatchTypeNames
//         {
//             get
//             {
//                 if (_matchTypeNames == null)
//                     _matchTypeNames = Enum.GetNames(typeof(PuzzleMatchTypes)) as string[];

//                 return _matchTypeNames;
//             }
//         }

//         public static string GetMatchTypeName(PuzzleTileColors orbType)
//         {
//             int index = (int)orbType;
//             string colorStr = MatchTypeNames[index];

//             return colorStr;
//         }
//     }
// }

