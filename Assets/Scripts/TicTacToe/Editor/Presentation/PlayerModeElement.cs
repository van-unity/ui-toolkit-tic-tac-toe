using TicTacToe.Editor.VisualElementExtensions;
using UnityEngine.UIElements;

namespace TicTacToe.Editor.Presentation {
    public class PlayerModeElement : VisualElement {
        private readonly Label _symbolLabel;
        private readonly Label _modeLabel;

        public PlayerModeElement() {
            this.SetStyleFromPath("PlayerModeElement");
            this.AddToClassList("player-mode-element");

            _symbolLabel = new Label();
            _symbolLabel.AddToClassList("symbol-label");

            _modeLabel = new Label();
            _modeLabel.AddToClassList("mode-label");
            
            this.Add(_symbolLabel);
            this.Add(_modeLabel);
        }
        
        public void SetSymbol(string symbol) {
            _symbolLabel.text = symbol;
        }

        public void SetMode(string mode) {
            _modeLabel.text = mode;
        }
    }
}