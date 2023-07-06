using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

namespace TicTacToe.Editor {
    public abstract class Player {
        protected readonly Board _board;
        protected readonly PlayerSymbol _symbol;

        public event Action<BoardPosition, PlayerSymbol> MoveMade;

        protected Player(Board board, PlayerSymbol symbol) {
            _board = board;
            _symbol = symbol;
        }

        public abstract void Play();

        protected bool TryMakeMove(BoardPosition position) {
            if (_board.ValueAt(position) != PlayerSymbol.None) {
                // The cell is already occupied, return false
                return false;
            }

            _board.PlaceSymbolAt(_symbol, position);

            // Trigger the MoveMade event
            MoveMade?.Invoke(position, _symbol);

            // The move was successful, return true
            return true;
        }
    }

    public class ManualPlayer : Player {
        public ManualPlayer(Board board, PlayerSymbol symbol) : base(board, symbol) {
        }

        public override void Play() {
            _board.RegisterCallback<ClickEvent>(OnBoardClick);
        }

        private void OnBoardClick(ClickEvent clickEvent) {
            var localPos = clickEvent.localPosition;
            var logicPos = _board.PixelToLogicPos(localPos);

            TryMakeMove(logicPos);

            _board.UnregisterCallback<ClickEvent>(OnBoardClick);
        }
    }

    public class AutomatedPlayer : Player {
        public AutomatedPlayer(Board board, PlayerSymbol symbol) : base(board, symbol) {
        }

        public override async void Play() {
            await PlayAsync();
        }

        private async Task PlayAsync() {
            await Task.Delay(300);
            var randomPos = new BoardPosition(Random.Range(0, _board.Rows), Random.Range(0, _board.Columns));
            while (!TryMakeMove(randomPos)) {
                randomPos = new BoardPosition(Random.Range(0, _board.Rows), Random.Range(0, _board.Columns));
                await Task.Yield();
            }
        }
    }
}