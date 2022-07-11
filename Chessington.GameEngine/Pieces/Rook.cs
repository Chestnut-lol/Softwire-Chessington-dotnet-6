using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Animation;

namespace Chessington.GameEngine.Pieces
{
    public class Rook : Piece
    {
        public Rook(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            Square square = board.FindPiece(this);
            List<Square> squares = new List<Square>();
            
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
            
            return squares;
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
        private bool CheckValidSquare(int rowNum, int colNum)
        {
            return (rowNum >= 0 && rowNum < 8 && colNum >= 0 && colNum < 8);
        }
        private void AddSquare(ref List<Square> squares, int rowNum, int colNum, ref Square currentSquare)
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