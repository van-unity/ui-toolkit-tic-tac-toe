using UnityEngine;

namespace Editor.TicTacToe.Scripts.Presentation {
    public interface IViewSettings {
        Vector2 WindowDimensions { get; }
        int BoardDrawDelayMS { get; }
    }
}