using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoresLeftManager : MonoBehaviour
{
	public Text ScoreText;
	public int choresLeft = 0;

	public float currentTimer = 0;

	void OnEnable()
	{
        ScoreText.text = "Loading...";
        UpdateData();
	}

	void Update()
	{
		if (currentTimer < 0.5)
		{
			currentTimer += Time.deltaTime;
		}
		else
		{
			currentTimer = 0;
			UpdateData();
		}
	}

	void UpdateData()
	{
		if (ChoresManager.instance == null) return;

		List<Chore> choresLeft = ChoresManager.instance.GetUnfinishedChores();

		if (choresLeft.Count == 0)
		{
			ScoreText.text = "No chores left";
		}
		else
		{
			ScoreText.text = choresLeft.Count + " chores left";
		}
	}
}
