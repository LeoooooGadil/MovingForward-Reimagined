using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameStatisticsSaveData
{
	public long veryFirstTimePlayed;
	public long lastTimePlayed;
	public long lastTimeStartedPlaying;
	public Dictionary<string, TimeSpan> totalTimesPlayedPerDay;

    public Dictionary<string, int> totalTimesPlayed;

	public GameStatisticsSaveData(GameStatisticsSave _gameStatisticsSave)
	{
        veryFirstTimePlayed = _gameStatisticsSave.veryFirstTimePlayed;
        lastTimePlayed = _gameStatisticsSave.lastTimePlayed;
        lastTimeStartedPlaying = _gameStatisticsSave.lastTimeStartedPlaying;
        totalTimesPlayedPerDay = _gameStatisticsSave.totalTimesPlayedPerDay;
	}
}
