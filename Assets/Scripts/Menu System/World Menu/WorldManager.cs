using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class WorldManager : MonoBehaviour
{
	public List<WorldItem> worldItems = new List<WorldItem>();
	public int currentWorld = 0;
	public WorldItem currentWorldItem;

	public ScrollRect scrollRect;

	void Start()
	{
		InitializeWorlds();

		try
		{
			currentWorld = SceneryManager.instance.sceneryManagerSave.currentSceneryIndex;
			currentWorldItem = worldItems[currentWorld];
		}
		catch
		{
			currentWorld = 0;
		}
		Debug.Log("Current World: " + currentWorld);

		ScrollToCurrentWorld();
	}

	void InitializeWorlds()
	{
		for (int i = 0; i < worldItems.Count; i++)
		{
			worldItems[i].worldIndex = i;
			worldItems[i].worldManager = this;
		}
	}

	// scroll to the current world
	public void ScrollToCurrentWorld()
	{
		float scrollValue = 0;

		for (int i = 0; i < currentWorld; i++)
		{
			scrollValue += 0.35f;
		}

		scrollRect.horizontalNormalizedPosition = scrollValue;
	}
}
