using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayManager : MonoBehaviour
{
	public List<GameObject> dirts;
	public List<GameObject> trashs;
	public DailyChoreRoom dailyChoreRoom;

	public bool isDirtOverlayActive = false;
	public bool isTrashOverlayActive = false;

	void Update()
	{
		CheckChores();

		if (TutorialManager.instance.GetPhaseState() == 0)
		{
			HideAllOverlays();
		}
	}

	void HideAllOverlays()
	{
		HideDirtOverlay();
		HideTrashOverlay();
	}

	void CheckChores()
	{
		HideDirtOverlay();
		HideTrashOverlay();
		isDirtOverlayActive = false;
		isTrashOverlayActive = false;

		List<Chore> loadedUnfinishedChores = ChoresManager.instance.GetUnfinishedChores();

		if (loadedUnfinishedChores == null) return;

		if (loadedUnfinishedChores.Count == 0) return;



		// check each row if the chore is for this room.
		foreach (Chore chore in loadedUnfinishedChores)
		{
			if (chore.dailyChoreRoom == dailyChoreRoom)
			{
				if (chore.dailyChoreType == DailyChoreType.DustMeOff)
				{
					isDirtOverlayActive = true;
					ShowDirtOverlay();
				}
				else if (chore.dailyChoreType == DailyChoreType.ThrowMeOut)
				{
					isTrashOverlayActive = true;
					ShowTrashOverlay();
				}
			}
		}
	}

	void ShowDirtOverlay()
	{
		foreach (GameObject dirt in dirts)
		{
			dirt.SetActive(true);
		}
	}

	void ShowTrashOverlay()
	{
		foreach (GameObject trash in trashs)
		{
			trash.SetActive(true);
		}
	}

	void HideDirtOverlay()
	{
		foreach (GameObject dirt in dirts)
		{
			dirt.SetActive(false);
		}
	}

	void HideTrashOverlay()
	{
		foreach (GameObject trash in trashs)
		{
			trash.SetActive(false);
		}
	}
}
