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
            MoveLikeBishop(ref square, ref squares, ref board);
            MoveLikeRook(ref square, ref squares, ref board);
            return squares;
        }

        private void MoveLikeRook(ref Square square, ref List<Square> squares, ref Board board)
        {
            int rowNum = square.Row;
            int colNum = square.Col + 1;
            while (colNum < 8)
            {
                if (!CheckValidSquare(rowNum, colNum))
                {
                    continue;
                }
                if (board.GetPiece(new Square(rowNum, colNum))!=null)
                {
                    TakeOpponentPieces(ref squares, ref square, ref board, rowNum, colNum);
                    break;
                }
                AddSquare(ref squares, rowNum, colNum, ref square);
                colNum += 1;
            }

            colNum = square.Col - 1;
            while (colNum >= 0)
            {
                if (!CheckValidSquare(rowNum, colNum))
                {
                    continue;
                }
                if (board.GetPiece(new Square(rowNum, colNum))!=null)
                {
                    TakeOpponentPieces(ref squares, ref square, ref board, rowNum, colNum);
                    break;
                }
                AddSquare(ref squares, rowNum, colNum, ref square);
                colNum -= 1;
            }
            
            
            colNum = square.Col;
            rowNum = square.Row + 1;
            while (rowNum < 8)
            {
                if (!CheckValidSquare(rowNum, colNum))
                {
                    continue;
                }
                if (board.GetPiece(new Square(rowNum, colNum))!=null)
                {
                    TakeOpponentPieces(ref squares, ref square, ref board, rowNum, colNum);
                    break;
                }
                AddSquare(ref squares, rowNum, colNum, ref square);
                rowNum += 1;
            }
            rowNum = square.Row - 1;
            while (rowNum >= 0)
            {
                if (board.GetPiece(new Square(rowNum, colNum))!=null)
                {
                    TakeOpponentPieces(ref squares, ref square, ref board, rowNum, colNum);
                    break;
                }
                AddSquare(ref squares, rowNum, colNum, ref square);
                rowNum -= 1;
            }
        }
        private void MoveLikeBishop(ref Square square, ref List<Square> squares, ref Board board)
        {
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
                        TakeOpponentPieces(ref squares, ref square, ref board, rowUp, colNum);
                        rowUp = 8; // No more squares will be added along this line
                    }
                }
                if (CheckValidSquare(rowDown, colNum))
                {
                    if (board.GetPiece(new Square(rowDown, colNum)) != null)
                    {
                        TakeOpponentPieces(ref squares, ref square, ref board, rowDown, colNum);
                        rowDown = -1; // No more squares will be added along this line
                    }
                }
                AddSquare(ref squares,  rowUp,  colNum, ref square);
                AddSquare(ref squares,  rowDown,  colNum, ref square);
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
                        TakeOpponentPieces(ref squares, ref square, ref board, rowUp, colNum);
                        rowUp = 8; // No more squares will be added along this line
                    }
                }
                if (CheckValidSquare(rowDown, colNum))
                {
                    if (board.GetPiece(new Square(rowDown, colNum)) != null)
                    {
                        TakeOpponentPieces(ref squares, ref square, ref board, rowDown, colNum);
                        rowDown = -1; // No more squares will be added along this line
                    }
                }
                AddSquare(ref squares, rowUp,  colNum, ref square);
                AddSquare(ref squares,  rowDown,  colNum, ref square);
                colNum += 1;
            }
        }
        private bool CheckValidSquare(int rowNum, int colNum)
        {
            return (rowNum >= 0 && rowNum < 8 && colNum >= 0 && colNum < 8);
        }
        private void TakeOpponentPieces(ref List<Square> squares, ref Square square, ref Board board, int rowNum,
            int colNum)
        {
            var piece = board.GetPiece(new Square(rowNum, colNum));
            if (piece.Player != this.Player)
            {
                AddSquare(ref squares, rowNum, colNum, ref square);
            }
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
