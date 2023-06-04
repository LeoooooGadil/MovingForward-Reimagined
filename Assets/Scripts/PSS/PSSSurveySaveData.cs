using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PSSSurveySaveData
{
	public Dictionary<string, Dictionary<string, SurveyResult>> surveyResults;

	public PSSSurveySaveData()
	{
		surveyResults = new Dictionary<string, Dictionary<string, SurveyResult>>();
	}

	public PSSSurveySaveData(PSSSurveySave save)
	{
		surveyResults = new Dictionary<string, Dictionary<string, SurveyResult>>();

		foreach (var player in save.surveyResults)
		{
			surveyResults.Add(player.Key, new Dictionary<string, SurveyResult>());

			foreach (var survey in player.Value)
			{
				surveyResults[player.Key].Add(survey.Key, survey.Value);
			}
		}
	}
}
