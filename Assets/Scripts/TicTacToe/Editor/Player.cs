using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

namespace TicTacToe.Editor {
    public enum PlayerSymbol {
        None = 0,
        X = 1,
        O = 2
    }

    public abstract class Player {
        protected readonly Board _board;
        protected readonly PlayerSymbol _symbol;

        public event Action<int, int, PlayerSymbol> MoveMade;

        protected Player(Board board, PlayerSymbol symbol) {
            _board = board;
            _symbol = symbol;
        }

        public abstract void Play();

        protected bool TryMakeMove(int rowIndex, int columnIndex) {
            if (_board.ValueAt(rowIndex, columnIndex) != PlayerSymbol.None) {
                // The cell is already occupied, return false
                return false;
            }

            _board.PlaceSymbolAt(_symbol, rowIndex, columnIndex);

            // Trigger the MoveMade event
            MoveMade?.Invoke(rowIndex, columnIndex, _symbol);

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

            TryMakeMove(logicPos.x, logicPos.y);

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
            var randomRowIndex = (int)Random.Range(0, _board.Rows);
            var randomColumnIndex = (int)Random.Range(0, _board.Columns);
            while (!TryMakeMove(randomRowIndex, randomColumnIndex)) {
                randomRowIndex = (int)Random.Range(0, _board.Rows);
                randomColumnIndex = (int)Random.Range(0, _board.Columns);
                await Task.Yield();
            }
        }
    }
}