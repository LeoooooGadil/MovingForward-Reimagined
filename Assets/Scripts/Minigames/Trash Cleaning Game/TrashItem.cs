using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrashItem : MonoBehaviour
{
    public List<Sprite> furnitureSprites = new List<Sprite>();
    
	public bool isBeingThrown = false;
	public ItemType itemType;
	public TakeMeOutGame game;
    public float points = 0;

	private bool isDragging = false;
	private Vector3 offset;
	private Rigidbody2D rb;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
        points = Random.Range(5, 30);

        // set color based on item type
        if (itemType == ItemType.Trash)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.green;
        }

        AudioManager.instance.PlaySFX("ButtonClick");
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the mouse pointer
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                // The mouse is over this GameObject, so we can start dragging it
                isDragging = true;
                offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                rb.bodyType = RigidbodyType2D.Kinematic; // Switch to kinematic to prevent physics from interfering
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            // Stop dragging the GameObject
            isDragging = false;
            rb.constraints = RigidbodyConstraints2D.None;
            rb.bodyType = RigidbodyType2D.Static; // Switch back to static to disable physics
        }

        if (isDragging)
        {
            // Drag the GameObject along with the mouse pointer
            Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z);
            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
            rb.MovePosition(cursorPosition);
        }
	}
}

public enum ItemType
{
	Trash,
	NotTrash
}
