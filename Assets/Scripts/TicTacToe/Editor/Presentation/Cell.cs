using TicTacToe.Editor.VisualElementExtensions;
using UnityEngine.UIElements;

namespace TicTacToe.Editor.Presentation {
    public class Cell : VisualElement {
        public Cell(int symbolSize, string symbol, IStyleSettings styleSettings) {
            this.SetStyleFromPath(styleSettings.CellStyle);
            this.AddToClassList("cell");
            var label = new Label(symbol) {
                style = {
                    fontSize = symbolSize
                }
            };

            this.Add(label);
        }
    }
}