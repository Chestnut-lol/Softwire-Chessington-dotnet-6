using System.Collections.Generic;
using System.Linq;

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
            for (int i = 0; i < 8; i++)
            {
                if (i == square.Col)
                {
                    ;
                }
                else
                {
                    squares.Add(new Square(square.Row, i));
                }
            }
            for (int i = 0; i < 8; i++)
            {
                if (i == square.Row)
                {
                    ;
                }
                else
                {
                    squares.Add(new Square(i, square.Col));
                }
            }
            return squares;
        }
    }
}