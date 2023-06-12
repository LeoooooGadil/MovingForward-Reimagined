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

	private void LoadData()
	{
        string _playerName = ProfileManager.instance.GetUserName();
		int _playerAge = int.Parse(ProfileManager.instance.GetAge());
        int _playerLevel = ExperienceManager.instance.GetExperienceSave().level;
        int _playerPSSScore = PSSAccess.

        PlayersName = _playerName;
        PlayersLevel = _playerLevel;
        PlayersAge = _playerAge;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		AudioManager.instance.PlaySFX("PopClick");
	}
}
