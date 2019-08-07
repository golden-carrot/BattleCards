using BattleCards.Battle;
using BattleCards.Cards;
using BattleCards.System;
using System.Collections.Generic;
using System.Linq;
using BattleCards.UI;
using UnityEngine;

namespace BattleCards.Testing
{
	public partial class TestCardSceneManager : MonoBehaviour
	{
		[SerializeField] private Transform _cardUIContext;

		private GameObject _otherCardInstance;
		private GameObject _myCardInstance;

		private void Awake()
		{
			Field.Init();

			var cards = Resources.LoadAll<BattleCard>("Cards");
			if (cards != null && cards.Length > 0) {
				var uiObject = Resources.Load<GameObject>("Cards/UI/UI_BattleCard");
				if (uiObject != null) {
					foreach (var card in cards) {
						var instance = Instantiate(uiObject);
						instance.transform.parent = _cardUIContext;
						instance.transform.localPosition = Vector3.zero;
						instance.transform.localScale = Vector3.one;
						
						var uiBattleCard = instance.GetComponent<UIBattleCard>();
						if (uiBattleCard != null) {
							uiBattleCard.Init(card.name);
						}						
					}
				}
			}
		}

		public void OnClickBattleButton()
		{
			CardBattleFunctions.Battle();
		}

		public void OnClickExitButton()
		{
			Application.Quit();
		}

		private void SetCardInstance(string id, Transform pivot, ref GameObject prevInstance)
		{
			if (prevInstance != null)
				DestroyImmediate(prevInstance);

			var newInstance = Instantiate(Resources.Load<GameObject>($"Cards/{id}"));
			newInstance.transform.parent = pivot;
			newInstance.transform.localPosition = Vector3.zero;
			newInstance.transform.localScale = Vector3.one;
			newInstance.transform.localRotation = Quaternion.identity;

			var battleCard = newInstance.GetComponent<BattleCard>();
			battleCard.Row = 1;
			battleCard.Column = 1;

			prevInstance = newInstance;
		}
	}
}
