using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurveyButton : MonoBehaviour
{
	public QuestionaireAnswers answer;
	public PSSSurveyManager surveyManager;
    public bool isActive = true;

	private Button button;

	// Start is called before the first frame update
	void Start()
	{
		button = GetComponent<Button>();
		button.onClick.AddListener(AnswerTheQuestion);
	}

	void Update()
	{
        if (isActive)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
	}

	void AnswerTheQuestion()
	{
		surveyManager.AnswerTheQuestion(answer);
	}
}
