using UnityEngine;

namespace BattleCards.Cards {
    public class CardFrame : MonoBehaviour {
        [SerializeField] private TextMesh _powerText;
        [SerializeField] private TextMesh _attackText;
        [SerializeField] private TextMesh _healthText;

        private BattleCard _battleCard;

        public void Init(BattleCard battleCard) {
            _battleCard = battleCard;

            UpdateData();
        }

        public void UpdateData() {
            if (_battleCard == null)
                return;
            
            _powerText.text = _battleCard.Power > 0 ? _battleCard.Power.ToString() : "-";
            _attackText.text = _battleCard.Attack > 0 ? _battleCard.Attack.ToString() : "-";
            _healthText.text = _battleCard.Health > 0 ? _battleCard.Health.ToString() : "-";
        }
    }
}

