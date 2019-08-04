using BattleCards.Cards;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BattleCards.Battle
{
	public static class Field
	{
		public static List<BattleCard> FieldCards => _fieldCards;
		private static List<BattleCard> _fieldCards;
		
		public static void Init()
		{
			if (_fieldCards == null)
				_fieldCards = new List<BattleCard>();
		}

		public static void AddCard(BattleCard battleCard)
		{
			if (_fieldCards.Contains(battleCard))
				return;

			_fieldCards.Add(battleCard);
		}

		public static void RemoveCard(int row, int column)
		{
			var battleCard = _fieldCards.FirstOrDefault(card => card.Row == row && card.Column == column);
			if(battleCard != null)
			{
				_fieldCards.Remove(battleCard);
			}
		}

		public static BattleCard GetCard(int row, int column)
		{
			return _fieldCards.FirstOrDefault(card => card.Row == row && card.Column == column);
		}
	}
}