using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            List<Square> squares = new List<Square>();
            Square square = board.FindPiece(this);
            int colNum = square.Col - 1;
            int rowUp = square.Row;
            int rowDown = square.Row;
            while (colNum >= 0)
            {
                rowUp += 1;
                rowDown -= 1;
                if (CheckValidSquare(rowUp, colNum))
                {
                    if (board.GetPiece(new Square(rowUp, colNum)) != null)
                    {
                        rowUp = 8; // No more squares will be added along this line
                    }
                    if (board.GetPiece(new Square(rowDown, colNum)) != null)
                    {
                        rowDown = -1; // No more squares will be added along this line
                    }
                }
                AddSquare(ref squares, ref rowUp, ref colNum, ref square);
                AddSquare(ref squares, ref rowDown, ref colNum, ref square);
                colNum -= 1;
            }

            colNum = square.Col + 1;
            rowUp = square.Row;
            rowDown = square.Row;
            while (colNum < 8)
            {
                rowUp += 1;
                rowDown -= 1;
                if (CheckValidSquare(rowUp, colNum))
                {
                    if (board.GetPiece(new Square(rowUp, colNum)) != null)
                    {
                        rowUp = 8; // No more squares will be added along this line
                    }
                    if (board.GetPiece(new Square(rowDown, colNum)) != null)
                    {
                        rowDown = -1; // No more squares will be added along this line
                    }
                }
                AddSquare(ref squares, ref rowUp, ref colNum, ref square);
                AddSquare(ref squares, ref rowDown, ref colNum, ref square);
                colNum += 1;
            }

            return squares;
        }

        private void AddSquare(ref List<Square> squares, ref int rowNum, ref int colNum, ref Square currentSquare)
        {
            if (rowNum == currentSquare.Row && colNum == currentSquare.Col)
            {
                return;
            }
            if (rowNum >= 0 && rowNum < 8 && colNum >= 0 && colNum < 8)
            {
                squares.Add(new Square(rowNum, colNum));
            }
        }
        private bool CheckValidSquare(int rowNum, int colNum)
        {
            return (rowNum >= 0 && rowNum < 8 && colNum >= 0 && colNum < 8);
        }
    }
}