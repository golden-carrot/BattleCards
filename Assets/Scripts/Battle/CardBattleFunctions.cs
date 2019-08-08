
using BattleCards.Cards;
using System.Collections.Generic;
using System.Linq;
using BattleCards.Cards.CardAbility;
using UnityEngine;

namespace BattleCards.Battle
{
	public static partial class CardBattleFunctions
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
			public int Row;
			public int Column;
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
					Row = card.Row,
					Column = card.Column,
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
			foreach (var data in _battleFunctionData)
			{
				data.Card.PostBattleAbility.ForEach(ability =>
				{
					ability.Action(data);
				});

				data.Card.UpdateData(data);

				if (data.Card.Health > 0) continue;
				
				var undestructableAbility = data.Card.GetComponent<UndestructableAbility>();
				if (undestructableAbility != null) continue;
				
				Field.RemoveCard(data.Card.Row, data.Card.Column);
				GameObject.DestroyImmediate(data.Card.gameObject);
			}
		}
	}
}
