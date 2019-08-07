using BattleCards.Cards;
using UnityEngine;
using UnityEngine.UI;

namespace BattleCards.UI
{
	public class UIBattleCard : MonoBehaviour
	{
		[SerializeField] private string _id;
		[SerializeField] private Image _portrait;
		[SerializeField] private Text _power;

		public void Init(string id) {
			_id = id;
			
			var portrait = Resources.Load<Sprite>($"Cards/Portrait/{id}");
			if (portrait != null) {
				_portrait.sprite = portrait;
			}

			var card = Resources.Load<GameObject>($"Cards/{id}");
			if (card != null) {
				var battleCard = card.GetComponent<BattleCard>();
				if (battleCard != null) {
					_power.text = battleCard.Power.ToString();
				}
			}
		}

		private void OnMouseDown()
		{
			var sceneManager = GameObject.FindObjectOfType<Testing.TestCardSceneManager>();
			if(sceneManager != null)
			{
				sceneManager.ShowDragCard(_id);
			}
		}
	}
}
