namespace TicTacToe.Editor.Presentation {
    public interface IStyleSettings {
        string BoardStyle { get; }
        string CellStyle { get; }
        string GameScreenStyle { get; }
        string PlayerModeStyle { get; }
        string PopupsContainerStyle { get; }
        string MessageboxPopupStyle { get; }
    }
}