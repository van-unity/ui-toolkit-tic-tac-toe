using TicTacToe.Editor.VisualElementExtensions;
using UnityEngine.UIElements;

namespace TicTacToe.Editor.Presentation {
    public class Cell : VisualElement {
        public Cell(int symbolSize, string symbol) {
            this.SetStyleFromPath("Cell");
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