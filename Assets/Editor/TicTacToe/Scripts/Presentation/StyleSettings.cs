using Editor.TicTacToe.Scripts.Utils;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.TicTacToe.Scripts.Presentation {
    [CreateAssetMenu(fileName = "StyleSettings", menuName = MenuNames.SETTINGS + "/StyleSettings", order = 0)]
    public class StyleSettings : ScriptableObject, IStyleSettings {
        [SerializeField] private StyleSheet _rootStyle;
        [SerializeField] private StyleSheet _boardStyle;
        [SerializeField] private StyleSheet _cellStyle;
        [SerializeField] private StyleSheet _gameScreenStyle;
        [SerializeField] private StyleSheet _playerModeStyle;
        [SerializeField] private StyleSheet _popupsContainerStyle;
        [SerializeField] private StyleSheet _messageboxPopupStyle;

        public StyleSheet RootStyle => _rootStyle;
        public StyleSheet BoardStyle => _boardStyle;
        public StyleSheet CellStyle => _cellStyle;
        public StyleSheet GameScreenStyle => _gameScreenStyle;
        public StyleSheet PlayerModeStyle => _playerModeStyle;
        public StyleSheet PopupsContainerStyle => _popupsContainerStyle;
        public StyleSheet MessageboxPopupStyle => _messageboxPopupStyle;
    }
}