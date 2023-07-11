using System;
using TicTacToe.Editor.Domain;
using UnityEngine;

namespace TicTacToe.Editor.Application {
    public class GameController : IDisposable {
        private readonly IPlayer[] _players;
        private readonly BoardController _boardController;

        private int _currentPlayerIndex;
        private bool _canPlay;

        public GameController(IPlayer[] players, BoardController boardController) {
            _players = new IPlayer[players.Length];
            Array.Copy(players, _players, players.Length);
            _currentPlayerIndex = 0;
            _boardController = boardController;
            _boardController.CellClicked += OnCellClicked;
            _boardController.Win += OnWin;
            _boardController.Draw += OnDraw;
        }

        private void PlayerOnMoved(PlayerMovedResult result) {
            _boardController.UpdateCell(result.MovePosition, result.Symbol);
            _canPlay = true;
            _currentPlayerIndex = _currentPlayerIndex == 0 ? 1 : 0;
            if (_players[_currentPlayerIndex].PlayerType == PlayerType.Auto) {
                _players[_currentPlayerIndex].Move(BoardPosition.Empty(), PlayerOnMoved);
            }
        }

        private void OnDraw() {
            // handle draw
            Debug.LogError("draw");
        }

        private void OnWin(Win obj) {
            // handle win
            Debug.LogError("win");
        }

        public void Start() {
            _canPlay = true;
            if (_players[_currentPlayerIndex].PlayerType == PlayerType.Auto) {
                _canPlay = false;
                _players[_currentPlayerIndex].Move(BoardPosition.Empty(), PlayerOnMoved);
            }
        }

        private void OnCellClicked(BoardPosition cellPosition) {
            if (!_canPlay || _players[_currentPlayerIndex].PlayerType != PlayerType.Manual) {
                return;
            }

            _canPlay = false;
            _players[_currentPlayerIndex].Move(cellPosition, PlayerOnMoved);
        }

        public void Dispose() {
            _boardController.CellClicked -= OnCellClicked;
            _boardController.Win -= OnWin;
            _boardController.Draw -= OnDraw;
        }
    }
}