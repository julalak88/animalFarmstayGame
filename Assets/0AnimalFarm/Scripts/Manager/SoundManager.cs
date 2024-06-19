using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Hellmade.Sound;

public class SoundManager : SerializedMonoBehaviour
{
    public static SoundManager Ins;

    public Dictionary<string, AudioData> bgm;
    public Dictionary<string, AudioData> sfx;

    Audio currentBGM;

    private void Awake() {
        Ins = this;
        DontDestroyOnLoad(transform.gameObject);
    }

    public void PlayBGM(string soundName) {
        AudioData audioData = bgm[soundName];
        if (audioData.audio == null) {
            int audioID = EazySoundManager.PlayMusic(audioData.audioClip, audioData.volume, true, true, 2, .2f);
            audioData.audio = EazySoundManager.GetAudio(audioID);
        } else if (audioData.audio != null && audioData.audio.Paused) {
            audioData.audio.Resume();
        } else if(audioData.audio != null && currentBGM != audioData.audio) {
            currentBGM.Pause();
            audioData.audio.Play();
        }
        currentBGM = audioData.audio;
    }

    public void PauseBGM() {
        currentBGM.Pause();
    }

    public void PlaySFX(string soundName, float ratioVolume = 1) {
        AudioData audioData = sfx[soundName];
        EazySoundManager.PlaySound(audioData.audioClip, audioData.volume * ratioVolume);
        //if (audioData.audio == null) {
        //int audioID = EazySoundManager.PlaySound(audioData.audioClip, audioData.volume * ratioVolume);
        //audioData.audio = EazySoundManager.GetAudio(audioID);
        //}else {
        //audioData.audio.Play();
        //}
    }
}

[System.Serializable]
public class AudioData
{
    [HideInInspector]
    public Audio audio;
    public AudioClip audioClip;
    [Range(0f, 1f)]
    public float volume = 1;
}

