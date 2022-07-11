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
            var squares = new List<Square>() { new Square(square.Row - 1, 0) };
            //return Enumerable.Empty<Square>();
            return squares;
        }
    }
}