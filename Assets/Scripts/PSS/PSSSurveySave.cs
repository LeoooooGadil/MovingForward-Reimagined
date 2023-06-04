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

        if(surveyResults.ContainsKey(todayString))
        {
            if(surveyResults[todayString].ContainsKey(surveyResult.timestamp.ToString()))
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
        timestamp = System.DateTimeOffset.Now.ToUnixTimeSeconds();
        answers = new List<QuestionaireAnswers>();
        scores = new List<int>();
        totalScore = 0;
    }
}
