using BattleCards.Battle;
using BattleCards.Cards;
using BattleCards.System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleCards.Testing
{
	public partial class TestCardSceneManager
	{
		[SerializeField] private float DRAG_SCALE = 1f;

		private bool _isDragging = false;
		private GameObject _dragCardInstance;

		public void ShowDragCard(string id)
		{
			DestroyDragCard();

			_dragCardInstance = Instantiate(Resources.Load<GameObject>($"Cards/{id}"));
			if(_dragCardInstance != null)
			{
				_isDragging = true;
				_dragCardInstance.transform.localPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				_dragCardInstance.transform.localScale = new Vector3(DRAG_SCALE, DRAG_SCALE, DRAG_SCALE);
				_dragCardInstance.transform.localRotation = Quaternion.identity;
			}
		}

		private void DestroyDragCard()
		{
			if (_dragCardInstance != null)
			{
				DestroyImmediate(_dragCardInstance);
			}
		}

		private void PlaceDragCardToPivot(Transform pivot)
		{
			if(_dragCardInstance == null)
			{
				Debug.LogError("Drag card object is null..!!");
				return;
			}

			var fieldGridItem = pivot.gameObject.GetComponent<FieldGridItem>();
			if(Field.HasCard(fieldGridItem.Row, fieldGridItem.Column))
			{
				Destroy(_dragCardInstance);
				_dragCardInstance = null;
				_isDragging = false;
				return;
			}

			_dragCardInstance.transform.parent = pivot;
			_dragCardInstance.transform.localPosition = Vector3.zero;
			_dragCardInstance.transform.localScale = Vector3.one;
			_dragCardInstance.transform.localRotation = Quaternion.identity;
			
			var draggedBattleCard = _dragCardInstance.GetComponent<BattleCard>();
			if (fieldGridItem != null && draggedBattleCard)
			{
				draggedBattleCard.Row = fieldGridItem.Row;
				draggedBattleCard.Column = fieldGridItem.Column;
				draggedBattleCard.Team = fieldGridItem.Direction;

				Field.AddCard(draggedBattleCard);
			}

			_dragCardInstance = null;
			_isDragging = false;
		}

		private void Update()
		{
			if(_dragCardInstance != null && _isDragging)
			{
				if(Input.GetMouseButton(0))
				{
					_dragCardInstance.transform.localPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				}
				else
				{
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					RaycastHit raycastHit;
					if(Physics.Raycast(ray, out raycastHit, float.MaxValue))
					{
						PlaceDragCardToPivot(raycastHit.transform);
					}
				}
			}
		}
	}
}
