
using UnityEngine;
using static BattleCards.Battle.CardBattleFunctions;

namespace BattleCards.System
{
	public class FieldGridItem : MonoBehaviour
	{
		public int Row => _row;
		[SerializeField] private int _row;

		public int Column => _column;
		[SerializeField] private int _column;

		public Team Direction => _direction;
		[SerializeField] private Team _direction;

		public void SetPosition(int row, int column)
		{
			_row = row;
			_column = column;
		}
	}
}
