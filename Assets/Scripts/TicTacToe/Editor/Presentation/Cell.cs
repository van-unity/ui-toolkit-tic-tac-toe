using TicTacToe.Editor.Utils;
using UnityEngine.UIElements;

namespace TicTacToe.Editor.Presentation {
    public class Cell : VisualElement {
        public Cell(int symbolSize, string symbol) {
            StyleHelperMethods.SetStyleFromPath(this, "Cell");
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