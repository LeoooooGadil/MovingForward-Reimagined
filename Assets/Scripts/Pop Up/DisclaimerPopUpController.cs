using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DisclaimerPopUpController : MonoBehaviour
{
    public Text disclaimerText;
    public string disclaimer;
    public UnityAction closingAction;
    public UnityAction additionalAction;
    public UnityAction disclaimerPopUpOkButtonAction;

    public Button disclaimerPopUpOkButton;

    void Start()
    {
        disclaimerPopUpOkButton.onClick.AddListener(OnDisclaimerPopUpOkButtonClicked);
        disclaimerText.text = disclaimer;
    }

    public void OnDisclaimerPopUpOkButtonClicked()
    {
        additionalAction?.Invoke();
        closingAction?.Invoke();
    }

    public void SetDisclaimer(string disclaimer)
    {
        this.disclaimer = disclaimer;
    }
}
