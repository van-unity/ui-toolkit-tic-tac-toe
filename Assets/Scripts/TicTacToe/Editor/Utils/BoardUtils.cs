using UnityEngine;

namespace TicTacToe.Editor.Utils {
    public static class BoardUtils {
        public static Vector2 IndexToPixelPos(Vector2 index, float cellWidth, float cellHeight) =>
            new Vector2(index.x * cellHeight, index.y * cellWidth);
    }
}