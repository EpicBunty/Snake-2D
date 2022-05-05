using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance { get { return instance; } }

    [SerializeField] private AudioSource soundMusic;

    [SerializeField] private AudioSource soundEffect;

    [SerializeField] private SoundType[] sounds;
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
        //PlayBgMusic(Sounds.BgMusic);
    }

    public void PlayBgMusic(Sounds sound)
    {
        AudioClip clip = getSoundClip(sound);
        if (clip != null)
        {
            soundMusic.clip = clip;
            soundMusic.Play();
        }
        else
        {
            Debug.LogError("Clip not found for soundtype: " + sound);
        }
    }

    public void Play(Sounds sound)
    {
        AudioClip clip = getSoundClip(sound);
        if (clip != null)
        {
            soundEffect.clip = clip;
            soundEffect.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError("Clip not found for soundtype: " + sound);
        }
    }

    public void Play(Sounds sound, float volume)
    {
        AudioClip clip = getSoundClip(sound);
        if (clip != null)
        {
            soundEffect.clip = clip;
            soundEffect.PlayOneShot(clip, volume);
        }
        else
        {
            Debug.LogError("Clip not found for soundtype: " + sound);
        }
    }

    private AudioClip getSoundClip(Sounds sound)
    {
        SoundType sounditem = Array.Find(sounds, item => item.soundType == sound);
        if (sounditem != null) return sounditem.soundClip;
        return null;
    }
}

[Serializable]
public class SoundType
{
    public Sounds soundType;
    public AudioClip soundClip;
}

public enum Sounds
{
    ButtonClick,
    ButtonBack,
    GameStart,
    LobbyMusic,
    BgMusic,
    Collectible1,
    Collectible2,
    FightMusic,
}
