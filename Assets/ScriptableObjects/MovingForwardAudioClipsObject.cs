using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Audio Clips Manifest", menuName = "Moving Forward/Audio", order = 1)]
public class MovingForwardAudioClipsObject : ScriptableObject
{
	[SerializeField]
	public List<MovingForwardAudioClip> SFXClips;
    [SerializeField]
    public List<MovingForwardAudioClip> MusicClips;

    [System.Serializable]

    [IncludeInSettings(true)]
	public class MovingForwardAudioClip
	{
        [SerializeField]
        public string name;

        [SerializeField]
		public AudioClip clip;  
        
        [SerializeField]
        public float volume = 1.0f;
	}
}


