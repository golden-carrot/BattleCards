using UnityEngine;

namespace BattleCards.Cards
{
	public class BattleCard : Card
	{
		public int Power => _power;
		[SerializeField] private int _power = 0;

		public int Range => _range;
		[SerializeField] private int _range = 0;

		[SerializeField] private TextMesh _powerText;
	}
}
