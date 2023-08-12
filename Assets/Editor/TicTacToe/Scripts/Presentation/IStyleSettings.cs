using UnityEngine.UIElements;

namespace Editor.TicTacToe.Scripts.Presentation {
    public interface IStyleSettings {
        StyleSheet RootStyle { get; }
        StyleSheet BoardStyle { get; }
        StyleSheet CellStyle { get; }
        StyleSheet GameScreenStyle { get; }
        StyleSheet PlayerModeStyle { get; }
        StyleSheet PopupsContainerStyle { get; }
        StyleSheet MessageboxPopupStyle { get; }
    }
}