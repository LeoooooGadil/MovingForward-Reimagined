using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PSSSurveyManager : MonoBehaviour
{
	public static PSSSurveyManager instance;
	public MovingForwardPSSQuestionaireScriptableObject questionaireScriptableObject;

	public PSSSurveySave surveySave;

	public Text questionText;
	public Text questionNumberText;

	public List<SurveyButton> answersButton;

	private int currentQuestion = 0;
	public bool isAcceptingAnswers = true;
	public bool isSurveyDone = false;
	public List<QuestionaireAnswers> answers;
	public QuestionaireAnswers lastAnswer = QuestionaireAnswers.none;
	public int score = 0;
	public GameObject topPanel;
	public GameObject surveyPanel;
	public PSSSurveyResult surveyResult;
	public GameObject inThePastWeekText;
	List<int> scores = new List<int>();

	private string saveFileName = "PSSSurveySave";

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	void LoadSaveData()
	{
		PSSSurveySaveData loadedData = SaveSystem.Load("PSSSurveySave") as PSSSurveySaveData;

		if (loadedData != null)
		{
			surveySave = new PSSSurveySave();
			surveySave.surveyResults = loadedData.surveyResults;
		}
		else
		{
			surveySave = new PSSSurveySave();
		}
	}

	void Start()
	{
		LoadSaveData();

		bool isScoreEmpty = PSSAccess.CheckIfScoreIsEmpty();

		if (isScoreEmpty)
		{
			topPanel.SetActive(false);
		}

		answers = new List<QuestionaireAnswers>();

		foreach (var button in answersButton)
		{
			button.surveyManager = this;
		}

		surveyResult.gameObject.SetActive(false);

		AudioManager.instance.PlaySFX("ButtonClick");
		questionText.text = questionaireScriptableObject.questions[currentQuestion].question;
	}

	void Update()
	{
		if (isSurveyDone)
		{
			foreach (var button in answersButton)
			{
				button.isActive = false;
				button.gameObject.SetActive(false);
			}

			return;
		}

		if (lastAnswer == QuestionaireAnswers.none && !isSurveyDone)
		{
			foreach (var button in answersButton)
			{
				button.isActive = true;
			}

			return;
		}

		for (int i = 0; i < answersButton.Count; i++)
		{
			if (answersButton[i].answer == lastAnswer)
			{
				answersButton[i].isActive = true;
			}
			else
			{
				answersButton[i].isActive = false;
			}
		}
	}

	public void AnswerTheQuestion(QuestionaireAnswers answer)
	{
		if (!isAcceptingAnswers)
		{
			return;
		}

		lastAnswer = answer;
		StartCoroutine(AnswerCurrentQuestion(answer));
	}

	IEnumerator AnswerCurrentQuestion(QuestionaireAnswers answer)
	{
		isAcceptingAnswers = false;
		Debug.Log("Answered the question: " + answer);
		answers.Add(answer);
		currentQuestion++;
		AudioManager.instance.PlaySFX("PopClick");
		yield return new WaitForSeconds(1.5f);
		if (currentQuestion < questionaireScriptableObject.questions.Count)
		{
			if (currentQuestion > questionaireScriptableObject.questions.Count - 1)
			{
				questionNumberText.text = questionaireScriptableObject.questions.Count + "/" + questionaireScriptableObject.questions.Count;
			}
			else
			{
				questionNumberText.text = (currentQuestion + 1) + "/" + questionaireScriptableObject.questions.Count;
			}

			AudioManager.instance.PlaySFX("ButtonClick");
			questionText.text = questionaireScriptableObject.questions[currentQuestion].question;
			isAcceptingAnswers = true;
			lastAnswer = QuestionaireAnswers.none;
		}
		else
		{
			topPanel.SetActive(true);
			AudioManager.instance.PlaySFX("ButtonClick");
			isSurveyDone = true;
			inThePastWeekText.SetActive(false);
			questionNumberText.gameObject.SetActive(false);
			CalculateScore();
			SaveSurveyResult();

			Debug.Log("Survey is over");

			questionText.text = "Calculating your score.";
			yield return new WaitForSeconds(1f);
			questionText.text = "Calculating your score..";
			yield return new WaitForSeconds(1f);
			questionText.text = "Calculating your score...";
			yield return new WaitForSeconds(1f);

			surveyPanel.SetActive(false);
			surveyResult.gameObject.SetActive(true);
			surveyResult.SetResult(score);

			AudioManager.instance.PlaySFX("ButtonClick");
		}
	}

	public void SaveSurveyResult()
	{
		LoadSaveData();

		SurveyResult resultData = new SurveyResult();

		resultData.answers = answers;
		resultData.scores = scores;
		resultData.totalScore = score;

		surveySave.AddSurveyResult(resultData);

		SaveSurvey();
	}

	List<int> ConvertQuestionAnswersToScore()
	{
		List<int> _scores = new List<int>();

		// normal 4 = very often, 3 = fairly often, 2 = sometimes, 1 = almost never, 0 = never
		// reversed 0 = very often, 1 = fairly often, 2 = sometimes, 3 = almost never, 4 = never
		List<int> reversedQuestions = new List<int> { 3, 4, 6, 7 };

		for (int i = 0; i < answers.Count; i++)
		{
			int equivalent = GetEquivalent(answers[i], reversedQuestions.Contains(i));
			_scores.Add(equivalent);
		}

		scores = _scores;

		return scores;
	}

	int GetEquivalent(QuestionaireAnswers answer, bool isReversed)
	{
		int equivalent = 0;

		switch (answer)
		{
			case QuestionaireAnswers.never:
				equivalent = isReversed ? 4 : 0;
				break;
			case QuestionaireAnswers.almostNever:
				equivalent = isReversed ? 3 : 1;
				break;
			case QuestionaireAnswers.sometimes:
				equivalent = 2;
				break;
			case QuestionaireAnswers.fairlyOften:
				equivalent = isReversed ? 1 : 3;
				break;
			case QuestionaireAnswers.veryOften:
				equivalent = isReversed ? 0 : 4;
				break;
			default:
				break;
		}

		return equivalent;
	}

	public void CalculateScore()
	{
		int score = 0;

		List<int> scores = ConvertQuestionAnswersToScore();

		// log the scores
		for (int i = 0; i < scores.Count; i++)
		{
			Debug.Log("Question " + (i + 1) + " score: " + scores[i]);
		}

		for (int i = 0; i < scores.Count; i++)
		{
			score += scores[i];
		}

		this.score = score;
	}

	public void SaveSurvey()
	{
		PSSSurveySaveData surveySaveData = new PSSSurveySaveData(surveySave);
		SaveSystem.Save(saveFileName, surveySaveData);
	}
}
