using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsVolumeSlider : MonoBehaviour
{
	public float sfxVolume;
	public float musicVolume;

    public float currentSFXVolume;
    public float currentMusicVolume;

	public Slider sfxSlider;
	public Slider musicSlider;

	void Start()
	{
        LoadVolumes();

		sfxSlider.value = sfxVolume;
		musicSlider.value = musicVolume;
        currentSFXVolume = sfxVolume;
        currentMusicVolume = musicVolume;
	}

	void LoadVolumes()
	{
        sfxVolume = PlayerPrefs.HasKey("sfxVolume") ? PlayerPrefs.GetFloat("sfxVolume") : 1f;
        musicVolume = PlayerPrefs.HasKey("musicVolume") ? PlayerPrefs.GetFloat("musicVolume") : 1f;

        Debug.Log("sfxVolume: " + sfxVolume);
        Debug.Log("musicVolume: " + musicVolume);
	}

    void Update()
    {
        currentSFXVolume = sfxSlider.value;
        currentMusicVolume = musicSlider.value;

        if (currentSFXVolume != GetSFXVolume())
        {
            SetSFXVolume(currentSFXVolume);
            SaveVolumes();
        }

        if (currentMusicVolume != GetMusicVolume())
        {
            SetMusicVolume(currentMusicVolume);
            SaveVolumes();
        }
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    float GetSFXVolume()
    {
        return sfxVolume;
    }

    float GetMusicVolume()
    {
        return musicVolume;
    }

    public void SaveVolumes()
    {
        PlayerPrefs.Save();
    }
}
