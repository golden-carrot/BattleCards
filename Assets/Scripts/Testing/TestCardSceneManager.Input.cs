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
		private BattleCard _dragCardInstance;

		public void ShowDragCard(string id)
		{
			DestroyDragCard();

			var dragCardInstance = Instantiate(Resources.Load<GameObject>($"Cards/{id}"));
			if (dragCardInstance == null)
				return;

			_dragCardInstance = dragCardInstance.GetComponent<BattleCard>();
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

		private void PlaceDragCardToPivot(GameObject cardInstance, Transform pivot)
		{
			if(cardInstance == null)
			{
				Debug.LogError("Drag card object is null..!!");
				return;
			}

			var fieldGridItem = pivot.gameObject.GetComponent<FieldGridItem>();
			if(Field.HasCard(fieldGridItem.Row, fieldGridItem.Column))
			{
				Destroy(cardInstance);
				cardInstance = null;
				_isDragging = false;
				return;
			}

			cardInstance.transform.parent = pivot;
			cardInstance.transform.localPosition = Vector3.zero;
			cardInstance.transform.localScale = Vector3.one;
			cardInstance.transform.localRotation = Quaternion.identity;
			
			var draggedBattleCard = cardInstance.GetComponent<BattleCard>();
			if (fieldGridItem != null && draggedBattleCard)
			{
				draggedBattleCard.Row = fieldGridItem.Row;
				draggedBattleCard.Column = fieldGridItem.Column;
				draggedBattleCard.Team = fieldGridItem.Direction;

				Field.AddCard(draggedBattleCard);
			}
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
					if(Physics.Raycast(ray, out raycastHit, float.MaxValue)) {
						FieldGrid.Instance.PlaceCardInstance(_dragCardInstance, raycastHit.transform);
						_dragCardInstance = null;
						_isDragging = false;
					}
				}
			}
		}
	}
}
