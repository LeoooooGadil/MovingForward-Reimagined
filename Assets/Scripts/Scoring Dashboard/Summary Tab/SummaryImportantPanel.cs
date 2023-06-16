using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummaryImportantPanel : MonoBehaviour
{
	public Text PlayerName;
	public Text PlayerAge;

	public string playerName;
	public string playerAge;
    public int playerLevel;

    private float currentTimer;

	// void Start()
	// {
	// 	LoadPlayerData();
	// }

	// void Update()
	// {

    //     if (currentTimer < 0.5)
    //     {
    //         currentTimer += Time.deltaTime;
    //     }
    //     else
    //     {
    //         currentTimer = 0;
    //         LoadPlayerData();
    //     }

    //     PlayerName.text = "(" + playerLevel + ") " + playerName;
    //     PlayerAge.text = playerAge;
	// }

	// void LoadPlayerData()
	// {
	// 	string _playerName = ProfileManager.instance.GetUserName();
	// 	int _playerAge = int.Parse(ProfileManager.instance.GetAge());
    //     int _playerLevel = ExperienceManager.instance.GetExperienceSave().level;

	// 	playerName = _playerName;
	// 	playerAge = _playerAge.ToString();
    //     playerLevel = _playerLevel;
	// }
}
