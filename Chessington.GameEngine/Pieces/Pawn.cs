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
            TakeOpponentPiece(ref board, ref square, ref squares);
            if (CheckValidSquare(rowNum, colNum) && board.GetPiece(new Square(rowNum, colNum)) != null)
            {
                return squares;
            }
            AddSquare(ref squares, rowNum, colNum, ref square, ref board);
            if (HasNotMoved(board))
            {
                AddSquare(ref squares, GetOneRowUp(rowNum), square.Col, ref square, ref board);
            }

            return squares;
        }

        private void TakeOpponentPiece(ref Board board, ref Square square, ref List<Square> squares)
        {
            int rowNum = GetOneRowUp(square.Row);
            int colNum = square.Col + 1;
            if (CheckValidSquare(rowNum, colNum))
            {
                var piece = board.GetPiece(new Square(rowNum, colNum));
                if (piece != null && piece.Player != this.Player)
                {
                    AddSquare(ref squares, rowNum, colNum, ref square, ref board);
                }
            }
            colNum = square.Col - 1;
            if (CheckValidSquare(rowNum, colNum))
            {
                var piece = board.GetPiece(new Square(rowNum, colNum));
                if (piece != null && piece.Player != this.Player)
                {
                    AddSquare(ref squares, rowNum, colNum, ref square, ref board);
                }
            }
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
        private bool HasNotMoved(Board board)
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

            
            if (CheckValidSquare(rowNum, colNum))
            {
                squares.Add(new Square(rowNum, colNum));
            }
        }
    }
}