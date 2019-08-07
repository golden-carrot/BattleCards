using UnityEngine;
using static BattleCards.Battle.CardBattleFunctions;

namespace BattleCards.Cards
{
	public class BattleCard : Card
	{
		public int Power => _power;
		[SerializeField] private int _power = 0;

		public int Attack => _attack;
		[SerializeField] private int _attack = 0;

		public int Health => _health;
		[SerializeField] private int _health = 0;

		public int Range => _range;
		[SerializeField] private int _range = 0;

		public int Row { get; set; }
		public int Column { get; set; }
		public Team Team { get; set; }

		private CardFrame _cardFrame;

		protected override void Awake()
		{
			base.Awake();

			var cardFrameObject = Instantiate(Resources.Load<GameObject>("Etc/Card Frame"));
			if (cardFrameObject == null) return;

			cardFrameObject.transform.parent = this.transform;
			cardFrameObject.transform.localPosition = Vector3.zero;
			cardFrameObject.transform.localScale = Vector3.one;
			
			_cardFrame = cardFrameObject.GetComponent<CardFrame>();
			if (_cardFrame != null) {
				_cardFrame.Init(this);
			}
		}

		public void SetPower(int power)
		{
			_power = power;
			if (_cardFrame != null) _cardFrame.UpdateData();
		}
	}
}
