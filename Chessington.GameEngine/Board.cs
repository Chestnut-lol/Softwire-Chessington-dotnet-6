using System;
using System.Collections.Generic;
using System.Windows;
using Chessington.GameEngine.Pieces;

namespace Chessington.GameEngine
{
    public class Board
    {
        private readonly Piece[,] _board;
        public Player CurrentPlayer { get; private set; }
        public IList<Piece> CapturedPieces { get; private set; } 
        
        public Move LastMove { get; set; }

        public Board()
            : this(Player.White) { }

        public Board(Player currentPlayer, Piece[,] boardState = null)
        {
            _board = boardState ?? new Piece[GameSettings.BoardSize, GameSettings.BoardSize]; 
            CurrentPlayer = currentPlayer;
            CapturedPieces = new List<Piece>();
        }

        public void AddPiece(Square square, Piece pawn)
        {
            _board[square.Row, square.Col] = pawn;
        }
    
        public Piece GetPiece(Square square)
        {
            return _board[square.Row, square.Col];
        }
        
        public Square FindPiece(Piece piece)
        {
            for (var row = 0; row < GameSettings.BoardSize; row++)
                for (var col = 0; col < GameSettings.BoardSize; col++)
                    if (_board[row, col] == piece)
                        return Square.At(row, col);

            throw new ArgumentException("The supplied piece is not on the board.", "piece");
        }

        public void MovePiece(Square from, Square to)
        {
            var movingPiece = _board[from.Row, from.Col];
            if (movingPiece == null) { return; }
            //Handle special cases
            if (movingPiece.GetType() == typeof(Pawn) && (from.Col!=to.Col))
            {
                EnPasse(from, to);
            }

            if (movingPiece.GetType() == typeof(King) && (Math.Abs(from.Col - to.Col) == 2))
            {
                ;
                Castling(from, to);
            }
            

            if (movingPiece.Player != CurrentPlayer)
            {
                throw new ArgumentException("The supplied piece does not belong to the current player.");
            }

            //If the space we're moving to is occupied, we need to mark it as captured.
            if (_board[to.Row, to.Col] != null)
            {
                OnPieceCaptured(_board[to.Row, to.Col]);
            }

            //Move the piece and set the 'from' square to be empty.
            _board[to.Row, to.Col] = _board[from.Row, from.Col];
            _board[from.Row, from.Col] = null;

            CurrentPlayer = movingPiece.Player == Player.White ? Player.Black : Player.White;
            OnCurrentPlayerChanged(CurrentPlayer);
            SetLastMove(movingPiece, from, to);
        }

        private void Castling(Square from, Square to)
        {
            int rookFromCol;
            int rookToCol;
            if (to.Col == 6) // small castle
            {
                rookFromCol = 7;
                rookToCol = 5;
            }
            else if (to.Col == 2) // big castle
            {
                rookFromCol = 0;
                rookToCol = 3;
            }
            else
            {
                throw new Exception("Bug in Board.Castling!");
            }

            _board[from.Row, rookFromCol].Moved = true; // Set rook as moved.
            _board[to.Row, rookToCol] = _board[from.Row, rookFromCol];
            _board[from.Row, rookFromCol] = null;
        }

        private void EnPasse(Square from, Square to)
        {
            var deletingPiece = _board[from.Row, to.Col];
            OnPieceCaptured(_board[from.Row, to.Col]);
            _board[from.Row, to.Col] = null;
        }
        
        private void SetLastMove(Piece piece, Square from, Square to)
        {
            Move move = new Move(piece, from, to);
            LastMove = move;
        }
        
        public delegate void PieceCapturedEventHandler(Piece piece);
        
        public event PieceCapturedEventHandler PieceCaptured;

        protected virtual void OnPieceCaptured(Piece piece)
        {
            var handler = PieceCaptured;
            handler?.Invoke(piece);
        }

        public delegate void CurrentPlayerChangedEventHandler(Player player);

        public event CurrentPlayerChangedEventHandler CurrentPlayerChanged;

        protected virtual void OnCurrentPlayerChanged(Player player)
        {
            var handler = CurrentPlayerChanged;
            handler?.Invoke(player);
        }
    }
}
