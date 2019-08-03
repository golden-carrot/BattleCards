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

		public int Row { get; set; }
		public int Column { get; set; }

		protected override void Awake()
		{
			base.Awake();
			
			if (_powerText != null)
				_powerText.text = Power.ToString();
		}

		public void SetPower(int power)
		{
			_power = power;
			if (_powerText != null)
				_powerText.text = Power.ToString();
		}
	}
}
