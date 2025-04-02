using System;
using UnityEngine;

namespace  Audio
{
    public class AudioObject : MonoBehaviour, IDisposable
    {
        public AudioSource audioSource;
        public bool isPlaying; 

        public void PlayScheduled(AudioClip clip,double time)
        {
            audioSource.clip = clip; 
            audioSource.PlayScheduled(time);
            Invoke("OnSoundEnd", audioSource.clip.length + 1); 
        }

        public void Play()
        {
            //for none scheduled plays - ui, etc
            //audioSource.Play();
        }

        public void Stop()
        {
            //you get the point
        }

        public void ScheduledStop(double time)
        {
            //also
        }

        void OnSoundEnd()
        {
            isPlaying = false; 
            Dispose();
        }

        public void Dispose()
        {
            audioSource.Stop();
            audioSource.clip = null;
            gameObject.SetActive(false);
        }
    }
}