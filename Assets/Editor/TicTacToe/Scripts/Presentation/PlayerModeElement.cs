using Editor.TicTacToe.Scripts.Presentation.VisualElementExtensions;
using UnityEngine.UIElements;

namespace Editor.TicTacToe.Scripts.Presentation {
    public class PlayerModeElement : VisualElement {
        private readonly Label _symbolLabel;
        private readonly Label _modeLabel;

        public PlayerModeElement(IStyleSettings styleSettings) {
            this.SetStyle(styleSettings.PlayerModeStyle);
            this.AddToClassList("player-mode-element");

            _symbolLabel = new Label();
            _symbolLabel.AddToClassList("symbol-label");

            _modeLabel = new Label();
            _modeLabel.AddToClassList("mode-label");
            
            this.Add(_symbolLabel);
            this.Add(_modeLabel);
            
            this.RegisterCallback<ClickEvent>(OnClick);
        }

        private void OnClick(ClickEvent evt) {
            
        }

        public void SetSymbol(string symbol) {
            _symbolLabel.text = symbol;
        }

        public void SetMode(string mode) {
            _modeLabel.text = mode;
        }
    }
}