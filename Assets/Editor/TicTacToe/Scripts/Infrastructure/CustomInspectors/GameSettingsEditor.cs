using System;
using Editor.TicTacToe.Scripts.Domain;
using Editor.TicTacToe.Scripts.Presentation.VisualElementExtensions;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.TicTacToe.Scripts.Infrastructure.CustomInspectors {
    [CustomEditor(typeof(GameSettings))]
    public class GameSettingsEditor : UnityEditor.Editor {
        private IAssetLoader _assetLoader;

        private void OnEnable() {
            _assetLoader = new AssetDatabaseAssetLoader();
        }

        public override VisualElement CreateInspectorGUI() {
            var root = new VisualElement();
            root.SetStyle(_assetLoader.LoadAsset<StyleSheet>("GameSettings"));
            root.AddToClassList("game-settings-editor");

            var title = new Label("TicTacToe GameSettings");
            title.AddToClassList("title");
            title.AddToClassList("bottom-line");

            var sizeField = new IntegerField() {
                label = "Board Size",
                bindingPath = "_boardSize",
            };
            sizeField.RegisterValueChangedCallback(OnBoardSizeChanged);
            sizeField.AddToClassList("size-field");
            sizeField.AddToClassList("bottom-line");

            var playerXMode = new EnumField(PlayerMode.Manual) {
                label = "Player X Mode",
                bindingPath = "_playerXMode"
            };
            playerXMode.AddToClassList("player-mode");

            var playerOMode = new EnumField(PlayerMode.Auto) {
                label = "Player O Mode",
                bindingPath = "_playerOMode"
            };
            playerOMode.AddToClassList("player-mode");

            var playerModesContainer = new VisualElement();
            playerModesContainer.Add(playerXMode);
            playerModesContainer.Add(playerOMode);
            playerModesContainer.AddToClassList("player-modes-container");
            playerModesContainer.AddToClassList("bottom-line");

            var automatedPlayerDelayField = new UnsignedIntegerField() {
                label = "Automated player delay in milliseconds",
                bindingPath = "_automatedPlayerDelayMS",
            };
            automatedPlayerDelayField.AddToClassList("automated-player-delay-field");
            automatedPlayerDelayField.AddToClassList("bottom-line");

            root.Add(title);
            root.Add(sizeField);
            root.Add(playerModesContainer);
            root.Add(automatedPlayerDelayField);
            return root;
        }

        private void OnBoardSizeChanged(ChangeEvent<int> evt) {
            if (evt.target is IntegerField field) {
                field.value = Mathf.Max(evt.newValue, 3);
            }
        }
    }
}