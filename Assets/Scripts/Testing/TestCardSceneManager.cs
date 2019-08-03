using BattleCards.Battle;
using BattleCards.Cards;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BattleCards.Testing
{
	public class TestCardSceneManager : MonoBehaviour
	{
		[SerializeField] private UnityEngine.UI.Dropdown _otherDropdown;
		[SerializeField] private UnityEngine.UI.Dropdown _myDropdown;
		[SerializeField] private Transform _otherPivot;
		[SerializeField] private Transform _myPivot;

		private GameObject _otherCardInstance;
		private GameObject _myCardInstance;

		private void Awake()
		{
			if(_otherDropdown != null && _myDropdown != null)
			{
				var battleCards = Resources.LoadAll<BattleCard>("Cards");

				_otherDropdown.ClearOptions();
				_myDropdown.ClearOptions();

				_otherDropdown.AddOptions(new List<string>() { "None" });
				_otherDropdown.AddOptions(battleCards.Select(Card => Card.Id).ToList());

				_myDropdown.AddOptions(new List<string>() { "None" });
				_myDropdown.AddOptions(battleCards.Select(Card => Card.Id).ToList());
			}
		}

		public void OnSelectedOtherDropdown()
		{
			var selectedId = _otherDropdown.options[_otherDropdown.value].text;

			SetCardInstance(selectedId, _otherPivot, ref _otherCardInstance);
		}

		public void OnSelectedMyDropdown()
		{
			var selectedId = _myDropdown.options[_myDropdown.value].text;

			SetCardInstance(selectedId, _myPivot, ref _myCardInstance);
		}

		public void OnClickBattleButton()
		{
			CardBattleFunctions.Battle(new List<BattleCard>() { _myCardInstance.GetComponent<BattleCard>() }, new List<BattleCard>() { _otherCardInstance.GetComponent<BattleCard>() });
		}

		public void OnClickExitButton()
		{
			Application.Quit();
		}

		public void OnClickBlankSpace()
		{

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
