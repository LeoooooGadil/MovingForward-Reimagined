using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
	public BoxCollider2D boxCollider;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// Debug.Log("TrashCan: OnTriggerStay2D");
		if (collision.gameObject.tag == "Trash Item")
		{
            StartCoroutine(ThrowItInTheTrash(collision.gameObject));
		}
	}

    IEnumerator ThrowItInTheTrash(GameObject collision)
    {
        collision.GetComponent<TrashItem>().isBeingThrown = true;
        AudioManager.instance.PlaySFX("TrashCrumpleSfx");

        // position the trash item in the top center of the trash can
        Vector3 TrashCanCenter = new Vector3(transform.position.x, transform.position.y + boxCollider.size.y / 2, transform.position.z);

        // move the trash item to the center of the trash can
        while (collision.transform.position != TrashCanCenter)
        {
            collision.transform.position = Vector3.MoveTowards(collision.transform.position, TrashCanCenter, 0.1f);
            yield return null;
        }

        // yield return new WaitForSeconds(0f);
        Destroy(collision.gameObject);
    }
}
