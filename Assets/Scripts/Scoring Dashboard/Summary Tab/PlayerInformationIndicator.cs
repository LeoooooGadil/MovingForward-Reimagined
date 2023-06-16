using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerInformationIndicator : MonoBehaviour, IPointerClickHandler
{
	public Text PlayersNameText;
	public Text belowText;

	public string UpperText;
	public string LowerText;

	public string PlayersName;
	public int PlayersLevel;
	public string PlayersPSSScore;

	public int PlayersAge;
	public string PlayersScoreDifference;

	bool isShowPSSScore = true;

	void Start()
	{
		LoadData();
	}

	void Update()
	{
		UpdateScore();
	}

	void UpdateScore()
	{
		if (isShowPSSScore)
		{
			PlayersNameText.text = PlayersPSSScore;
			belowText.text = PlayersScoreDifference;
		}
		else
		{
			PlayersNameText.text = PlayersName;
			belowText.text = PlayersAge.ToString();
		}
	}

	private void LoadData()
	{
		string _playerName = ProfileManager.instance.GetUserName();
		int _playerAge = int.Parse(ProfileManager.instance.GetAge());
		int _playerLevel = ExperienceManager.instance.GetExperienceSave().level;
		List<int> _playerPSSScore = PSSAccess.GetLastTwoScores();

		PlayersName = _playerName;
		PlayersLevel = _playerLevel;
		PlayersAge = _playerAge;
		PlayersPSSScore = _playerPSSScore[0].ToString() + " (" + GetEquivalent(_playerPSSScore[0]) + ")";
		int _playerPSSScoreDifference = _playerPSSScore[0] - _playerPSSScore[1];
		// if minus, add a minus sign and add + to the front
		if (_playerPSSScoreDifference < 0)
		{
			PlayersScoreDifference = "-" + Mathf.Abs(_playerPSSScoreDifference).ToString();
		}
		else if (_playerPSSScoreDifference == 0)
		{
			PlayersScoreDifference = _playerPSSScoreDifference.ToString();
		}
		else
		{
			PlayersScoreDifference = "+" + _playerPSSScoreDifference.ToString();
		}
	}

	string GetEquivalent(int score)
	{
		if (score >= 0 && score <= 13)
		{
			return "Low stress";
		}
		else if (score >= 14 && score <= 26)
		{
			return "Moderate stress";
		}
		else if (score >= 27 && score <= 40)
		{
			return "High perceived stress";
		}
		else
		{
			return "Error";
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		AudioManager.instance.PlaySFX("PopClick");
		isShowPSSScore = !isShowPSSScore;

		if (isShowPSSScore) OnScreenNotificationManager.instance.CreateNotification("Changed to Player's PSS Score", OnScreenNotificationType.Info);
		else OnScreenNotificationManager.instance.CreateNotification("Changed to Player's Information", OnScreenNotificationType.Info);
	}
}
