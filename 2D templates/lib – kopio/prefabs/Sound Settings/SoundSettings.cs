using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSettings : MonoBehaviour
{
    //Place to games EventSystem or similar gameobject

    public List<SoundTarget> soundTargets = new List<SoundTarget>();
    // Start is called before the first frame update
    void Start()
    {
        soundTargets.Add(new SoundTarget("Music", false, 1));
        soundTargets.Add(new SoundTarget("Ambience", false, 1));
        soundTargets.Add(new SoundTarget("Sound Effects", false, 1));
    }

    //Changes values of sound targets
    public void ChangeValues(string type, bool mute, float volume)
    {
        for(int i = 0; i < soundTargets.Count; i++)
        {
            if(soundTargets[i].name == type)
            {
                soundTargets[i].volume = volume;
                soundTargets[i].mute = mute;
            }
        }
    }
}

public class SoundTarget
{
    public string name;
    public bool mute;
    public float volume;

    public SoundTarget(string name, bool mute, float volume)
    {
        this.name = name;
        this.mute = mute;
        this.volume = volume;
    }
}
