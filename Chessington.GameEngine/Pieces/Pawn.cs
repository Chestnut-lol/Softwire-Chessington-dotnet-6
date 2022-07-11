using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls.Ribbon;

namespace Chessington.GameEngine.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(Player player) 
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var square = board.FindPiece(this);
            List<Square> squares = new List<Square>();
            int rowNum = GetOneRowUp(square.Row);
            int colNum = square.Col;
            if (CheckValidSquare(rowNum, colNum) && board.GetPiece(new Square(rowNum, colNum)) != null)
            {
                return squares;
            }
            AddSquare(ref squares, rowNum, colNum, ref square, ref board);
            if (HasMoved(board))
            {
                AddSquare(ref squares, GetOneRowUp(rowNum), square.Col, ref square, ref board);
            }

            return squares;
        }

        private int GetOneRowUp(int rowNum)
        {
            if (this.Player == Player.White)
            {
                return rowNum - 1;
                
            }
            else
            {
                return rowNum + 1;
            }
        }
        private bool HasMoved(Board board)
        {
            if (this.Player == Player.Black)
            {
                return board.FindPiece(this).Row == 1;
            }
            else
            {
                return board.FindPiece(this).Row == 7;
            }
        }
        private bool CheckValidSquare(int rowNum, int colNum)
        {
            return (rowNum >= 0 && rowNum < 8 && colNum >= 0 && colNum < 8);
        }
        private void AddSquare(ref List<Square> squares, int rowNum, int colNum, ref Square currentSquare, ref Board board)
        {
            if (rowNum == currentSquare.Row && colNum == currentSquare.Col)
            {
                return;
            }

            if (CheckValidSquare(rowNum, colNum) && board.GetPiece(new Square(rowNum, colNum)) != null)
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