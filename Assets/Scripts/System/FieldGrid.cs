using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BattleCards.System
{
	public class FieldGrid : MonoBehaviour
	{
		public int Row => _row;
		[SerializeField] private int _row;

		public int Column => _column;
		[SerializeField] private int _column;

		[SerializeField] private List<FieldGridItem> _gridItems;

#if UNITY_EDITOR
		[ContextMenu("Set Grid Item")]
		public void SetGridItem()
		{
			if (_gridItems != null)
				_gridItems.Clear();
			_gridItems = GetComponentsInChildren<FieldGridItem>().ToList();

			for (int i = 0; i < _gridItems.Count; i++)
			{
				var gridItem = _gridItems[i];
				gridItem.SetPosition(i / _column + 1, i % _column + 1);
			}
		}
#endif

		public Transform GetPivot(int row, int column)
		{
			return _gridItems.FirstOrDefault(item => item.Row == row && item.Column == column).transform;
		}
	}
}
