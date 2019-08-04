
using BattleCards.Cards;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BattleCards.Battle
{
	public static class CardBattleFunctions
	{
		public enum Direction
		{
			North,
			South,
			East,
			West,
		}

		private class BattleGroup
		{
			public BattleCard Attacker;
			public int AttackerPower;
			public int AttackerRange;
			public Direction AttackerDirection;
			
			public int Row => Attacker.Row;
			public int Column => Attacker.Row;

			public bool CheckPosition(int row, int column)
			{
				var value = false;
				if (Attacker != null)
					value = Attacker.Row == row && Attacker.Column == column;

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
				return;
			}
			else
			{
				_battleGroup.Add(new BattleGroup()
				{
					Attacker = card,
					AttackerPower = card.Power,
					AttackerRange = card.Range,
					AttackerDirection = card.Direction,
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
			foreach(var bg in _battleGroup)
			{
				var victims = new List<BattleCard>();
				if (bg.AttackerDirection == Direction.North)
				{
					victims.Add(Field.GetCard(bg.Attacker.Row - bg.AttackerRange, bg.Attacker.Column));
				} else if (bg.AttackerDirection == Direction.South)
				{
					victims.Add(Field.GetCard(bg.Attacker.Row + bg.AttackerRange, bg.Attacker.Column));
				}

				foreach(var victim in victims)
				{
					victim?.SetPower(victim.Power - bg.AttackerPower);
				}
			}
		}

		private static void ResultBattle()
		{
			foreach (var bg in _battleGroup)
			{
				if(bg.Attacker.Power <= 0)
				{
					Field.RemoveCard(bg.Attacker.Row, bg.Attacker.Column);
					GameObject.DestroyImmediate(bg.Attacker.gameObject);
				}
			}
		}
	}
}
