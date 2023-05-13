using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatisticsSave
{
	public long veryFirstTimePlayed;
	public long lastTimePlayed;
	public long lastTimeStartedPlaying;
    public Dictionary<string, TimeSpan> totalTimesPlayedPerDay;

	public GameStatisticsSave()
	{
		veryFirstTimePlayed = 0;
		lastTimePlayed = 0;
		lastTimeStartedPlaying = 0;
        totalTimesPlayedPerDay = new Dictionary<string, TimeSpan>();
	}

	public GameStatisticsSave(GameStatisticsSaveData _gameStatisticsSaveData)
	{
        veryFirstTimePlayed = _gameStatisticsSaveData.veryFirstTimePlayed;
        lastTimePlayed = _gameStatisticsSaveData.lastTimePlayed;
        lastTimeStartedPlaying = _gameStatisticsSaveData.lastTimeStartedPlaying;
        totalTimesPlayedPerDay = _gameStatisticsSaveData.totalTimesPlayedPerDay;
	}

}
