using System.Collections.Generic;
using System.Linq;
using BattleCards.Battle;
using BattleCards.Cards;
using UnityEngine;

namespace BattleCards.System
{
	public class FieldGrid : Singleton<FieldGrid>
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

		public void PlaceCardInstance(BattleCard battleCard, Transform pivot) {
			if(battleCard == null)
			{
				Debug.LogError("Drag card object is null..!!");
				return;
			}

			var fieldGridItem = pivot.gameObject.GetComponent<FieldGridItem>();
			if (fieldGridItem == null) {
				Destroy(battleCard.gameObject);
				return;
			}
			
			if(Field.HasCard(fieldGridItem.Row, fieldGridItem.Column))
			{
				Destroy(battleCard.gameObject);
				return;
			}

			battleCard.gameObject.transform.parent = pivot;
			battleCard.gameObject.transform.localPosition = Vector3.zero;
			battleCard.gameObject.transform.localScale = Vector3.one;
			battleCard.gameObject.transform.localRotation = Quaternion.identity;
			
			battleCard.Row = fieldGridItem.Row;
			battleCard.Column = fieldGridItem.Column;
			battleCard.Team = fieldGridItem.Direction;

			Field.AddCard(battleCard);
		}
		
		public void MoveCardInstance(BattleCard battleCard, int row, int column) {
			if(battleCard == null)
			{
				Debug.LogError("Drag card object is null..!!");
				return;
			}
            
			if(Field.HasCard(row, column)) {
				return;
			}

			var pivot = GetPivot(row, column);
			battleCard.gameObject.transform.parent = pivot;
			battleCard.gameObject.transform.localPosition = Vector3.zero;
			battleCard.gameObject.transform.localScale = Vector3.one;
			battleCard.gameObject.transform.localRotation = Quaternion.identity;

			battleCard.Row = row;
			battleCard.Column = column;
		}
	}
}
