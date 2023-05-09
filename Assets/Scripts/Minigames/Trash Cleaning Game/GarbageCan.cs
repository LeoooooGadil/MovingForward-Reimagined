using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCan : MonoBehaviour
{
	public TakeMeOutGame takeMeOutGame;
	public ItemType itemType;
	public Transform trashCenter;
	public Transform scoreLocation;
	private Vector3 offset = new Vector3(0, 3f, 0);
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Trash Item")
		{
			StartCoroutine(ThrowItInTheTrash(other.gameObject));
		}
	}

	IEnumerator ThrowItInTheTrash(GameObject collision)
	{
		collision.GetComponent<TrashItem>().isBeingThrown = true;
		AudioManager.instance.PlaySFX("TrashCrumpleSfx");

		takeMeOutGame.DunkedTrash(collision.GetComponent<TrashItem>());

		if (collision.GetComponent<TrashItem>().itemType == GetCurrenItemType())
		{
			AudioManager.instance.PlaySFX("WinSfx");
			takeMeOutGame.AddPoints(collision.GetComponent<TrashItem>().points);
			takeMeOutGame.SpawnAtScore(scoreLocation.position, collision.GetComponent<TrashItem>().points);
		}
		else
		{
			AudioManager.instance.PlaySFX("WrongSfx");
			takeMeOutGame.RemovePoints(collision.GetComponent<TrashItem>().points);
			takeMeOutGame.SpawnAtScore(scoreLocation.position, -collision.GetComponent<TrashItem>().points);
		}

		takeMeOutGame.SwitchTrash();

		// position the trash item in the top of the trash can
		Vector3 TrashCanCenter = new Vector3(transform.position.x, transform.position.y, transform.position.z) + offset;

		// move the trash item to the center of the trash can
		while (collision.transform.position != TrashCanCenter)
		{
			collision.transform.position = Vector3.MoveTowards(collision.transform.position, TrashCanCenter, 0.09f);
			yield return null;
		}

		// move the trash item to the bottom of the trash can
		while (collision.transform.position != trashCenter.position)
		{
			collision.transform.position = Vector3.MoveTowards(collision.transform.position, trashCenter.position, 0.09f);
			yield return null;
		}

		

		Destroy(collision.gameObject);
	}

	ItemType GetCurrenItemType()
	{
		bool isSwitched = takeMeOutGame.isSwitched;

		if (isSwitched)
		{
			if (itemType == ItemType.Trash)
			{
				return ItemType.NotTrash;
			}
			else
			{
				return ItemType.Trash;
			}
		}
		else
		{
			return itemType;
		}
	}
}
