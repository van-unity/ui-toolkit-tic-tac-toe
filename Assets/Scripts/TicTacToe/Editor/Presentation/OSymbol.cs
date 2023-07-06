using UnityEngine.UIElements;

namespace TicTacToe.Editor.Presentation {
    public class OSymbol : Label {
        public OSymbol(int size, float width, float height): base("O"){
            this.style.fontSize = size;
            this.style.width = width;
            this.style.height = height;
        }
    }
}