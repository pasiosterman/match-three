# Match-three 

## Description

Generic Match-three algorithm with C# using recursion.

# Points of interest

## Matching algorithm 

If you're looking for the actual algorithm it's implemented in [DefaultTileMatcher](match-three/qb/matchthree/matching/matchers/DefaultTileMatcher.cs). 
It traverses the PuzzleTileGrid with recursion looking for matches of three and above. Then it checks those matches with IMatchType [implementations](match-three/qb/matchthree/matching/matchtypes/) to return types of said matches i.e match-three, match-four, row-match, column-match etc. 

