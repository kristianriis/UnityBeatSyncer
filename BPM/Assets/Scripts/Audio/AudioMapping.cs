using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public enum AudioType
    {
        None = 0, 
        Test1 = 1, 
        Test2 = 2,
        Test3 = 3,
        Test4 = 4,
        Test5 = 5,
    }
    
    [CreateAssetMenu(fileName = "AudioMapping", menuName = "ScriptableObjects/AudioMapping", order = 1)]
    public class AudioMapping : ScriptableObject
    {
        [SerializeField]
        public List<AudioInfo> audioMappingList = new List<AudioInfo>(); 
    
        public AudioInfo GetAudioType(AudioType audioType)
        {
            if (audioMappingList != null && audioMappingList.Count > 0)
            {
                AudioInfo value = audioMappingList.Find(x => x.audioType == audioType);
                return value;
            }

            return null; 
        }
    }
}