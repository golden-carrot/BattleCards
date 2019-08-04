using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BattleCards.UI
{
	public class UIBattleCard : MonoBehaviour
	{
		[SerializeField] private string _id;
		[SerializeField] private Image _portrait;
		[SerializeField] private Text _power;

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
