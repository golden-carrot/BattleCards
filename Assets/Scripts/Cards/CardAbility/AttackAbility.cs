using BattleCards.Battle;
using UnityEngine;
using static BattleCards.Battle.CardBattleFunctions;

namespace BattleCards.Cards.CardAbility
{
	public class AttackAbility : CardAbility
	{
		public override void Action(BattleFunctionData data)
		{
			var target = Field.GetFirstCardInRange(data.Card.Row, data.Card.Column, data.Range, data.Team);
			if(target != null)
			{
				target.DecreaseHealth(data.Attack);
			}
		}
	}
}
