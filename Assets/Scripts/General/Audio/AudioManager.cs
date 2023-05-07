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
	}

	public void PlaySFX(MovingForwardAudioClipsObject.MovingForwardAudioClip clip, float volume = 1.0f)
	{
		// create a new audio source
		AudioSource newAudioSource = gameObject.AddComponent<AudioSource>();
		newAudioSource.clip = clip.clip;

		// set the audio mixer group
		newAudioSource.outputAudioMixerGroup = GetAudioMixerGroup();

		// set the volume
		newAudioSource.volume = clip.volume * volume;

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
		PlaySFX(clip);
	}

	public void PlayMusic(MovingForwardAudioClipsObject.MovingForwardAudioClip clip, float volume = 1.0f, bool fade = false)
	{
		// set the clip
		MusicAudioSource.clip = clip.clip;

		// set the audio mixer group
		MusicAudioSource.outputAudioMixerGroup = GetAudioMixerGroup();

		// set the volume
		MusicAudioSource.volume = 0;

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


		// find the clip
		MovingForwardAudioClipsObject.MovingForwardAudioClip clip = audioClips.MusicClips.Find(x => x.name == clipName);

		// play the clip
		PlayMusic(clip);
	}

	IEnumerator ChangeMusicCoroutine(string clipName, float volume = 1.0f)
	{
		StartCoroutine(FadeOutMusic());
		yield return new WaitForSeconds(0.5f);
		PlayMusic(clipName, 0, true);
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
