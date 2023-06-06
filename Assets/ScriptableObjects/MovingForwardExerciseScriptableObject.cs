using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Exercise Manifest", menuName = "Moving Forward/Exercise", order = 1)]
public class MovingForwardExerciseScriptableObject : ScriptableObject
{
    [SerializeField]
    public List<Stretching> Stretchings;

    [SerializeField]
    public List<Exercise> Exercises;

}

[System.Serializable]
public class Stretching
{
	public string name;
	public List<string> steps;
}

[System.Serializable]
public class Exercise
{
    public string name;
    public List<string> steps;
}
