using UnityEngine;
using UnityEngine.UIElements;

namespace TicTacToe.Editor {
    [CreateAssetMenu(fileName = "TicTacToe", menuName = "Settings", order = 0)]
    public class TicTacToeSettings : ScriptableObject {
        public float _lineWidthAnimationDuration = .25f;
        public EasingMode _lineWidthEasingMode = EasingMode.EaseOut;
    }
}