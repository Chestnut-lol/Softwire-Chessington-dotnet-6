using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Queen : Piece
    {
        public Queen(Player player)
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
                AddSquare(ref squares, ref rowUp, ref colNum, ref square);
                AddSquare(ref squares, ref rowDown, ref colNum, ref square);
                colNum += 1;
            }
            for (int i = 0; i < 8; i++)
            {
                if (i == square.Col)
                {
                    ;
                }
                else
                {
                    squares.Add(new Square(square.Row, i));
                }
            }
            for (int i = 0; i < 8; i++)
            {
                if (i == square.Row)
                {
                    ;
                }
                else
                {
                    squares.Add(new Square(i, square.Col));
                }
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
        }
    }
