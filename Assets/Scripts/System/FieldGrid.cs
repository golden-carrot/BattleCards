using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleCards.System
{
	public class FieldGrid : MonoBehaviour
	{
		public int Row => _row;
		[SerializeField] private int _row;

		public int Column => _column;
		[SerializeField] private int _column;

		[ContextMenu("Set Grid Item")]
		public void SetGridItem()
		{
			var gridItems = GetComponentsInChildren<FieldGridItem>();

			for (int i = 0; i < gridItems.Length; i++)
			{
				var gridItem = gridItems[i];
				gridItem.SetPosition(i / _column + 1, i % _column + 1);
			}
		}
	}
}
