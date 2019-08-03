
using BattleCards.Cards;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BattleCards.Battle
{
	public static class CardBattleFunctions
	{
		private class BattleGroup
		{
			public BattleCard ACard;
			public BattleCard BCard;

			public int Row => ACard.Row;
			public int Column => BCard.Row;

			public bool CheckPosition(int row, int column)
			{
				var value = false;
				if (ACard != null)
					value = ACard.Row == row && ACard.Column == column;
				if (BCard != null)
					value &= BCard.Row == row && BCard.Column == column;

				return value;
			}
		}

		private static List<BattleGroup> _battleGroup;
		private static void ResetBattlePair()
		{
			if (_battleGroup == null)
				_battleGroup = new List<BattleGroup>();

			_battleGroup.Clear();
		}

		private static void AddCardToPair(BattleCard card)
		{
			if(_battleGroup.Any(pair => pair.CheckPosition(card.Row, card.Column)))
			{
				var pair = _battleGroup.First<BattleGroup>(p => p.CheckPosition(card.Row, card.Column));
				pair.BCard = card;
			}
			else
			{
				_battleGroup.Add(new BattleGroup()
				{
					ACard = card,
				});
			}
		}

		public static void Battle(List<BattleCard> myCards, List<BattleCard> otherCards)
		{
			ResetBattlePair();
			myCards.ForEach(card =>
			{
				AddCardToPair(card);
			});
			otherCards.ForEach(card =>
			{
				AddCardToPair(card);
			});

			PlayBattleGroup();
		}

		private static void PlayBattleGroup()
		{
			foreach(var bg in _battleGroup)
			{
				var ACard = bg.ACard;
				var BCard = bg.BCard;

				var tempACardPower = ACard.Power;
				var tempBCardPower = BCard.Power;

				ACard.SetPower(ACard.Power - tempBCardPower);
				BCard.SetPower(BCard.Power - tempACardPower);

				if (ACard.Power <= 0)
					GameObject.DestroyImmediate(ACard.gameObject);
				if (BCard.Power <= 0)
					GameObject.DestroyImmediate(BCard.gameObject);
			}
		}
	}
}
