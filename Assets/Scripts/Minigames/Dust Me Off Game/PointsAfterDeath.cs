using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsAfterDeath : MonoBehaviour
{
	public float points = 10;
	private float lifeTime = 2f;
    public Text pointsText;

	void Start()
	{
        StartCoroutine(DestroyOverTime(lifeTime));
        pointsText.text = "+" + points.ToString() + " pts";
	}

    IEnumerator DestroyOverTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
