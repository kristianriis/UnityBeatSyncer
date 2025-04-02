using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    //Add whatever you need in here
    [System.Serializable]
    public class AudioInfo 
    {
        [SerializeField]
        public AudioType audioType;
        [SerializeField]
        public AudioClip audioClip;
        [SerializeField]
        public AudioMixerGroup audioMixerGroup;
        [SerializeField]
        public float volume; 
    }
}