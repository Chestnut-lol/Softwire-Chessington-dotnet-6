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
            squares.Add(new Square(rowNum,square.Col) );
            if (HasMoved(board))
            {
                squares.Add(new Square(GetOneRowUp(rowNum),square.Col));
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
    }
}