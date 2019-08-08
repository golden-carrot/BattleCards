using BattleCards.Battle;
using static BattleCards.Battle.CardBattleFunctions;

namespace BattleCards.Cards.CardAbility
{
	public class AttackAbility : CardAbility {
		protected bool _successAttack = false;
		
		public override void Action(BattleFunctionData data) {
			_successAttack = false;
			var target = Field.GetFirstCardInRange(data.Row, data.Column, data.Range, data.Team);
			if(target != null)
			{
				DecreaseDataHealth(target.Row, target.Column, data.Attack);
				_successAttack = true;
			}
		}
	}
}
