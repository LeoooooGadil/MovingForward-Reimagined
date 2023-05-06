using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragComponent : MonoBehaviour
{
    private Vector3 offset;
    private bool dragging = false;

    public bool isActve = false;

    void OnMouseDown()
    {
        offset = transform.position - GetInputPosition();
        dragging = true;
    }

    void OnMouseUp()
    {
        dragging = false;
    }

    void Update()
    {
        if (dragging && isActve)
        {
            Vector3 newPosition = GetInputPosition() + offset;
            transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
        }
    }

    private Vector3 GetInputPosition()
    {
        Vector3 inputPos;

        // Check for touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            inputPos = touch.position;
        }
        // Otherwise, use mouse input
        else
        {
            inputPos = Input.mousePosition;
        }

        // Convert input position to world position
        inputPos = Camera.main.ScreenToWorldPoint(inputPos);
        inputPos.z = 0;

        return inputPos;
    }
}
