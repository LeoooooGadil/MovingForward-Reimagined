using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WordlePopUpController : MonoBehaviour
{
	public Text wordleDefinitionText;
	public string wordleDefinition;
    public UnityAction closingAction;
    public UnityAction wordlePopUpOkButtonAction;

    public Button wordlePopUpOkButton;

	void Start()
	{
        wordlePopUpOkButton.onClick.AddListener(OnWordlePopUpOkButtonClicked);
        wordleDefinitionText.text = wordleDefinition;
	}

    public void OnWordlePopUpOkButtonClicked()
    {
        closingAction?.Invoke();
    }

    public void SetWordleDefinition(string wordleDefinition)
    {
        this.wordleDefinition = wordleDefinition.ToUpper();
    }
}
