using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameVerticalScroller : MonoBehaviour
{
	private Vector2 startPosition;

	private Vector2 touchStartPosition;
	private Vector2 cameraStartPosition;

	private bool isDragging = false;

	public float swipeSpeed = 0.5f;
	public float dragSpeed = 0.5f;

	// The maximum distance the camera can move from the start position
	// only in the y direction and only the positive direction
	private float distanceToMaxSide = 300f;

	void Start()
	{
		startPosition = transform.position;
	}

	void Update()
	{
		bool isTouchingUI = IsPointerOverGameView();

		if (!isTouchingUI) return;
		OnDesktop();
	}

	void OnDesktop()
	{
		if (Input.GetMouseButtonDown(0))
		{
			touchStartPosition = Input.mousePosition;
			cameraStartPosition = transform.position;
			isDragging = true;
		}
		else if (Input.GetMouseButton(0) && isDragging)
		{
			float deltaY = Input.mousePosition.y - touchStartPosition.y;
			float newPosition = cameraStartPosition.y - (deltaY / Screen.height) * dragSpeed;
			float clampedY = Mathf.Clamp(newPosition, startPosition.y - distanceToMaxSide, startPosition.y + distanceToMaxSide);

			// check if dragging up
			if (clampedY == startPosition.y + distanceToMaxSide && deltaY > 0)
			{
				transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);
			}
			else
			{
                transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);
			}
		}
		else if (Input.GetMouseButtonUp(0))
		{
			isDragging = false;
			float deltaY = Input.mousePosition.y - touchStartPosition.y;
			float newPosition = cameraStartPosition.y - (deltaY / Screen.height) * swipeSpeed;
			float clampedY = Mathf.Clamp(newPosition, startPosition.y - distanceToMaxSide, startPosition.y + distanceToMaxSide);

			transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);
		}
	}

	private bool IsPointerOverGameView()
	{
		// Referencing this code for GraphicRaycaster https://gist.github.com/stramit/ead7ca1f432f3c0f181f
		// the ray cast appears to require only eventData.position.
		PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
		eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

		List<RaycastResult> results = new List<RaycastResult>();

		if (EventSystem.current == null)
			return false;

		EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

		// only return true if there is the tag
		foreach (RaycastResult result in results)
		{
			if (result.gameObject.tag == "WordleGameView")
			{
				return true;
			}
		}

		return false;
	}
}

