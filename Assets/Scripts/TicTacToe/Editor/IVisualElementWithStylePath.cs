using UnityEngine.UIElements;

namespace TicTacToe.Editor {
    public interface IVisualElementWithStylePath {
        string StylePath { get; }

        void AddStyle(StyleSheet styleSheet);

    }
}