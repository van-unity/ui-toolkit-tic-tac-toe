using TicTacToe.Presentation.Core;
using TicTacToe.Utils;
using UnityEngine;

namespace TicTacToe.Infrastructure.Core {
    [CreateAssetMenu(fileName = "ViewSettings", menuName = MenuNames.SETTINGS + "/ViewSettings", order = 0)]
    public class ViewSettings : ScriptableObject, IViewSettings {
        [SerializeField] private int _boardDrawDelayMS = 1000;
        public int BoardDrawDelayMS => _boardDrawDelayMS;
    }
}