using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DailyTaskItem : MonoBehaviour
{
    public TMP_Text taskNameText;
    public TMP_Text taskPointsText;
    public CheckboxHandler checkboxHandler;
    public DailyTaskManager dailyTaskManager;

    public string taskName;
    public int taskPoints;

    public bool isCompleted = false;

    void Start()
    {
        taskNameText.text = taskName;
        taskPointsText.text = taskPoints.ToString() + " pts";
        checkboxHandler.SetChecked(isCompleted);
    }

    void Update()
    {
        // put middle line through the task name if it is completed
        if (isCompleted)
        {
            taskNameText.text = "<s>" + taskName + "</s>";
        }
        else
        {
            taskNameText.text = taskName;
        }
    }

    public bool IsCompleted()
    {
        return isCompleted;
    }

    public void SetCompleted(bool value)
    {
        isCompleted = value;
        dailyTaskManager.UpdateTask(taskName, value);
    }
}
