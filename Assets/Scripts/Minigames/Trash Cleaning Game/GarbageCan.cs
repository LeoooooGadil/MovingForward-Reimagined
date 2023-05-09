using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCan : MonoBehaviour
{
    public TakeMeOutGame takeMeOutGame;
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

        // position the trash item in the top of the trash can
        Vector3 TrashCanCenter = new Vector3(transform.position.x, transform.position.y, transform.position.z) + offset;

        // move the trash item to the center of the trash can
        while (collision.transform.position != TrashCanCenter)
        {
            collision.transform.position = Vector3.MoveTowards(collision.transform.position, TrashCanCenter, 0.07f);
            yield return null;
        }

        // move the trash item to the bottom of the trash can
        while (collision.transform.position != transform.position)
        {
            collision.transform.position = Vector3.MoveTowards(collision.transform.position, transform.position, 0.07f);
            yield return null;
        }

        takeMeOutGame.DunkedTrash(collision.GetComponent<TrashItem>());
    }
}
