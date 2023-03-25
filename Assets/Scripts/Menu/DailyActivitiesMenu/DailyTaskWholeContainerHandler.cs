using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DailyTaskWholeContainerHandler : MonoBehaviour, IPointerClickHandler
{
    private DailyTaskItem dailyTaskItem;

    void Start()
    {
        dailyTaskItem = GetComponentInParent<DailyTaskItem>();
    }

    public void OnClick()
    {
        AudioManager.instance.PlaySFX("AcceptClick");
        dailyTaskItem.SetCompleted(!dailyTaskItem.IsCompleted());
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick();
    }
}
