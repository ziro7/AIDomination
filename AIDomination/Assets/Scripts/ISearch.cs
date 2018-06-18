using UnityEngine;

namespace Assets.Scripts
{
	interface ISearch
	{
		void Search();
		void AddNewNeighbours(Vector2Int neighbourCoordinates);
	}
}
