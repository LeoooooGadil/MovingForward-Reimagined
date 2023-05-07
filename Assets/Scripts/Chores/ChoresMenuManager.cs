using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoresMenuManager : MonoBehaviour
{
	public List<Chore> unfinishedChores;
	public List<Chore> finishedChores;

	public GameObject chorePrefab;
	public Transform choreContainer;
	public Text ratioText;

	void Start()
	{
		LoadChores();
	}

	void OnDisable()
	{
		CleanUpChores();
	}

	void OnEnable()
	{
		LoadChores();
		DisplayChores();
	}

	void Update()
	{
		UpdateRatioText();
	}

	void UpdateRatioText()
	{

		if (ChoresManager.instance == null)
		{
			Debug.LogError("ChoreManager.instance is null");
			return;
		}

		List<Chore> loadedFinishedChores = ChoresManager.instance.GetChores()["finished"];
		List<Chore> loadedUnfinishedChores = ChoresManager.instance.GetChores()["unfinished"];

		if (loadedFinishedChores == null || loadedUnfinishedChores == null) return;

		ratioText.text = loadedUnfinishedChores.Count + "/" + loadedFinishedChores.Count;
	}

	void LoadChores()
	{
		if (ChoresManager.instance == null)
		{
			Debug.LogError("ChoreManager.instance is null");
			return;
		}

		List<Chore> loadedunfinishedChores = ChoresManager.instance.GetChores()["unfinished"];
		List<Chore> loadedfinishedChores = ChoresManager.instance.GetChores()["finished"];

		if (unfinishedChores == null || finishedChores == null) return;

		Debug.Log("Unfinished Chores: " + unfinishedChores.Count);
		Debug.Log("Finished Chores: " + finishedChores.Count);

		unfinishedChores = loadedunfinishedChores;
		finishedChores = loadedfinishedChores;
	}

	void DisplayChores()
	{
		if (unfinishedChores == null || finishedChores == null) return;

		StartCoroutine(DisplayChoresWithDelay());
	}

	IEnumerator DisplayChoresWithDelay()
	{
		int index = 0;
		foreach (Chore chore in unfinishedChores)
		{
			GameObject choreObject = Instantiate(chorePrefab, choreContainer);
			choreObject.GetComponent<ChoreItem>().choreMenuManager = this;
			choreObject.GetComponent<ChoreItem>().SetChore(chore);
			choreObject.GetComponent<ChoreItem>().SetIndex(index);
			index++;
			yield return new WaitForSeconds(0.1f);
		}

		foreach (Chore chore in finishedChores)
		{
			GameObject choreObject = Instantiate(chorePrefab, choreContainer);
			choreObject.GetComponent<ChoreItem>().choreMenuManager = this;
			choreObject.GetComponent<ChoreItem>().SetChore(chore);
			choreObject.GetComponent<ChoreItem>().SetIndex(index);
			index++;
			yield return new WaitForSeconds(0.1f);
		}
	}

	void CleanUpChores()
	{
		foreach (Transform child in choreContainer)
		{
			Destroy(child.gameObject);
		}
	}

	public void CompleteChore(Chore chore)
	{
        if(chore == null) return;
        ChoresManager.instance.CompleteChore(chore);
	}

	public void PlayChore(Chore chore)
	{
		if (chore == null) return;
		ChoresManager.instance.PlayChore(chore);
	}
}
