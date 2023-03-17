using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    public BoxCollider2D boundsCollider;

    public float leftBound;
    public float rightBound;

    public float borderSize = 0.1f;
    private SpriteRenderer leftBorder;
    private SpriteRenderer rightBorder;

    private void Start()
    {
        boundsCollider = GetComponent<BoxCollider2D>();

        leftBound = boundsCollider.bounds.min.x;
        rightBound = boundsCollider.bounds.max.x;

        // Create the left border
        GameObject leftBorderObject = new GameObject("Left Border");
        leftBorderObject.transform.SetParent(transform);
        leftBorder = leftBorderObject.AddComponent<SpriteRenderer>();
        leftBorder.sprite = Sprite.Create(Texture2D.whiteTexture, new Rect(0, 0, 1, 1), Vector2.one * 0.5f);
        leftBorder.color = Color.red;
        leftBorder.sortingOrder = 1;

        // Create the right border
        GameObject rightBorderObject = new GameObject("Right Border");
        rightBorderObject.transform.SetParent(transform);
        rightBorder = rightBorderObject.AddComponent<SpriteRenderer>();
        rightBorder.sprite = Sprite.Create(Texture2D.whiteTexture, new Rect(0, 0, 1, 1), Vector2.one * 0.5f);
        rightBorder.color = Color.red;
        rightBorder.sortingOrder = 1;

        // Update the borders based on the size and position of the collider
        UpdateBorders();
    }

    void OnValidate()
    {
        // Update the borders in the editor when the border size changes
        if (leftBorder != null && rightBorder != null)
        {
            UpdateBorders();
        }
    }

    void UpdateBorders()
    {
        // Set the size and position of the left border
        leftBorder.transform.localScale = new Vector3(borderSize, boundsCollider.size.y + borderSize * 2.0f, 1.0f);
        leftBorder.transform.localPosition = new Vector3(-boundsCollider.size.x / 2.0f - borderSize / 2.0f, 0.0f, 0.0f);

        // Set the size and position of the right border
        rightBorder.transform.localScale = new Vector3(borderSize, boundsCollider.size.y + borderSize * 2.0f, 1.0f);
        rightBorder.transform.localPosition = new Vector3(boundsCollider.size.x / 2.0f + borderSize / 2.0f, 0.0f, 0.0f);
    }

    void OnDrawGizmosSelected()
    {
        // Draw the collider bounds in the editor
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(boundsCollider.size.x, boundsCollider.size.y, 0.0f));
    }

    public float GetWidth()
    {
        return boundsCollider.bounds.size.x;
    }
}
