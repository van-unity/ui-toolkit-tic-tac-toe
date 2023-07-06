using UnityEngine.UIElements;

namespace TicTacToe.Editor.Presentation {
    public class XSymbol : Label {
        public XSymbol(int size, float width, float height): base("X"){
            this.style.fontSize = size;
            this.style.width = width;
            this.style.height = height;
        }
    }
}