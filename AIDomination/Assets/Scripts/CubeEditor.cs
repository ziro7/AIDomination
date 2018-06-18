using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Waypoint))]
public class CubeEditor : MonoBehaviour
{

	Waypoint _waypoint;

	private void Awake()
	{
		_waypoint = GetComponent<Waypoint>();
	}

	void Update()
	{
		SnapToGrid();
		UpdateLabel();
	}

	private void SnapToGrid()
	{
		int gridSize = _waypoint.GetGridSize();
		transform.position = new Vector3(
			_waypoint.GetGridPos().x * gridSize,
			0f,
			_waypoint.GetGridPos().y * gridSize
		);
	}

	private void UpdateLabel()
	{
		TextMesh textMesh = GetComponentInChildren<TextMesh>();
		string labelText =
			_waypoint.GetGridPos().x +
			"," +
			_waypoint.GetGridPos().y;
		textMesh.text = labelText;
		gameObject.name = labelText;
	}
}
