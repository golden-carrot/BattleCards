using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

		[SerializeField] private Animator _animator;

		public List<CardAbility.CardAbility> OnBattleAbility => _onBattleAbility;
		private List<CardAbility.CardAbility> _onBattleAbility;

		public List<CardAbility.CardAbility> PreBattleAbility => _preBattleAbility;
		private List<CardAbility.CardAbility> _preBattleAbility;

		public List<CardAbility.CardAbility> PostBattleAbility => _postBattleAbility;
		private List<CardAbility.CardAbility> _postBattleAbility;

		public int Row { get; set; }
		public int Column { get; set; }
		public Team Team { get; set; }

		private CardFrame _cardFrame;

		protected override void Awake()
		{
			base.Awake();

			var ability = GetComponents<CardAbility.CardAbility>().ToList();
			_onBattleAbility = new List<CardAbility.CardAbility>();
			_preBattleAbility = new List<CardAbility.CardAbility>();
			_postBattleAbility = new List<CardAbility.CardAbility>();
			ability.ForEach(ab =>
			{
				switch (ab.AbilityActionType)
				{
					case CardAbility.CardAbility.AbilityType.OnBattleAction:
						_onBattleAbility.Add(ab);
						break;
					case CardAbility.CardAbility.AbilityType.PreBattleAction:
						_preBattleAbility.Add(ab);
						break;
					case CardAbility.CardAbility.AbilityType.PostBattleAction:
						_postBattleAbility.Add(ab);
						break;
				}
			});

			var cardFrameObject = Instantiate(Resources.Load<GameObject>("Etc/Card Frame"));
			if (cardFrameObject == null) return;

			cardFrameObject.transform.parent = _animator != null ? _animator.gameObject.transform : this.transform;
			cardFrameObject.transform.localPosition = Vector3.zero;
			cardFrameObject.transform.localScale = Vector3.one;
			
			_cardFrame = cardFrameObject.GetComponent<CardFrame>();
			if (_cardFrame != null) {
				_cardFrame.Init(this);
			}
		}

		public void UpdateData(BattleFunctionData data) {
			Row = data.Row;
			Column = data.Column;
			_power = data.Power;
			_attack = data.Attack;
			_health = data.Health;
			_range = data.Range;
			Team = data.Team;
			
			_cardFrame.UpdateData();
		}

		public IEnumerator PlayAttackAnimation(Team team)
		{
			if (_animator == null)
				yield break;

			if(team == Team.My)
				_animator.Play("Attack_Up");
			else
				_animator.Play("Attack_Down");
			yield return new WaitForSeconds(GetCurrentAnimationLength());
		}

		private float GetCurrentAnimationLength()
		{
			_animator.Update(float.Epsilon);
			var info = _animator.GetCurrentAnimatorStateInfo(0);
			return info.length;
		}
	}
}
