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

		if (points > 0)
		{
			pointsText.text = "+" + points.ToString();
			// #ffda79
			pointsText.color = new Color(1f, 0.85f, 0.475f);
		}
		else
		{
			pointsText.text = points.ToString();
			// #ff5252
			pointsText.color = new Color(1f, 0.322f, 0.322f);
		}
	}

	IEnumerator DestroyOverTime(float time)
	{
		yield return new WaitForSeconds(time);
		Destroy(gameObject);
	}
}
