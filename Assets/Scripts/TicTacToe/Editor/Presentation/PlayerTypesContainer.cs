using System;
using TicTacToe.Editor.Domain;
using TicTacToe.Editor.Utils;
using UnityEngine.UIElements;

namespace TicTacToe.Editor.Presentation {
    public class PlayerTypesContainer : VisualElement {
        public event Action<PlayerType> PlayerXTypeChanged;
        public event Action<PlayerType> PlayerOTypeChanged;

        public PlayerTypesContainer(PlayerType playerXType, PlayerType playerOType) {
            StyleHelperMethods.SetStyleFromPath(this, "PlayerTypesContainer");
            this.AddToClassList("player-types-container");

            var playerXTypeElement = new PlayerTypeElement(Symbol.X, playerXType);
            var playerOTypeElement = new PlayerTypeElement(Symbol.O, playerOType);

            playerXTypeElement.ValueChanged += OnPlayerXTypeChanged;
            playerOTypeElement.ValueChanged += OnPlayerOTypeChanged;

            this.Add(playerXTypeElement);
            this.Add(playerOTypeElement);
        }

        private void OnPlayerXTypeChanged(PlayerType newPlayerType) =>
            PlayerXTypeChanged?.Invoke(newPlayerType);


        private void OnPlayerOTypeChanged(PlayerType newPlayerType) =>
            PlayerOTypeChanged?.Invoke(newPlayerType);
    }
}