using System.Collections;
using BattleCards.Battle;
using BattleCards.System;

namespace BattleCards.Cards.CardAbility {
    public class MarchAbility : CardAbility
    {
        public override IEnumerator Action(CardBattleFunctions.BattleFunctionData my) {
            var targetRow = 0;
            
            if (my.Team == CardBattleFunctions.Team.My) {
                targetRow = my.Row - 1;
            }
            else {
                targetRow = my.Row + 1;
            }

            if (Field.HasCard(targetRow, my.Column))
	            yield break;

            my.Row = targetRow;
            FieldGrid.Instance.MoveCardInstance(my.Card, my.Row, my.Column);
        }
    }
}

