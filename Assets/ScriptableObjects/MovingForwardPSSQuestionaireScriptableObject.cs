using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PSS Questionaire Manifest", menuName = "Moving Forward/Survey", order = 1)]
public class MovingForwardPSSQuestionaireScriptableObject : ScriptableObject
{
    [SerializeField]
    public List<QuestionaireQuestion> questions;
}

[System.Serializable]
public class QuestionaireQuestion
{
    public int qeustionNumber;
    public string question;
}

public enum QuestionaireAnswers
{
    veryOften,
    fairlyOften,
    sometimes,
    almostNever,
    never,
    none
}
