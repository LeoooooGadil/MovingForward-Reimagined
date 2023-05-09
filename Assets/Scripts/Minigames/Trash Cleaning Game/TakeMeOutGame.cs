using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeMeOutGame : MonoBehaviour
{
	public TakeMeOutSpawnArea spawnArea;
	public GameObject trashPrefab;
	public BoxCollider2D boxCollider;

	public Text timerText;
	public Text scorePoints;

	public float TotalPoints = 0;
	public int totalSeconds = 60;
	public float currentTimer = 1f;
	public int howManyTrashItemsPerSpawn = 5;

	private bool isGameRunning = false;
    private BoxCollider2D trashArea;
    private Vector3 offset = new Vector3(0, 3, 0);
	private List<TrashItem> spawnedTrashItems = new List<TrashItem>();

	void Start()
	{
		StartTheGame();
	}

	private void StartTheGame()
	{
		isGameRunning = true;
		StartCoroutine(SpawnItems());
	}

	void Update()
	{
		scorePoints.text = TotalPoints.ToString("F0");
		timerText.text = totalSeconds.ToString("F0") + "s";

		if (!isGameRunning) return;

		currentTimer -= Time.deltaTime;

		if (currentTimer < 0)
		{
			totalSeconds--;

			if (totalSeconds <= 0)
			{
				isGameRunning = false;
				// this is where you would call the function to stop the game
			}

			currentTimer = 1f;
		}


	}

	void CheckIfTrashListEmpty()
	{
		if (spawnedTrashItems.Count != 0) return;
		StartCoroutine(SpawnItems());
	}

	public void DunkedTrash(TrashItem trashItem)
	{
		spawnedTrashItems.Remove(trashItem);
		Debug.Log("DunkedTrash: " + trashItem.name);
		Destroy(trashItem.gameObject);

		CheckIfTrashListEmpty();
	}

	IEnumerator SpawnItems()
	{
		if (!isGameRunning) yield break;

		Debug.Log("SpawnItems");

		for (int i = 0; i < howManyTrashItemsPerSpawn; i++)
		{
			TrashItem spawnedItem = spawnArea.Spawn(trashPrefab, gameObject);
			spawnedItem.game = this;
			spawnedTrashItems.Add(spawnedItem);
			// randomize the trash item game objects name
			spawnedItem.name = spawnedItem.name + UnityEngine.Random.Range(0, 1000);

			yield return new WaitForSeconds(0.1f);
		}

	}
}








