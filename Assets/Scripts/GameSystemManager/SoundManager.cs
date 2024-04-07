using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip audioClip;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    //public Slider _bgmSlider;
    //public Slider _effectSlider;

    public AudioSource Bgm;
    public List<AudioSource> Effect;

    private float SFX_vol = 0.5f;

    public Sound[] BgmSnds;
    public Sound[] EffectSnds;

    public void PlayBgm(string name)
    {
        Sound s = Array.Find(BgmSnds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            Bgm.clip = s.audioClip;
            Bgm.Play();
        }
    }

    public void PlayEffect(string name)
    {
        Sound s = Array.Find(EffectSnds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            AudioSource sfx = gameObject.AddComponent<AudioSource>();
            sfx.volume = SFX_vol;
            sfx.clip = s.audioClip;
            sfx.Play();
            Effect.Add(sfx);
        }
    }

    private void Awake()
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

    private void Start()
    {
        PlayBgm("Title");
    }

    private void Update()
    {
        foreach (var i in Effect)
        {
            if (!i.isPlaying)
            {
                Effect.Remove(i);
                Destroy(i);
                break;
            }
        }
    }

    public void BgmVolume(float volume)
    {
        Bgm.volume = volume;
    }

    public void EffectVolume(float volume)
    {
        SFX_vol = volume;
    }


    //public void BgmVolume()
    //{
    //    instance.BgmVolume(_bgmSlider.value);
    //}
    //
    //public void EffectVolume()
    //{
    //    instance.EffectVolume(_bgmSlider.value);
    //}
    //
    //public void SetVolume()
    //{
    //    _bgmSlider.value = instance.Bgm.volume;
    //    _effectSlider.value = instance.Effect.volume;
    //}
}