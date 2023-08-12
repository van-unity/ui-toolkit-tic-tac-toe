using UnityEngine.UIElements;

namespace TicTacToe.Presentation.VisualElementExtensions {
    public static class StyleExtensions {
        public static void SetStyle(this VisualElement element, StyleSheet styleSheet) {
            element.styleSheets.Clear();
            element.styleSheets.Add(styleSheet);
        }

        public static void AddToStyle(this VisualElement element, StyleSheet styleSheet) {
            element.styleSheets.Add(styleSheet);
        }
    }
}