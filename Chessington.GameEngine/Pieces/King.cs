using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class King : Piece
    {
        public King(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            List<Square> squares = new List<Square>();
            Square square = board.FindPiece(this);
            int rowNum = square.Row + 1;
            int colNum = square.Col + 1;
            for (int i = -1; i < 2; i++)
            {
                AddSquare(ref squares, rowNum, colNum, ref square);
                rowNum -= 1;
            }

            colNum = square.Col - 1;
            rowNum = square.Row + 1;
            for (int i = -1; i < 2; i++)
            {
                AddSquare(ref squares, rowNum, colNum, ref square);
                rowNum -= 1;
            }
            AddSquare(ref squares, square.Row + 1, square.Col, ref square);
            AddSquare(ref squares, square.Row - 1, square.Col, ref square);
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