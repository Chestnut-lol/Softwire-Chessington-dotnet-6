using System.Collections.Generic;

namespace Chessington.GameEngine;

public class Utilities
{
    public static void AddSquare(ref List<Square> squares, int rowNum, int colNum, ref Square currentSquare)
    {
        if (rowNum == currentSquare.Row && colNum == currentSquare.Col)
        {
            return;
        }
        if (CheckValidSquare(rowNum, colNum))
        {
            squares.Add(new Square(rowNum, colNum));
        }
    }
    public static bool CheckValidSquare(int rowNum, int colNum)
    {
        return (rowNum >= 0 && rowNum < 8 && colNum >= 0 && colNum < 8);
    }
}