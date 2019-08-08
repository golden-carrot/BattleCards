using BattleCards.Battle;
using BattleCards.System;
using UnityEngine;

namespace BattleCards.Cards.CardAbility
{
	public class RemainCardOnDeathAbility : CardAbility
	{
		[SerializeField] private string _targetCardId;

		public override void Action(CardBattleFunctions.BattleFunctionData my) {
			if (my.Health != 0)
				return;
			
			var _fieldGrid = FindObjectOfType<FieldGrid>();
			var newInstance = Instantiate(Resources.Load<GameObject>($"Cards/{_targetCardId}"));
			var pivot = _fieldGrid.GetPivot(my.Card.Row, my.Card.Column);

			if (newInstance != null && pivot != null)
			{
				newInstance.transform.parent = pivot;
				newInstance.transform.localPosition = Vector3.zero;
				newInstance.transform.localScale = Vector3.one;
				newInstance.transform.localRotation = Quaternion.identity;

				var fieldGridItem = pivot.gameObject.GetComponent<FieldGridItem>();
				var battleCard = newInstance.GetComponent<BattleCard>();
				battleCard.Row = fieldGridItem.Row;
				battleCard.Column = fieldGridItem.Column;
				battleCard.Team = fieldGridItem.Direction;

				Field.AddCard(battleCard);
			}
		}
	}
}