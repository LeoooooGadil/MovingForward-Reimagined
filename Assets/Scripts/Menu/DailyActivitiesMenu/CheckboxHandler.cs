using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CheckboxHandler : MonoBehaviour, IPointerClickHandler
{
    public Color32 checkedColor = new Color32(0, 0, 0, 255);
    public Color32 uncheckedColor = new Color32(0, 0, 0, 255);

    public GameObject checkbox;

    private bool isChecked = false;
    private Image checkboxImage;
    private DailyTaskItem dailyTaskItem;
    private DailyTaskAnimator dailyTaskAnimator;

    void Start()
    {
        checkboxImage = checkbox.GetComponent<Image>();
        dailyTaskAnimator = GetComponentInParent<DailyTaskAnimator>();
        dailyTaskItem = GetComponentInParent<DailyTaskItem>();
    }

    void Update()
    {
        isChecked = dailyTaskItem.IsCompleted();

        if (isChecked)
        {
            checkboxImage.color = checkedColor;
        }
        else
        {
            checkboxImage.color = uncheckedColor;
        }
    }

    public void OnClick()
    {
        AudioManager.instance.PlaySFX("AcceptClick");
        dailyTaskItem.SetCompleted(!isChecked);
    }

    public bool IsChecked()
    {
        return isChecked;
    }

    public void SetChecked(bool value)
    {
        isChecked = value;
    }

	public void OnPointerClick(PointerEventData eventData)
	{
		OnClick();
	}
}
