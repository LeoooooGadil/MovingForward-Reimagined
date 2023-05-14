using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateProfileManager : MonoBehaviour
{
	public static CreateProfileManager instance;
    
    public Sprite createProfileButtonEnabled;
	public Sprite createProfileButtonDisabled;

	public TMP_InputField usernameInputField;
	public TMP_InputField ageInputField;
    public GameObject createProfileButtonGameObject;

	ProfileManagerSave profileManagerSave;

	private Button createProfileButton;
    private Image createProfileButtonImage;
	private string saveFileName = "profileManagerSave";

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	void Start()
	{
        createProfileButton = createProfileButtonGameObject.GetComponent<Button>();
        createProfileButtonImage = createProfileButtonGameObject.GetComponent<Image>();

		LoadProfile();

		createProfileButton.onClick.AddListener(OnCreateProfileButtonClicked);
	}

	void Update()
	{
        if ((usernameInputField.text != "") && (ageInputField.text != ""))
        {
            createProfileButton.interactable = true;
            createProfileButtonImage.sprite = createProfileButtonEnabled;
            
        }

        else
        {
            createProfileButton.interactable = false;
            createProfileButtonImage.sprite = createProfileButtonDisabled;
        }
	}

	void LoadProfile()
	{
		ProfileManagerSaveData profileManagerSaveData = SaveSystem.Load(saveFileName) as ProfileManagerSaveData;

		if (profileManagerSaveData != null)
		{
			profileManagerSave = new ProfileManagerSave(profileManagerSaveData);
            usernameInputField.text = profileManagerSave.username;
			ageInputField.text = profileManagerSave.age;
		}
		else
		{
			profileManagerSave = new ProfileManagerSave();
		}
	}

	public void OnCreateProfileButtonClicked()
	{
		if ((usernameInputField.text != "") && ageInputField.text != "")
		{
			CreateProfile(usernameInputField.text);
			CreateProfileAge(ageInputField.text);
		}
	}

	public void CreateProfile(string username)
	{
		
		profileManagerSave.setUsername(username);
		SaveProfile();

		AudioManager.instance.PlaySFX("ButtonClick");

        LevelManager.instance.ChangeScene("Game", true, SceneTransitionMode.Slide, false);
	}

	public void CreateProfileAge(string age)
	{

		profileManagerSave.setAge(age);
		SaveProfile();

        LevelManager.instance.ChangeScene("Game");

	}

	void SaveProfile()
	{
		ProfileManagerSaveData profileManagerSaveData = new ProfileManagerSaveData(profileManagerSave);
		SaveSystem.Save(saveFileName, profileManagerSaveData);
	}
}
