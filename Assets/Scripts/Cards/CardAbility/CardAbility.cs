using System.Collections;
using UnityEngine;
using static BattleCards.Battle.CardBattleFunctions;

namespace BattleCards.Cards.CardAbility
{
	public abstract class CardAbility : MonoBehaviour
	{
		public enum AbilityType
		{
			OnBattleAction,
			PreBattleAction,
			PostBattleAction,
			Passive,
		}

		public AbilityType AbilityActionType => _abilityType;
		[SerializeField] protected AbilityType _abilityType;
		abstract public IEnumerator Action(BattleFunctionData my);
	}
}
