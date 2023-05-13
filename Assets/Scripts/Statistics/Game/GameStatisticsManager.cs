using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatisticsManager : MonoBehaviour
{
	public GameStatisticsManager Instance { get; private set; }

	public TimeSpan currentDurationOfPlay;
    private float currentTimer = 0f;
    private bool isTimerRunning = false;

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			Destroy(this);
		}

		
	}

	void Start()
	{
        bool isVeryFirstTimePlayed = GameStatisticsStorage.isVeryFirstTimePlayed();

		if (isVeryFirstTimePlayed)
		{
			GameStatisticsStorage.VeryFirstTimePlayed(System.DateTimeOffset.UtcNow.ToUnixTimeSeconds());
		}

		GameStatisticsStorage.LastTimeStartedPlaying(System.DateTimeOffset.UtcNow.ToUnixTimeSeconds());
        isTimerRunning = true;
    }

	void Update()
	{
        if (isTimerRunning)
        {
            currentTimer += Time.deltaTime;
            currentDurationOfPlay = TimeSpan.FromSeconds(Time.deltaTime);

            if (currentTimer >= 300f) 
            {
                currentTimer = 0f;
                GameStatisticsStorage.TotalTimesPlayedPerDay(System.DateTime.Now.ToString("dd/MM/yyyy"), currentDurationOfPlay);
                GameStatisticsStorage.LastTimePlayed(System.DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            }
        }
	}

    public void SaveGameStatisticsStorage()
    {
        GameStatisticsStorage.LastTimePlayed(System.DateTimeOffset.UtcNow.ToUnixTimeSeconds());
    }

    void OnApplicationQuit()
    {
        SaveGameStatisticsStorage();
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveGameStatisticsStorage();
        }
    }
}
