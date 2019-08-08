using System.Linq;

namespace BattleCards.Battle {
    public static partial class CardBattleFunctions {

        public static void DecreaseDataHealth(int row, int column, int value) {
            var targetData = _battleFunctionData.First(data => data.Row == row && data.Column == column);
            if (targetData != null)
                targetData.Health -= value;
            if (targetData.Health < 0)
                targetData.Health = 0;
        }
    }
}
