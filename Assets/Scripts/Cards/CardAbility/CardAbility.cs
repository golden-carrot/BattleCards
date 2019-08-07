using UnityEngine;
using static BattleCards.Battle.CardBattleFunctions;

namespace BattleCards.Cards.CardAbility
{
	public abstract class CardAbility : MonoBehaviour
	{
		abstract public void Action(BattleFunctionData my);
	}
}
