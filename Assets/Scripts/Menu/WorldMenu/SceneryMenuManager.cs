using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneryMenuManager : MonoBehaviour
{

	public GameObject sceneryCardPrefab;
	public GameObject sceneryCardContainer;

	private SceneryManager sceneryManager;
	private MovingForwardSceneryObject sceneryObject;

	void Awake()
	{
		sceneryManager = SceneryManager.instance;

		if (sceneryManager == null)
		{
			Debug.LogError("SceneryManager is null");
		}

		sceneryObject = sceneryManager.sceneryObject;
	}

	void OnEnable()
	{
		for (int i = 0; i < sceneryObject.sceneryList.Count; i++)
		{
			MovingForwardSceneryObject.MovingForwardScenery scenery = sceneryObject.sceneryList[i];

			GameObject sceneryCard = Instantiate(sceneryCardPrefab, sceneryCardContainer.transform);
			SceneryCard card = sceneryCard.GetComponent<SceneryCard>();
			card.cardName = scenery.label;
			card.sceneName = scenery.sceneName;
			card.thumbnail = scenery.thumbnail;
			card.isLocked = false;
			card.isCurrent = scenery.sceneName == sceneryManager.currentScenery.name;
			card.cardIndex = i;
		}
	}

	void SaveSceneryManager()
	{
		
	}
}
