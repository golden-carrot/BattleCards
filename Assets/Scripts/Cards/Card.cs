using BattleCards.Cards.CardActions;
using System.Collections.Generic;
using UnityEngine;

namespace BattleCards.Cards
{
	public class Card : MonoBehaviour
	{
		public string Id => _id;
		[SerializeField] private string _id;

		public List<CardAction> ActionList => _actionList;
		[SerializeField] List<CardAction> _actionList;
	}
}
