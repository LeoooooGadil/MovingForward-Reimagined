using UnityEngine;

public class SettingsButtonHider : MonoBehaviour
{
	public GameObject SettingsButton;
	public bool hideButton = true;


	private void Start()
	{
		HideUnHideButton();
	}

	private void Update()
	{
		if (TutorialManager.instance == null) return;

		if (TutorialManager.instance.GetPhaseState() == 3)
		{
			hideButton = false;
		}
		else
		{
			hideButton = true;
		}

		HideUnHideButton();
	}

	private void HideUnHideButton()
	{
		if (hideButton)
		{
			SettingsButton.SetActive(false);
		}
		else
		{
			SettingsButton.SetActive(true);
		}
	}
}
