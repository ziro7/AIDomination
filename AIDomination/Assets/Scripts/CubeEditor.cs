using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
public class CubeEditor : MonoBehaviour
{
	
	[SerializeField] [Range(1f,20f)] private float _gridSize = 10f;

	private TextMesh _textMesh;

	void Start()
	{
		_textMesh = GetComponentInChildren<TextMesh>();
	}

	void Update()
	{
		Vector3 snapPos;
		snapPos.x = Mathf.RoundToInt(transform.position.x / _gridSize) * _gridSize;
		snapPos.y = 0f;
		snapPos.z = Mathf.RoundToInt(transform.position.z / _gridSize) * _gridSize;

		transform.position = new Vector3(snapPos.x, snapPos.y, snapPos.z);
		string _labelText = snapPos.x / _gridSize + "," + snapPos.z / _gridSize;
		_textMesh.text = _labelText;
		gameObject.name = "Cube: " + _labelText;
	}
	
}
