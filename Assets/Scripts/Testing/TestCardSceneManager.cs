using BattleCards.Cards;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BattleCards.Testing
{
	public class TestCardSceneManager : MonoBehaviour
	{
		[SerializeField] private UnityEngine.UI.Dropdown _otherDropdown;
		[SerializeField] private UnityEngine.UI.Dropdown _meDropdown;
		[SerializeField] private Transform _otherPivot;
		[SerializeField] private Transform _mePivot;

		private GameObject _otherCardInstance;
		private GameObject _meCardInstance;

		private void Awake()
		{
			if(_otherDropdown != null && _meDropdown != null)
			{
				var battleCards = Resources.LoadAll<BattleCard>("Cards");

				_otherDropdown.ClearOptions();
				_meDropdown.ClearOptions();

				_otherDropdown.AddOptions(new List<string>() { "None" });
				_otherDropdown.AddOptions(battleCards.Select(Card => Card.Id).ToList());

				_meDropdown.AddOptions(new List<string>() { "None" });
				_meDropdown.AddOptions(battleCards.Select(Card => Card.Id).ToList());
			}
		}

		public void OnSelectedOtherDropdown()
		{
			var selectedId = _otherDropdown.options[_otherDropdown.value].text;

			SetCardInstance(_otherPivot, _otherCardInstance, selectedId);
		}

		public void OnSelectedMeDropdown()
		{
			var selectedId = _meDropdown.options[_meDropdown.value].text;

			SetCardInstance(_mePivot, _meCardInstance, selectedId);
		}

		public void OnClickBattleButton()
		{

		}

		private void SetCardInstance(Transform pivot, GameObject prevInstance, string id)
		{
			if (prevInstance != null)
				DestroyImmediate(prevInstance);

			var newInstance = Instantiate(Resources.Load<GameObject>($"Cards/{id}"));
			newInstance.transform.parent = pivot;
			newInstance.transform.localPosition = Vector3.zero;
			newInstance.transform.localScale = Vector3.one;
			newInstance.transform.localRotation = Quaternion.identity;

			prevInstance = newInstance;
		}
	}
}
