using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSSSurveySave
{
	public Dictionary<string, Dictionary<string, SurveyResult>> surveyResults;

	public PSSSurveySave()
	{
		surveyResults = new Dictionary<string, Dictionary<string, SurveyResult>>();
	}

	public PSSSurveySave(PSSSurveySaveData data)
	{
		surveyResults = new Dictionary<string, Dictionary<string, SurveyResult>>();

		foreach (var player in data.surveyResults)
		{
			surveyResults.Add(player.Key, new Dictionary<string, SurveyResult>());

			foreach (var survey in player.Value)
			{
				surveyResults[player.Key].Add(survey.Key, survey.Value);
			}
		}
	}

	public void AddSurveyResult(SurveyResult surveyResult)
	{
		string todayString = DateTime.Today.ToString("dd/MM/yyyy");

		if (surveyResults.ContainsKey(todayString))
		{
			if (surveyResults[todayString].ContainsKey(surveyResult.timestamp.ToString()))
			{
				surveyResults[todayString][surveyResult.timestamp.ToString()] = surveyResult;
			}
			else
			{
				surveyResults[todayString].Add(surveyResult.timestamp.ToString(), surveyResult);
			}
		}
		else
		{
			Dictionary<string, SurveyResult> surveyDictionary = new Dictionary<string, SurveyResult>();
			surveyDictionary.Add(surveyResult.timestamp.ToString(), surveyResult);
			surveyResults.Add(todayString, surveyDictionary);
		}
	}

	internal SurveyResult GetLastSurvey()
	{
		Dictionary<string, SurveyResult> surveyDictionary = new Dictionary<string, SurveyResult>();

		foreach (KeyValuePair<string, Dictionary<string, SurveyResult>> survey in surveyResults)
		{
			foreach (KeyValuePair<string, SurveyResult> surveyResult in survey.Value)
			{
				surveyDictionary.Add(surveyResult.Key, surveyResult.Value);
			}
		}

		SurveyResult lastSurvey = new SurveyResult();

		// get last survey

		foreach (KeyValuePair<string, SurveyResult> survey in surveyDictionary)
		{
			if (survey.Value.timestamp > lastSurvey.timestamp)
			{
				lastSurvey = survey.Value;
			}
		}

		return lastSurvey;
	}

	internal List<int> GetLast2Survey()
	{

        Dictionary<string, SurveyResult> surveyDictionary = new Dictionary<string, SurveyResult>();

        foreach (KeyValuePair<string, Dictionary<string, SurveyResult>> survey in surveyResults)
        {
            foreach (KeyValuePair<string, SurveyResult> surveyResult in survey.Value)
            {
                surveyDictionary.Add(surveyResult.Key, surveyResult.Value);
            }
        }

        List<SurveyResult> last2Survey = new List<SurveyResult>();

        if(surveyDictionary.Count > 1)
        {
            // get the recent 2 items
			List<SurveyResult> surveyList = new List<SurveyResult>();

			foreach (KeyValuePair<string, SurveyResult> survey in surveyDictionary)
			{
				surveyList.Add(survey.Value);
			}

			surveyList.Sort((x, y) => y.timestamp.CompareTo(x.timestamp));

			last2Survey.Add(surveyList[0]);
			last2Survey.Add(surveyList[1]);
        }
        else
        {
            foreach (KeyValuePair<string, SurveyResult> survey in surveyDictionary)
            {
                last2Survey.Add(survey.Value);
            }

            last2Survey.Add(new SurveyResult());
        }

        List<int> last2Scores = new List<int>();

        foreach (SurveyResult survey in last2Survey)
        {
            last2Scores.Add(survey.totalScore);
        }

		Debug.Log("Last 2 Scores: " + last2Scores[0] + " " + last2Scores[1]);

        return last2Scores;
	}
}

[System.Serializable]
public class SurveyResult
{
	public long timestamp;
	public List<QuestionaireAnswers> answers;
	public List<int> scores;
	public int totalScore;

	public SurveyResult()
	{
		timestamp = TimeStamp.GetTimeStamp();
		answers = new List<QuestionaireAnswers>();
		scores = new List<int>();
		totalScore = 0;
	}
}
