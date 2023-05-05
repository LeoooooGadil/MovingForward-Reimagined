using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
	public CameraBounds cameraBounds;

	private BoxCollider2D playerCollider;
	private PlayerController playerController;
	private float moveHorizontal = 0f;
	private float leftBound;
	private float rightBound;
	public enum NPCState { Idle, WalkingLeft, WalkingRight };
	public NPCState npcState = NPCState.Idle;

	public float currentTimer = 0f;
	public float idleMaxTime = 7f;
	public float walkMaxTime = 5f;

	void Start()
	{
		playerController = GetComponent<PlayerController>();
		playerCollider = GetComponent<BoxCollider2D>();
        Spawn();
	}

	void Update()
	{
		// make sure the player is within the bounds of the camera
		leftBound = cameraBounds.leftBound + playerCollider.size.x / 2.0f;
		rightBound = cameraBounds.rightBound - playerCollider.size.x / 2.0f;

		if (transform.position.x < leftBound)
		{
			transform.position = new Vector3(leftBound, transform.position.y, transform.position.z);
            npcState = NPCState.WalkingRight;
		}
		else if (transform.position.x > rightBound)
		{
			transform.position = new Vector3(rightBound, transform.position.y, transform.position.z);
            npcState = NPCState.WalkingLeft;
		}

		playerController.Move(moveHorizontal);
	}

    void Spawn()
    {
        // spawn the player at a random location within the camera bounds
        float randomX = Random.Range(leftBound, rightBound);
        transform.position = new Vector3(randomX, transform.position.y, transform.position.z);
    }

	void FixedUpdate()
	{
		switch (npcState)
		{
			case NPCState.Idle:
				moveHorizontal = 0f;
                Idling();
				break;
			case NPCState.WalkingLeft:
				moveHorizontal = -1f;
                Walking();
				break;
			case NPCState.WalkingRight:
				moveHorizontal = 1f;
                Walking();
				break;
		}
	}

	void Idling()
	{
		npcState = NPCState.Idle;

		if (currentTimer >= idleMaxTime)
		{
			currentTimer = 0f;
            int random = Random.Range(0, 2);
            int randomWalkTime = Random.Range(1, 6);
            
            if (random == 0)
            {
                npcState = NPCState.WalkingLeft;
            }
            else
            {
                npcState = NPCState.WalkingRight;
                
            }

            walkMaxTime = randomWalkTime;
		}
		else
		{
			currentTimer += Time.deltaTime;
		}
	}

	void Walking()
	{
		if (currentTimer >= walkMaxTime)
		{
			currentTimer = 0f;
            int randomIdleTime = Random.Range(1, 13);
            idleMaxTime = randomIdleTime;
			npcState = NPCState.Idle;
		}
		else
		{
			currentTimer += Time.deltaTime;
		}
	}



}
