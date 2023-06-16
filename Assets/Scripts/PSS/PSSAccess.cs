using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PSSAccess
{
	private static string saveFileName = "PSSSurveySave";

	static PSSSurveySave LoadPSSSurveySave()
	{
		PSSSurveySaveData pssSurveySaveData = SaveSystem.Load(saveFileName) as PSSSurveySaveData;

		if (pssSurveySaveData != null)
		{
			return new PSSSurveySave(pssSurveySaveData);
		}
		else
		{
			return new PSSSurveySave();
		}
	}

	public static bool CheckIfScoreIsEmpty()
	{
		PSSSurveySave pssSurveySave = LoadPSSSurveySave();

		if (pssSurveySave.surveyResults.Count == 0)
			return true;

		return false;
	}

	public static List<int> GetLastTwoScores()
	{
		List<PSSSurveyResult> lastTwoScores = new List<PSSSurveyResult>();

		PSSSurveySave pssSurveySave = LoadPSSSurveySave();

		return pssSurveySave.GetLast2Survey();
	}
}
