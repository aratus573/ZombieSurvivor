using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// 用 AudioManager.Instance.PlaySFX( string sfxNAME ) 來播放音效
// 用 AudioManager.Instance.bgmSource.Stop() 來停止背景音樂
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // 照抄的
    public Sound[] backgroundMusic, soundEffects; 
    public AudioSource bgmSource, sfxSource; //Background Music Source and Sound Effect Source

    
    private void Awake() 
    {
        // 照抄的
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start() 
    {
        PlayMusic("Theme");
    }

    public void PlayMusic(string name)
    {
        // 在陣列backgroundMusic裡找名字等於name的物件
        Sound sound = Array.Find(backgroundMusic, x => x.name == name);
        if (sound == null)
        {
            Debug.Log("sound not found");
        }
        else
        {
            bgmSource.clip = sound.clip;
            bgmSource.Play();
        }
    }


    public void PlaySFX(string name)
    {
        // 在陣列soundEffects裡找名字等於name的物件
        Sound sound = Array.Find(soundEffects, x => x.name == name); 
        if (sound == null)
        {
            Debug.Log("sound not found");
        }
        else
        {
            sfxSource.PlayOneShot(sound.clip);
        }
    }


    public void ToggleBGM()
    {
        bgmSource.mute = !bgmSource.mute;
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    // 以下兩個可以有時間有需要再使用
    public void BGMVolume(float newVolume)
    {
        bgmSource.volume = newVolume;
    }
    public void SFXVolume(float newVolume)
    {
        sfxSource.volume = newVolume;
    }


}
