using TicTacToe.Domain;
using UnityEngine.UIElements;

namespace TicTacToe.Presentation.CustomEvents {
    public class PlayerModeClickedEvent : PointerEventBase<PlayerModeClickedEvent> {
        public PlayerSymbol PlayerSymbol { get; }

        public PlayerModeClickedEvent(PlayerSymbol playerSymbol, IEventHandler target){
            PlayerSymbol = playerSymbol;
            this.target = target;
        }

        public PlayerModeClickedEvent() {
            PlayerSymbol = PlayerSymbol.Empty;
        }
    }
}