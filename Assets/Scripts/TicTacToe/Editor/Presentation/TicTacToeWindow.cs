using System;
using TicTacToe.Editor.Application;
using TicTacToe.Editor.Domain;
using TicTacToe.Editor.Utils;
using UnityEngine.UIElements;

namespace TicTacToe.Editor.Presentation {
    public class TicTacToeWindow : UnityEditor.EditorWindow {
        public event Action<Symbol, PlayerType> PlayerTypeChanged;

        private void CreateGUI() {
            // StyleHelperMethods.SetStyleFromPath(rootVisualElement, "TicTacToeWindow");
            var restartButton = new Button(() => {
                Main.Restart();
                rootVisualElement.Clear();
                CreateGUI();
            }) { text = "Restart Game" };


            var playerTypesContainer = new PlayerTypesContainer(
                Main.PlayerTypeBySymbol[Symbol.X],
                Main.PlayerTypeBySymbol[Symbol.O]
            );

            playerTypesContainer.PlayerXTypeChanged += OnPlayerXTypeChanged;
            playerTypesContainer.PlayerOTypeChanged += OnPlayerOTypeChanged;

            rootVisualElement.Add(playerTypesContainer);
            rootVisualElement.Add(Main.BoardView);
            rootVisualElement.Add(restartButton);
        }

        private void OnPlayerXTypeChanged(PlayerType newPlayerType) {
            PlayerTypeChanged?.Invoke(Symbol.X, newPlayerType);
        }

        private void OnPlayerOTypeChanged(PlayerType newPlayerType) {
            PlayerTypeChanged?.Invoke(Symbol.O, newPlayerType);
        }
    }
}