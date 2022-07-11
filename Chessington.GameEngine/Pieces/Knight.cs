using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Knight : Piece
    {
        public Knight(Player player)
            : base(player)
        {
        }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            List<Square> squares = new List<Square>();
            Square square = board.FindPiece(this);
            int addRow = 2;
            int addCol = 1;
            AddSquare(ref squares, square.Row + addRow, square.Col + addCol, ref square);
            AddSquare(ref squares, square.Row - addRow, square.Col - addCol, ref square);
            AddSquare(ref squares, square.Row - addRow, square.Col + addCol, ref square);
            AddSquare(ref squares, square.Row + addRow, square.Col - addCol, ref square);
            addRow = 1;
            addCol = 2;
            AddSquare(ref squares, square.Row + addRow, square.Col + addCol, ref square);
            AddSquare(ref squares, square.Row - addRow, square.Col - addCol, ref square);
            AddSquare(ref squares, square.Row - addRow, square.Col + addCol, ref square);
            AddSquare(ref squares, square.Row + addRow, square.Col - addCol, ref square);


            return squares;
        }

        private void AddSquare(ref List<Square> squares, int rowNum, int colNum, ref Square currentSquare)
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
    