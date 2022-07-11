using Chessington.GameEngine.Pieces;

namespace Chessington.GameEngine;

public class Move
{
    public Piece Piece { get; set; }
    public Square FromSquare { get; set; }
    public Square ToSquare { get; set; }

    public Move(Piece piece, Square from, Square to)
    {
        Piece = piece;
        FromSquare = from;
        ToSquare = to;
    }
    
}