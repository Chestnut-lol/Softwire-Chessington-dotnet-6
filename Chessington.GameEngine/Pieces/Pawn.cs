using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(Player player) 
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var square = board.FindPiece(this);

            int rowNum;
            if (this.Player == Player.White)
            {
                rowNum = square.Row - 1;
            }
            else
            {
                rowNum = square.Row + 1;
            }
            List<Square> squares = new List<Square>(){ new Square(rowNum,0)  };
            return squares;
        }
    }
}