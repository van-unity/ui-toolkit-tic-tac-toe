using System;
using TicTacToe.Editor.Domain;
using TicTacToe.Editor.VisualElementExtensions;
using UnityEngine;
using UnityEngine.UIElements;

namespace TicTacToe.Editor.Presentation {
    public class StartButtonClickEvent : EventBase<StartButtonClickEvent> {
        public StartButtonClickEvent(IEventHandler target) {
            this.target = target;
        }

        public StartButtonClickEvent() {
        }
    }

    public class RestartButtonClicked : EventBase<RestartButtonClicked> {
        public RestartButtonClicked(IEventHandler target) {
            this.target = target;
        }

        public RestartButtonClicked() {
        }
    }

    public class PlayerModeClickedEvent : EventBase<PlayerModeClickedEvent> {
        public Symbol Symbol { get; }

        public PlayerModeClickedEvent(Symbol symbol, IEventHandler target) {
            Symbol = symbol;
            this.target = target;
        }

        public PlayerModeClickedEvent() {
            Symbol = Symbol.Empty;
        }
    }

    public class GameScreen : VisualElement {
        private readonly Button _startButton;
        private readonly Button _restartButton;
        private readonly Label _turnLabel;
        private readonly VisualElement _bottomPart;
        private readonly BoardView _boardView;
        private readonly PlayerModeElement _playerXMode;
        private readonly PlayerModeElement _playerOMode;

        public GameScreen(BoardView boardView) {
            this.SetStyleFromPath("GameScreen");
            this.AddToClassList("game-screen");

            _boardView = boardView;

            var topPart = new VisualElement();
            topPart.AddToClassList("top-part");
            this.Add(topPart);

            var gridPart = new VisualElement();
            gridPart.AddToClassList("grid-part");
            this.Add(gridPart);

            gridPart.Add(_boardView);


            _bottomPart = new VisualElement();
            _bottomPart.AddToClassList("bottom-part");
            this.Add(_bottomPart);

            var playerModesContainer = new VisualElement();
            playerModesContainer.AddToClassList("player-modes-container");

            _playerXMode = new PlayerModeElement();
            _playerXMode.SetSymbol(Symbol.X.ToString());

            _playerOMode = new PlayerModeElement();
            _playerOMode.SetSymbol(Symbol.O.ToString());

            _playerXMode.RegisterCallback<ClickEvent>(OnPlayerXTypeClicked);
            _playerOMode.RegisterCallback<ClickEvent>(OnPlayerOTypeClicked);

            playerModesContainer.Add(_playerXMode);
            playerModesContainer.Add(_playerOMode);
            topPart.Add(playerModesContainer);


            _turnLabel = new Label();
            _turnLabel.AddToClassList("turn-label");
            topPart.Add(_turnLabel);

            _startButton = new Button(OnStartButtonClick) {
                text = "Start"
            };
            _startButton.AddToClassList("button");

            _restartButton = new Button(OnRestartButtonClick) {
                text = "Restart"
            };
            _restartButton.AddToClassList("button");
        }

        private void OnPlayerXTypeClicked(ClickEvent clickEvent) {
            clickEvent.PreventDefault();
            var playerXTypeClickedEvent = new PlayerModeClickedEvent(Symbol.X, this);
            this.SendEvent(playerXTypeClickedEvent);
        }

        private void OnPlayerOTypeClicked(ClickEvent clickEvent) {
            var playerOTypeClickedEvent = new PlayerModeClickedEvent(Symbol.O, this);
            this.SendEvent(playerOTypeClickedEvent);
        }

        public void SetPlayerXMode(string playerXType) {
            _playerXMode.SetMode(playerXType);
        }

        public void SetPlayerOMode(string playerOType) {
            _playerOMode.SetMode(playerOType);
        }

        public void UpdateTurnLabel(string turnText) {
            _turnLabel.text = turnText;
        }

        public void SetStartButtonEnabled(bool isEnabled) {
            if (isEnabled) {
                _bottomPart.Add(_startButton);
                return;
            }

            if (_bottomPart.Contains(_startButton)) {
                _bottomPart.Remove(_startButton);
            }
        }

        public void SetReStartButtonEnabled(bool isEnabled) {
            if (isEnabled) {
                _bottomPart.Add(_restartButton);
                return;
            }

            _bottomPart.Remove(_restartButton);
        }

        private void OnStartButtonClick() {
            var clickEvent = new StartButtonClickEvent(this);
            this.SendEvent(clickEvent);
        }

        private void OnRestartButtonClick() {
            var clickEvent = new RestartButtonClicked(this);
            this.SendEvent(clickEvent);
        }

        public void InitializeBoard() {
            _boardView.Initialize();
        }

        public void ClearTheBoard() {
            _boardView.Reset();
        }
    }
}