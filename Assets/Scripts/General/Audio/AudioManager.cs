using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance;

	private List<AudioSource> SFXAudioSources;
	private AudioSource MusicAudioSource;
	public AudioMixerGroup[] SFXAudioMixerGroups;
	public AudioMixerGroup[] MusicAudioMixerGroups;
	public MovingForwardAudioClipsObject audioClips;

	private float SFXVolume;
	private float MusicVolume;

	private int timesSfxHasBeenSame = 0;
	private bool played = true;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	AudioMixerGroup GetAudioMixerGroup(int type = 0)
	{
		// type 0 = SFX
		// type 1 = Music
		switch (type)
		{
			case 0:
#if UNITY_EDITOR || UNITY_STANDALONE
				return SFXAudioMixerGroups[0];
#elif UNITY_ANDROID
                    return SFXAudioMixerGroups[1];
#endif
			case 1:
#if UNITY_EDITOR || UNITY_STANDALONE
				return MusicAudioMixerGroups[0];
#elif UNITY_ANDROID
                    return MusicAudioMixerGroups[1];
#endif
			default:
				return null;
		}
	}

	void Start()
	{
		SFXAudioSources = new List<AudioSource>();
		MusicAudioSource = gameObject.AddComponent<AudioSource>();

		SFXVolume = PlayerPrefs.HasKey("sfxVolume") ? PlayerPrefs.GetFloat("sfxVolume") : 1f;
		MusicVolume = PlayerPrefs.HasKey("musicVolume") ? PlayerPrefs.GetFloat("musicVolume") : 1f;

		Debug.Log("SFX Volume: " + SFXVolume);
		Debug.Log("Music Volume: " + MusicVolume);
	}

	void Update()
	{
		float _SfxVolume = PlayerPrefs.HasKey("sfxVolume") ? PlayerPrefs.GetFloat("sfxVolume") : 1f;
		float _MusicVolume = PlayerPrefs.HasKey("musicVolume") ? PlayerPrefs.GetFloat("musicVolume") : 1f;

		if (SFXVolume != _SfxVolume)
		{
			timesSfxHasBeenSame = 0;
			played = false;
			SFXVolume = _SfxVolume;
			foreach (AudioSource audioSource in SFXAudioSources)
			{
				audioSource.volume = SFXVolume;
			}
		}
		else
		{
			timesSfxHasBeenSame++;
			if (timesSfxHasBeenSame > 2 && !played)
			{
				PlaySFX("ButtonClick");
				played = true;
			}
		}

		if (MusicVolume != _MusicVolume)
		{
			MusicVolume = _MusicVolume;
			MusicAudioSource.volume = MusicVolume;
		}
	}

	public void PlaySFX(MovingForwardAudioClipsObject.MovingForwardAudioClip clip, float volume = 1.0f)
	{
		// create a new audio source
		AudioSource newAudioSource = gameObject.AddComponent<AudioSource>();
		newAudioSource.clip = clip.clip;

		// set the audio mixer group
		newAudioSource.outputAudioMixerGroup = GetAudioMixerGroup();

		// set the volume
		newAudioSource.volume = clip.volume * volume * SFXVolume;

		// add the audio source to the list
		SFXAudioSources.Add(newAudioSource);

		// play the clip
		newAudioSource.Play();

		// destroy the audio source when the clip is done playing
		// and remove it from the list
		StartCoroutine(DestroyAudioSource(newAudioSource));
	}

	IEnumerator DestroyAudioSource(AudioSource audioSource)
	{
		yield return new WaitForSeconds(audioSource.clip.length);
		SFXAudioSources.Remove(audioSource);
		Destroy(audioSource);
	}

	public void PlaySFX(string clipName, float volume = 1.0f)
	{

		// find the clip
		MovingForwardAudioClipsObject.MovingForwardAudioClip clip = audioClips.SFXClips.Find(x => x.name == clipName);

		// play the clip
		PlaySFX(clip, volume);
	}

	public void PlayMusic(MovingForwardAudioClipsObject.MovingForwardAudioClip clip, float volume = 1.0f, bool fade = false)
	{
		// set the clip
		MusicAudioSource.clip = clip.clip;

		// set the audio mixer group
		MusicAudioSource.outputAudioMixerGroup = GetAudioMixerGroup();

		// set the loop
		MusicAudioSource.loop = true;

		// play the clip
		MusicAudioSource.Play();

		if (fade)
		{
			StartCoroutine(FadeInMusic(volume));
		}
		else
		{
			MusicAudioSource.volume = volume;
		}
	}

	IEnumerator FadeInMusic(float volume = 1.0f)
	{
		while (MusicAudioSource.volume < 1)
		{
			MusicAudioSource.volume += 0.1f;
			yield return new WaitForSeconds(0.1f);
		}
		MusicAudioSource.volume = volume;
	}

	IEnumerator FadeOutMusic()
	{
		while (MusicAudioSource.volume > 0)
		{
			MusicAudioSource.volume -= 0.1f;
			yield return new WaitForSeconds(0.1f);
		}
		MusicAudioSource.Stop();
	}

	public void ChangeMusic(string clipName, float volume = 1.0f)
	{
		MovingForwardAudioClipsObject.MovingForwardAudioClip clip = audioClips.MusicClips.Find(x => x.name == clipName);

		if (MusicAudioSource.clip != clip.clip || !MusicAudioSource.isPlaying)
		{
			StartCoroutine(ChangeMusicCoroutine(clipName, volume));
		}
	}

	IEnumerator ChangeMusicCoroutine(string clipName, float volume = 1.0f)
	{
		if (MusicAudioSource.isPlaying)
		{
			StartCoroutine(FadeOutMusic());
			yield return new WaitForSeconds(0.5f);
		}
		PlayMusic(clipName, volume * MusicVolume, MusicAudioSource.isPlaying);
	}

	public void PlayMusic(string clipName, float volume = 1.0f, bool fade = false)
	{
		// find the clip
		MovingForwardAudioClipsObject.MovingForwardAudioClip clip = audioClips.MusicClips.Find(x => x.name == clipName);

		// play the clip
		PlayMusic(clip, volume, fade);
	}

	internal void StopMusic()
	{
		StartCoroutine(FadeOutMusic());
	}
}
