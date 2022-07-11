using System.Linq;
using Chessington.GameEngine.Pieces;
using FluentAssertions;
using NUnit.Framework;

namespace Chessington.GameEngine.Tests.Pieces
{
    [TestFixture]
    public class MorePawnTests{
        
        [Test]
        public void WhitePawns_EnPassant()
        {
            var board = new Board(Player.Black);
            var blackPawn = new Pawn(Player.Black);
            board.AddPiece(Square.At(1, 1), blackPawn);
            var whitePawn = new Pawn(Player.White);
            board.AddPiece(Square.At(3,2),whitePawn);
            board.MovePiece(Square.At(1,1),Square.At(3,1));
            var moves = whitePawn.GetAvailableMoves(board).ToList();
            moves.Should().Contain(Square.At(2, 1));
            //Assert.IsNull(board.GetPiece(Square.At(3, 1)), "Pawn not taken, en passe failed.");
        }
    }
}