using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeArea : MonoBehaviour
{
	RectTransform rectTransform;
	Rect safeArea;
	Vector2 minAnchor;
	Vector2 maxAnchor;

	public bool affectWidth = true;
	public bool affectHeight = true;

	void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
		safeArea = Screen.safeArea;
		minAnchor = safeArea.position;
		maxAnchor = safeArea.position + safeArea.size;

		minAnchor.x /= Screen.width;
		maxAnchor.x /= Screen.width;
		minAnchor.y /= Screen.height;
		maxAnchor.y /= Screen.height;

		rectTransform.anchorMin = minAnchor;
		rectTransform.anchorMax = maxAnchor;
	}

	void Update()
	{
		rectTransform = GetComponent<RectTransform>();
		safeArea = Screen.safeArea;
		minAnchor = safeArea.position;
		maxAnchor = safeArea.position + safeArea.size;

		minAnchor.x /= Screen.width;
		maxAnchor.x /= Screen.width;
		minAnchor.y /= Screen.height;
		maxAnchor.y /= Screen.height;

		rectTransform.anchorMin = minAnchor;
		rectTransform.anchorMax = maxAnchor;
	}
}
