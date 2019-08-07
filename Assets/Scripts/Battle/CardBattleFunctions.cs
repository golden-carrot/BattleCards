
using BattleCards.Cards;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BattleCards.Battle
{
	public static class CardBattleFunctions
	{
		public enum Team
		{
			My,
			Other,
		}

		public class BattleFunctionData
		{
			public BattleCard Card;
			public int Power;
			public int Attack;
			public int Health;
			public int Range;
			public Team Team;
			
			public bool CheckPosition(int row, int column)
			{
				var value = false;
				if (Card != null)
					value = Card.Row == row && Card.Column == column;

				return value;
			}
		}

		private static List<BattleFunctionData> _battleFunctionData;

		private static void ResetBattlePair()
		{
			if (_battleFunctionData == null)
				_battleFunctionData = new List<BattleFunctionData>();

			_battleFunctionData.Clear();
		}

		private static void AddCardToPair(BattleCard card)
		{
			if(_battleFunctionData.Any(pair => pair.CheckPosition(card.Row, card.Column)))
			{
				return;
			}
			else
			{
				_battleFunctionData.Add(new BattleFunctionData()
				{
					Card = card,
					Power = card.Power,
					Attack = card.Attack,
					Health = card.Health,
					Range = card.Range,
					Team = card.Team,
				});
			}
		}

		public static void Battle()
		{
			ResetBattlePair();
			Field.FieldCards.ForEach(card =>
			{
				AddCardToPair(card);
			});

			PlayBattleGroup();
			ResultBattle();
		}

		private static void PlayBattleGroup()
		{
			foreach(var data in _battleFunctionData)
			{
				data.Card.OnBattleAbility.ForEach(ability =>
				{
					ability.Action(data);
				});
			}
		}

		private static void ResultBattle()
		{
			foreach (var bg in _battleFunctionData)
			{
				if(bg.Card.Health <= 0)
				{
					Field.RemoveCard(bg.Card.Row, bg.Card.Column);
					bg.Card.PostBattleAbility.ForEach(ability =>
					{
						ability.Action(bg);
					});
					GameObject.DestroyImmediate(bg.Card.gameObject);
				}
			}
		}
	}
}
