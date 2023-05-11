using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEventBlocker : MonoBehaviour
{
    void Update()
    {
        // Check if the mouse is over any GameObject with a Collider
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            GameObject hitObject = hit.collider.gameObject;

            // Print the name of the GameObject blocking the mouse events
            Debug.Log("Mouse blocked by: " + hitObject.name);
        }
    }
}
