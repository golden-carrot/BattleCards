using BattleCards.Cards;
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

		public static bool HasCard(int row, int column)
		{
			return _fieldCards.Any(card => card.Row == row && card.Column == column);
		}

		public static BattleCard GetFirstCardInRange(int row, int column, int range, CardBattleFunctions.Team team)
		{
			if (range <= 0)
				return null;

			var deltaValue = team == CardBattleFunctions.Team.My ? -1 : 1;
			var checkRange = 0;
			var tempRow = row;
			BattleCard target = null;

			do
			{
				++checkRange;
				tempRow += deltaValue;
				target = GetCard(tempRow, column);
				if (target != null && target.Team == team)
					target = null;
			} while (target == null && checkRange <= range);

			return target;
		}
	}
}