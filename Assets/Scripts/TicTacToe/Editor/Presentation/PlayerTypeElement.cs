using System;
using TicTacToe.Editor.Domain;
using TicTacToe.Editor.Utils;
using UnityEngine.UIElements;

namespace TicTacToe.Editor.Presentation {
    public class PlayerTypeElement : VisualElement {
        private readonly Label _playerTypeButton;

        private PlayerType _playerType;

        public event Action<PlayerType> ValueChanged;


        public PlayerTypeElement(Symbol symbol, PlayerType playerType) {
            StyleHelperMethods.SetStyleFromPath(this, "PlayerTypeElement");
            this.AddToClassList("player-type-element");
            _playerType = playerType;

            var symbolLabel = new Label(symbol.ToString());
            symbolLabel.AddToClassList("symbol");

            _playerTypeButton = new Label(playerType.ToString());
            _playerTypeButton.AddToClassList("player-type-button");

            this.Add(symbolLabel);
            this.Add(_playerTypeButton);

            this.RegisterCallback<ClickEvent>(_ => OnPlayerTypeButtonClick());
        }

        private void OnPlayerTypeButtonClick() {
            _playerType = _playerType switch {
                PlayerType.Auto => PlayerType.Manual,
                PlayerType.Manual => PlayerType.Auto,
                _ => throw new ArgumentOutOfRangeException()
            };

            _playerTypeButton.text = _playerType.ToString();

            ValueChanged?.Invoke(_playerType);
        }
    }
}