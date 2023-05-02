using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{
	public static PopUpManager instance;
    public GameObject BackDrop;
    public List<PopUps> PopUps = new();

    private GameObject activePopUp;

	void Awake()
	{
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
	}

    public WordlePopUpController ShowWordleHintPopUp()
    {
        BackDrop.SetActive(true);
        activePopUp = Instantiate(PopUps.Find(x => x.Type == PopUpType.WordleHint).PopUpPrefab, GetComponentInChildren<Canvas>().transform);
        activePopUp.GetComponent<WordlePopUpController>().closingAction = () => ClosingPopUp();
        return activePopUp.GetComponent<WordlePopUpController>();
    }

    internal void ClosingPopUp()
    {
        BackDrop.SetActive(false);
        Destroy(activePopUp);
        AudioManager.instance.PlaySFX("CloseClick");
    }
}