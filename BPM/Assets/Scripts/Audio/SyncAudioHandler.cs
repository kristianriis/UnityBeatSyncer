using UnityEngine;

namespace Audio
{
    public class SyncAudioHandler : MonoBehaviour
    {
        private AudioMapping audioMapping;
        private AudioObject curActiveAudioObject;
        private double nextBeatTime;
        private int beatCount = 0;
        private int barCount = 0;

        //Set your bpm here man 
        private float mainBpm = 140;

        private void Start()
        {
            audioMapping = AudioManager.Instance.audioMapping;
            double beatInterval = 60d / mainBpm / 2;
            nextBeatTime = AudioSettings.dspTime + 2; //prep time for PlayScheduled
            StartNextSound(AudioType.Test4, 0);
        }

        private void Update()
        {
            double dspTime = AudioSettings.dspTime;
            double lookahead = 0.2; // schedule 200ms for framerate

            while (nextBeatTime < dspTime + lookahead)
            {
                TriggerSound(); 
                double beatInterval = 60d / mainBpm / 2;
                nextBeatTime += beatInterval;
            }
        }

        private void TriggerSound()
        {
            double scheduledTime = nextBeatTime;

            if (barCount % 4 == 0 && beatCount == 0)
            {
                StartNextSound(AudioType.Test4, scheduledTime);
            }

            if (barCount % 8 == 0 && beatCount == 0)
            {
                StartNextSound(AudioType.Test5, scheduledTime);
            }

            //downbeats
            if (beatCount % 2 == 0)
            {
                StartNextSound(AudioType.Test1, scheduledTime);

                if (beatCount == 2 || beatCount == 6)
                {
                    StartNextSound(AudioType.Test2, scheduledTime);
                }
            }
            else //offbeats
            {
                StartNextSound(AudioType.Test3, scheduledTime);
            }

            // advance 
            beatCount = (beatCount + 1) % 8;
            if (beatCount == 0)
            {
                barCount++;
            }
        }

        private void StartNextSound(AudioType type, double scheduledTime)
        {
            AudioInfo audioInfo = audioMapping.GetAudioType(type);
            if (audioInfo == null)
            {
                Debug.LogWarning($"AudioSystem: Audio Type {type} not found in AudioMapping list.");
                return;
            }

            AudioObject audioObject = AudioManager.Instance.GetAudioObject();

            if (audioObject == null)
            {
                Debug.LogWarning($"AudioSystem: audioobject not loaded");
                return;
            }

            //don't that here - but then again you're hackerman 
            audioObject.audioSource.outputAudioMixerGroup = audioInfo.audioMixerGroup;
            audioObject.audioSource.volume = audioInfo.volume;
            audioObject.isPlaying = true;
            audioObject.PlayScheduled(audioInfo.audioClip, scheduledTime);
        }

        //could use this for calculating music beats if you do full tracks 
        private double CalculateTimeToNextBeat(float bpm)
        {
            double barDuration = 60d / bpm * 4;
            double remainder =
                ((double)curActiveAudioObject.audioSource.timeSamples /
                 curActiveAudioObject.audioSource.clip.frequency) % (barDuration);
            double nextBarTime = AudioSettings.dspTime + barDuration - remainder;
            return nextBarTime;
        }
    }
}