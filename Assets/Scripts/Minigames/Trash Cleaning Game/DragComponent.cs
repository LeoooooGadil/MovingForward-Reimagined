using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragComponent : MonoBehaviour
{
	private Vector3 screenPoint;
	private Vector3 offset;
	private bool dragging = false;

	public bool isActive = true;

	private void OnMouseDown()
	{
		Debug.Log("OnMouseDown");
		if (!isActive) return;

		screenPoint = Camera.main.WorldToScreenPoint(transform.position);
		offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		dragging = true;
	}

	private void OnMouseDrag()
	{
		if (!isActive || !dragging) return;

		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
		transform.position = curPosition;
	}

	private void OnMouseUp()
	{
		dragging = false;
	}
}