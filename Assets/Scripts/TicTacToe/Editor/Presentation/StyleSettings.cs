using UnityEngine;
using UnityEngine.UIElements;

namespace TicTacToe.Editor.Presentation {
    [CreateAssetMenu(fileName = "StyleSettings", menuName = "TicTacToe/StyleSettings", order = 0)]
    public class StyleSettings : ScriptableObject, IStyleSettings {
        [SerializeField] private StyleSheet _boardStyle;
        [SerializeField] private StyleSheet _cellStyle;
        [SerializeField] private StyleSheet _gameScreenStyle;
        [SerializeField] private StyleSheet _playerModeStyle;
        [SerializeField] private StyleSheet _popupsContainerStyle;
        [SerializeField] private StyleSheet _messageboxPopupStyle;

        public string BoardStyle => _boardStyle.name;
        public string CellStyle => _cellStyle.name;
        public string GameScreenStyle => _gameScreenStyle.name;
        public string PlayerModeStyle => _playerModeStyle.name;
        public string PopupsContainerStyle => _popupsContainerStyle.name;
        public string MessageboxPopupStyle => _messageboxPopupStyle.name;
    }
}