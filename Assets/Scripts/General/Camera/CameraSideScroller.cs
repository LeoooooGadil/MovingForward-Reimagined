using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraSideScroller : MonoBehaviour
{
	private float screenWidth;
	private Vector2 touchStartPosition;
	private Vector2 cameraStartPosition;
	private bool isDragging = false;

	public float swipeSpeed = 0.5f;
	public float dragSpeed = 0.5f;
	public CameraBounds cameraBounds;

	private float distanceToMaxSide;

	void Start()
	{
		screenWidth = Screen.width;
		cameraStartPosition = transform.position;

		float viewportWidth = Camera.main.orthographicSize * 2.0f * Camera.main.aspect;
		distanceToMaxSide = viewportWidth / 2.0f;
	}

	void Update()
	{
		// Check if the touch is over a button UI element
		bool isTouchingUI = IsPointerOverUIObject();
		// If the touch is over a button UI element, don't move the camera
		if (isTouchingUI) return;


#if UNITY_EDITOR || UNITY_STANDALONE
		OnDesktop();
#elif UNITY_ANDROID || UNITY_IOS
			Touch touch = Input.GetTouch(0);
			OnMobile(touch);
#endif
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
			float deltaX = Input.mousePosition.x - touchStartPosition.x;
			float newPosition = cameraStartPosition.x - (deltaX / screenWidth) * dragSpeed;
			float clampedX = Mathf.Clamp(newPosition, cameraBounds.leftBound + distanceToMaxSide, cameraBounds.rightBound - distanceToMaxSide);

			// Check if the camera is still being dragged beyond the maximum bounds
			if (clampedX == cameraBounds.leftBound + distanceToMaxSide && deltaX < 0)
			{
				// If dragging to the left and already at the maximum left bound, start moving to the right
				transform.position = new Vector3(clampedX + distanceToMaxSide * 2.0f, transform.position.y, transform.position.z);
				cameraStartPosition = transform.position;
			}
			else if (clampedX == cameraBounds.rightBound - distanceToMaxSide && deltaX > 0)
			{
				// If dragging to the right and already at the maximum right bound, start moving to the left
				transform.position = new Vector3(clampedX - distanceToMaxSide * 2.0f, transform.position.y, transform.position.z);
				cameraStartPosition = transform.position;
			}
			else
			{
				transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
			}
		}
		else if (Input.GetMouseButtonUp(0) && isDragging)
		{
			isDragging = false;
			float deltaX = Input.mousePosition.x - touchStartPosition.x;
			float newPosition = cameraStartPosition.x - (deltaX / screenWidth) * swipeSpeed;
			float clampedX = Mathf.Clamp(newPosition, cameraBounds.leftBound + distanceToMaxSide, cameraBounds.rightBound - distanceToMaxSide);

			transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
		}
	}

	void OnMobile(Touch touch)
	{
		if (Input.touchCount == 1)
		{
			if (touch.phase == TouchPhase.Began)
			{
				touchStartPosition = touch.position;
				cameraStartPosition = transform.position;
				isDragging = true;
			}
			else if (touch.phase == TouchPhase.Moved && isDragging)
			{
				float deltaX = touch.position.x - touchStartPosition.x;
				float newPosition = cameraStartPosition.x - (deltaX / screenWidth) * dragSpeed;
				float clampedX = Mathf.Clamp(newPosition, cameraBounds.leftBound + distanceToMaxSide, cameraBounds.rightBound - distanceToMaxSide);

				// Check if the camera is still being dragged beyond the maximum bounds
				if (clampedX == cameraBounds.leftBound + distanceToMaxSide && deltaX < 0)
				{
					// If dragging to the left and already at the maximum left bound, start moving to the right
					transform.position = new Vector3(clampedX + distanceToMaxSide * 2.0f, transform.position.y, transform.position.z);
					cameraStartPosition = transform.position;
				}
				else if (clampedX == cameraBounds.rightBound - distanceToMaxSide && deltaX > 0)
				{
					// If dragging to the right and already at the maximum right bound, start moving to the left
					transform.position = new Vector3(clampedX - distanceToMaxSide * 2.0f, transform.position.y, transform.position.z);
					cameraStartPosition = transform.position;
				}
				else
				{
					transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
				}
			}
			else if (touch.phase == TouchPhase.Ended && isDragging)
			{
				isDragging = false;
				float deltaX = touch.position.x - touchStartPosition.x;
				float newPosition = cameraStartPosition.x - (deltaX / screenWidth) * swipeSpeed;
				float clampedX = Mathf.Clamp(newPosition, cameraBounds.leftBound + distanceToMaxSide, cameraBounds.rightBound - distanceToMaxSide);

				transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
			}
		}
	}

	// 3 days debugging to find this code. Thanks to this StackOverflow answer
	// https://stackoverflow.com/a/64172180

	private bool IsPointerOverUIObject()
	{
		// Referencing this code for GraphicRaycaster https://gist.github.com/stramit/ead7ca1f432f3c0f181f
		// the ray cast appears to require only eventData.position.
		PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
		eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

		List<RaycastResult> results = new List<RaycastResult>();

		if (EventSystem.current == null)
			return false;

		EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

		// only return true if there is the tag is Button
		foreach (RaycastResult result in results)
		{
			if (result.gameObject.tag == "DontSwipe")
			{
				return true;
			}
		}

		return false;
	}
}
