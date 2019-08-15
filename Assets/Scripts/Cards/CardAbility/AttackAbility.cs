using System.Collections;
using BattleCards.Battle;
using static BattleCards.Battle.CardBattleFunctions;

namespace BattleCards.Cards.CardAbility
{
	public class AttackAbility : CardAbility {
		protected bool _successAttack = false;
		
		public override IEnumerator Action(BattleFunctionData data) {
			_successAttack = false;
			var target = Field.GetFirstCardInRange(data.Row, data.Column, data.Range, data.Team);
			if(target != null)
			{
				yield return data.Card.PlayAttackAnimation(data.Team);
				CardBattleFunctions.Instance.DecreaseDataHealth(target.Row, target.Column, data.Attack);
				_successAttack = true;
			}
		}
	}
}
