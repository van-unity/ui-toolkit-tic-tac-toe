using Editor.TicTacToe.Scripts.Presentation;
using Editor.TicTacToe.Scripts.Utils;
using UnityEngine;

namespace Editor.TicTacToe.Scripts.Infrastructure {
    [CreateAssetMenu(fileName = "ViewSettings", menuName = MenuNames.SETTINGS + "/ViewSettings")]
    public class ViewSettings : ScriptableObject, IViewSettings {
        [SerializeField] private Vector2 _windowDimensions = new(640, 960);
        [SerializeField] private int _boardDrawDelayMS = 1000;

        public Vector2 WindowDimensions => _windowDimensions;
       
        public int BoardDrawDelayMS => _boardDrawDelayMS;
    }
}