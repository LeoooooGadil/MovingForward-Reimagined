using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
	private BoxCollider2D spawnArea;

	void Start()
	{
        spawnArea = GetComponent<BoxCollider2D>();
	}


	public FurnitureItem Spawn(GameObject prefab, GameObject parent)
	{
		// spawn prefab inside spawn area
        Vector3 spawnPosition = new Vector3(
            Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
            Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y),
            0
        );
        GameObject spawnedObject = Instantiate(prefab, spawnPosition, Quaternion.identity, parent.transform);

		return spawnedObject.GetComponent<FurnitureItem>();
	}
}
