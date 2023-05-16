using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroductionManager : MonoBehaviour
{
	public static IntroductionManager instance;

	public GameObject[] panels;
	public int currentPanel = 0;

	public Button nextButton;
	public Button backButton;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(this);
		}
	}

	void Start()
	{
		if(ProfileManager.instance.CheckIfNoPlayer())
		{
			currentPanel = 0;
		}
		else
		{
			currentPanel = 1;
			backButton.gameObject.SetActive(false);
		}

		AudioManager.instance.PlaySFX("ButtonClick");
		UpdatePanels();
        nextButton.onClick.AddListener(NextPanel);
        backButton.onClick.AddListener(BackPanel);
	}

	void UpdatePanels()
	{
		for (int i = 0; i < panels.Length; i++)
		{
			if (i == currentPanel)
			{
				panels[i].SetActive(true);
			}
			else
			{
				panels[i].SetActive(false);
			}
		}
	}

	void NextPanel()
	{
		AudioManager.instance.PlaySFX("PopClick");
        currentPanel++;
        UpdatePanels();
	}

    void BackPanel()
    {
		AudioManager.instance.PlaySFX("PopClick");
        currentPanel--;
        UpdatePanels();
    }
}
