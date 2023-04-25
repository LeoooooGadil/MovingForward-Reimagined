using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private Animator animate;
    private float moveSpeed;
    private float moveHorizontal;
    private bool facingRight = true;

	// Start is called before the first frame update
	void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animate = GetComponent<Animator>();
        moveSpeed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");

        animate.SetFloat("Speed", Mathf.Abs(moveHorizontal));

        if (moveHorizontal > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveHorizontal < 0 && facingRight)
        {
            Flip();
        }
    }

    // FixedUpdate is called every fixed frame-rate frame
    void FixedUpdate()
    {
        if (moveHorizontal != 0)
        {
            // Move the character left or right based on the input
            rb2D.velocity = new Vector2(moveHorizontal * moveSpeed, rb2D.velocity.y);
        }
        else
        {
            // If the player isn't moving, stop the character from moving
            rb2D.velocity = new Vector2(0f, rb2D.velocity.y);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
    }
}
