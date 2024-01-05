using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private Sound[] allSounds;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        SetUpSounds();
    }

    private void SetUpSounds()
    {
        foreach (var item in allSounds)
        {
            item.source = gameObject.AddComponent<AudioSource>();
            item.source.clip = item.klip;
            item.source.volume = item.vol;
            item.source.loop = item.isLooping;
        }
    }

    public void MainkanSuara(string findSound)
    {
        Sound find = Array.Find(allSounds, s => s.namaSound == findSound);
        if(find != null)
        {
            find.source.Play();
        }
    }
}


[System.Serializable]
public class Sound
{
    public string namaSound;
    public AudioClip klip;

    [Range(0f , 1f)] public float vol;
    public bool isLooping;

    [HideInInspector] public AudioSource source;
}
