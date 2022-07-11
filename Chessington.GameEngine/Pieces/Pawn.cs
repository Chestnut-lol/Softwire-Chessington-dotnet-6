using System;
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
            if (Utilities.CheckValidSquare(rowNum, colNum) && board.GetPiece(new Square(rowNum, colNum)) != null)
            {
                return squares;
            }
            Utilities.AddSquare(ref squares, rowNum, colNum, ref square);
            if (!this.Moved)
            {
                rowNum = GetOneRowUp(rowNum);
                if (Utilities.CheckValidSquare(rowNum, colNum) && board.GetPiece(new Square(rowNum, colNum)) == null)
                {
                    Utilities.AddSquare(ref squares, rowNum, colNum, ref square);
                }
            }

            if (board.LastMove == null)
            {
                return squares;
            }

            if (CheckLastMoveForEnPassant(ref board, ref square) && CanDoEnPassant(ref square))
            {
                var lastMove = board.LastMove;
                if (lastMove != null)
                {
                    DoEnPassant(lastMove, ref square, ref squares, ref board);
                }
                
            }

            return squares;
        }

        private bool CheckLastMoveForEnPassant(ref Board board, ref Square square)
        {
            Move lastMove = board.LastMove;
            if (lastMove.Piece.GetType() != typeof(Pawn))
            {
                return false;
            }

            if (Math.Abs(lastMove.ToSquare.Col - square.Col) != 1)
            {
                return false;
            }
            if (this.Player == Player.White)
            {
                return (lastMove.FromSquare.Row == 1) && (lastMove.ToSquare.Row == 3);
            }
            else
            {
                return (lastMove.FromSquare.Row == 6) && (lastMove.ToSquare.Row == 4);
            }
        }
        private bool CanDoEnPassant(ref Square square)
        {
            if (this.Player == Player.White)
            {
                return (square.Row == 3);
            }
            else
            {
                return (square.Row == 4);
            }
        }
        private void DoEnPassant(Move lastMove, ref Square square, ref List<Square> squares, ref Board board)
        {
            int colNum = lastMove.ToSquare.Col;
            int rowNum = lastMove.ToSquare.Row;
            rowNum = GetOneRowUp(rowNum);
            
            Utilities.AddSquare(ref squares, rowNum,colNum,ref square);
        }
        private void TakeOpponentPiece(ref Board board, ref Square square, ref List<Square> squares)
        {
            int rowNum = GetOneRowUp(square.Row);
            int colNum = square.Col + 1;
            if (Utilities.CheckValidSquare(rowNum, colNum))
            {
                var piece = board.GetPiece(new Square(rowNum, colNum));
                if (piece != null && piece.Player != this.Player)
                {
                    Utilities.AddSquare(ref squares, rowNum, colNum, ref square);
                }
            }
            colNum = square.Col - 1;
            if (Utilities.CheckValidSquare(rowNum, colNum))
            {
                var piece = board.GetPiece(new Square(rowNum, colNum));
                if (piece != null && piece.Player != this.Player)
                {
                    Utilities.AddSquare(ref squares, rowNum, colNum, ref square);
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
    }
}