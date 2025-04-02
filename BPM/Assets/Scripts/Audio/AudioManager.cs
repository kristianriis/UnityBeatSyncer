using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        public AudioMapping audioMapping; 
        public AudioObject audioObject;
        private List<AudioObject> audioObjectPool = new List<AudioObject>();
        private GameObject audioObjectPoolRoot;
        private SyncAudioHandler syncAudioHandler; //could trigger it all from here
        
        public static AudioManager Instance { get; private set; }
    
        //making you a singleton - you could do DI or whatever
        //Also you could do an interface for actually triggering sounds from here instead of the indiviudal handlers 
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject); 
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        
            audioObjectPoolRoot = new GameObject("AudioObjectPool");
            CreateAudioObjectPool();
        }
    
        private void CreateAudioObjectPool()
        {
            for (int i = 0; i < 20; i++)
            {
                AudioObject obj = Instantiate(audioObject, audioObjectPoolRoot.transform);
                audioObjectPool.Add(obj);
            }
        }

        public AudioObject GetAudioObject()
        {
            foreach (var obj in audioObjectPool)
            {
                if (obj.isPlaying == false)
                {
                    obj.gameObject.SetActive(true);
                    return obj; 
                }
            }

            return null; 
        }
    }
}