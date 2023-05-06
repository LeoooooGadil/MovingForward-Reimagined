using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DustMeOffGame : MonoBehaviour
{
	public GameObject furnitureItemPrefab;
	public GameObject pointsAfterDeathPrefab;
	public List<SpawnArea> spawnAreas;
	public Text pointsText;
	public Text timerText;

	public float spawnRate = 5f;

	public float TotalPoints = 0;
	public int TotalFurniture = 0;

	public int TotalSeconds = 60;
	public float currentTimer = 1f;

    private bool isGameRunning = true;

	private List<FurnitureItem> spawnedFurnitureItems = new List<FurnitureItem>();


	void Start()
	{
        isGameRunning = true;
		TotalFurniture = 0;
		TotalPoints = 0;
		TotalSeconds = 60;
		StartCoroutine(SpawnFurniture());
	}

	void Update()
	{
        pointsText.text = Mathf.Lerp(float.Parse(pointsText.text), TotalPoints, Time.deltaTime).ToString("F0");
		timerText.text = TotalSeconds.ToString("F0") + "s";

        if (!isGameRunning)
        {
            return;
        }

		currentTimer -= Time.deltaTime;

		if (currentTimer < 0)
		{
			TotalSeconds--;

            if (TotalSeconds <= 0)
            {
                isGameRunning = false;
                StopTheGame();
            }
			currentTimer = 1f;
		}

	}

	void StopTheGame()
	{
        StopAllCoroutines();
        foreach (FurnitureItem furnitureItem in spawnedFurnitureItems)
        {
            Destroy(furnitureItem.gameObject);
        }
        spawnedFurnitureItems.Clear();
	}

	public void CleanedFurniture(Vector3 position, float points)
	{
		Debug.Log("Cleaned Furniture");
		TotalPoints += points;
		TotalFurniture++;
		SpawnPointsAfterDeath(position, points);
	}

	public void SpawnPointsAfterDeath(Vector3 position, float points)
	{
		GameObject pointsAfterDeath = Instantiate(pointsAfterDeathPrefab, position, Quaternion.identity);
		pointsAfterDeath.GetComponent<PointsAfterDeath>().points = points;
	}

	public void AddFurnitureItem(FurnitureItem furnitureItem)
	{
		spawnedFurnitureItems.Add(furnitureItem);
	}

	public void RemoveFurnitureItem(FurnitureItem furnitureItem)
	{
		spawnedFurnitureItems.Remove(furnitureItem);
	}

	IEnumerator SpawnFurniture()
	{
		while (true)
		{
			Debug.Log("Spawning Furniture");
			int area = Random.Range(0, spawnAreas.Count);
			SpawnArea randomSpawnArea = spawnAreas[area];
			FurnitureItem spawnedFurnitureItem = randomSpawnArea.Spawn(furnitureItemPrefab, gameObject);
			spawnedFurnitureItem.dustMeOffGame = this;
			spawnedFurnitureItem.whereDidSpawn = area;
			yield return new WaitForSeconds(spawnRate);
		}
	}
}
