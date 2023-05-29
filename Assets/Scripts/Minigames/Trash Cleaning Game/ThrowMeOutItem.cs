using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowMeOutItem : MonoBehaviour
{
	public ThrowMeOutGame game;
	public List<Sprite> trashSprites;
	public List<Sprite> notTrashSprites;

	public int spriteIndex = 0;
	public bool isTrash = false;
	Animator animator;

	void Start()
	{
		animator = GetComponent<Animator>();

		// set sprite
		if (isTrash)
		{
			spriteIndex = Random.Range(0, trashSprites.Count);

			while (spriteIndex == game.lastSpriteIndex)
			{
				spriteIndex = Random.Range(0, trashSprites.Count);
			}

			GetComponent<SpriteRenderer>().sprite = trashSprites[spriteIndex];
		}
		else
		{
			spriteIndex = Random.Range(0, notTrashSprites.Count);

			while (spriteIndex == game.lastSpriteIndex)
			{
				spriteIndex = Random.Range(0, notTrashSprites.Count);
			}

			GetComponent<SpriteRenderer>().sprite = notTrashSprites[spriteIndex];
		}

		game.lastSpriteIndex = spriteIndex;
	}

	public void DestroyItem()
	{
		// StartCoroutine(ThrowAnimation());
		animator.SetTrigger("Throw");
		Destroy(gameObject, 1f);
	}

	public void NoThrow()
	{
        animator.SetTrigger("NotThrow");
        Destroy(gameObject, 1f);
	}
}
