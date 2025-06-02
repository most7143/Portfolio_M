using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance = null;

    private Dictionary<SoundNames, AudioSource> sounds = new();

    public List<AudioSource> Sources;

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        for (int i = 0; i < Sources.Count; i++)
        {
            sounds.Add(EXEnum.Parse<SoundNames>(Sources[i].name), Sources[i]);
        }
    }

    public static SoundManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    public void Play(SoundNames name)
    {
        if (sounds.ContainsKey(name))
        {
            if (IgnoreSound(name))
                return;

            if (sounds[name].isPlaying)
            {
                sounds[name].Stop();
            }

            StopBySound(name);

            sounds[name].time = GetStartTime(name);
            sounds[name].Play();
        }
    }

    private void StopBySound(SoundNames name)
    {
        if (name == SoundNames.WeaponUpgrade)
        {
            sounds[SoundNames.Success].Stop();
        }
        else if (name == SoundNames.LevelUp)
        {
            if (sounds[SoundNames.WeaponUpgrade].isPlaying)
            {
                sounds[SoundNames.WeaponUpgrade].Stop();
            }
        }
    }

    private bool IgnoreSound(SoundNames name)
    {
        if (name == SoundNames.Dead)
        {
            if (sounds[SoundNames.LevelUp].isPlaying)
                return true;
        }

        return false;
    }

    private float GetStartTime(SoundNames name)
    {
        if (name == SoundNames.ButtonClick)
            return 0.25f;
        else if (name == SoundNames.Slash1 || name == SoundNames.Slash2 || name == SoundNames.Success
            || name == SoundNames.Dead)
            return 0.1f;
        else if (name == SoundNames.LevelUp)
            return 0.25f;

        return 0;
    }
}