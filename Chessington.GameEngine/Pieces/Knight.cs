using System;
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
            AddSquare(ref squares, square.Row + addRow, square.Col + addCol, ref square, ref board);
            AddSquare(ref squares, square.Row - addRow, square.Col - addCol, ref square, ref board);
            AddSquare(ref squares, square.Row - addRow, square.Col + addCol, ref square, ref board);
            AddSquare(ref squares, square.Row + addRow, square.Col - addCol, ref square, ref board);
            addRow = 1;
            addCol = 2;
            AddSquare(ref squares, square.Row + addRow, square.Col + addCol, ref square, ref board);
            AddSquare(ref squares, square.Row - addRow, square.Col - addCol, ref square, ref board);
            AddSquare(ref squares, square.Row - addRow, square.Col + addCol, ref square, ref board);
            AddSquare(ref squares, square.Row + addRow, square.Col - addCol, ref square, ref board);


            return squares;
        }
        private bool CheckValidSquare(int rowNum, int colNum)
        {
            return (rowNum >= 0 && rowNum < 8 && colNum >= 0 && colNum < 8);
        }

        private bool CheckSquareTakenByFriendly(int rowNum, int colNum, ref Board board)
        {
            if (!CheckValidSquare(rowNum, colNum))
            {
                throw new Exception("Invalid rowNum and/or colNum!");
            }

            var piece = board.GetPiece(new Square(rowNum, colNum));
            if ((piece == null) || (piece.Player != this.Player))
            {
                return false;
            }

            return true;
        }
        private void AddSquare(ref List<Square> squares, int rowNum, int colNum, ref Square currentSquare, ref Board board)
        {
            if (rowNum == currentSquare.Row && colNum == currentSquare.Col)
            {
                return;
            }

            if (CheckValidSquare(rowNum, colNum))
            {
                if (!CheckSquareTakenByFriendly(rowNum, colNum, ref board))
                {
                    squares.Add(new Square(rowNum, colNum));
                }
            }
        }
    }
}
    