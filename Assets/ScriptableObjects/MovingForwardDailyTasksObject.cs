using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Daily Tasks Manifest", menuName = "Moving Forward/Daily Tasks", order = 1)]
public class MovingForwardDailyTasksObject : ScriptableObject
{

    [SerializeField]
    public List<MoivngForwardDailyTask> Tasks;

    [System.Serializable]

    [IncludeInSettings(true)]
	public class MoivngForwardDailyTask
	{
        [SerializeField]
        public string name;

        [SerializeField]
        public int points = 3;

        [SerializeField]
        public DailyTaskPriority priority = DailyTaskPriority.Low;
	}

    public enum DailyTaskPriority { Low, Medium, High }   
}
