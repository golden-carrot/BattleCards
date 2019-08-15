using System.Linq;

namespace BattleCards.Battle {
    public partial class CardBattleFunctions {

        public void DecreaseDataHealth(int row, int column, int value) {
            var targetData = _battleFunctionData.First(data => data.Row == row && data.Column == column);
            if (targetData != null)
                targetData.Health -= value;
            if (targetData.Health < 0)
                targetData.Health = 0;

            targetData.Card.UpdateData(targetData);
        }
    }
}
