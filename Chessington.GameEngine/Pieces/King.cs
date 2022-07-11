using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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
                AddSquare(ref squares, rowNum, colNum, ref square, ref board);
                rowNum -= 1;
            }

            colNum = square.Col - 1;
            rowNum = square.Row + 1;
            for (int i = -1; i < 2; i++)
            {
                AddSquare(ref squares, rowNum, colNum, ref square, ref board);
                rowNum -= 1;
            }
            AddSquare(ref squares, square.Row + 1, square.Col, ref square, ref board);
            AddSquare(ref squares, square.Row - 1, square.Col, ref square, ref board);
            TryCastle(ref squares, ref board, ref square);
            return squares;
        }

        private void TryCastle(ref List<Square> squares, ref Board board, ref Square square)
        {
            if (this.Moved)
            {
                return;
            }

            int lastRow = GetLastRow();
            if (CanBigCaste(ref lastRow, ref board))
            {
                
                AddSquare(ref squares, lastRow, 2, ref square, ref board);
            };
            if (CanSmallCastle(ref lastRow, ref board))
            {
                AddSquare(ref squares, lastRow, 6, ref square, ref board);

            }
        }

        private bool CanSmallCastle(ref int lastRow, ref Board board)
        {
            var piece = board.GetPiece(Square.At(lastRow, 0));
            if (piece == null)
            {
                return false;} 
            if (piece.GetType() != typeof(Rook))
            {
                return false;
            }
            
            if (piece.Moved)
            {
                return false;
            }


            for (int i = 6; i > 4; i--)
            {
                if (board.GetPiece(Square.At(lastRow, i)) != null)
                {
                    return false;
                }
            }

            return true;
        }

        private bool CanBigCaste(ref int lastRow, ref Board board)
        {
            var piece = board.GetPiece(Square.At(lastRow, 0));
            if (piece == null)
            {
                return false;} 
            if (piece.GetType() != typeof(Rook))
            {
                return false;
            }
            
            if (piece.Moved)
            {
                return false;
            }

            for (int i = 1; i < 4; i++)
            {
                if (board.GetPiece(Square.At(lastRow, i)) != null)
                {
                    return false;
                }
            }
            

            return true;
        }

        private int GetLastRow()
        {
            if (this.Player == Player.White)
            {
                return 7;
            }
            else
            {
                return 0;
            }
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