using System.Collections.Generic;
using UnityEngine;

namespace BattleCards.Cards
{
	public class Card : MonoBehaviour
	{
		public string Id => _id;
		[SerializeField] private string _id;

		private List<CardAbility.CardAbility> _actionList;

		protected virtual void Awake()
		{

		}
	}
}
