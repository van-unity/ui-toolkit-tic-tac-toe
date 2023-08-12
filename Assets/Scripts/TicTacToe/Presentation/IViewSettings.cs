using UnityEngine;

namespace TicTacToe.Presentation {
    public interface IViewSettings {
        Vector2 WindowDimensions { get; }
        int BoardDrawDelayMS { get; }
    }
}