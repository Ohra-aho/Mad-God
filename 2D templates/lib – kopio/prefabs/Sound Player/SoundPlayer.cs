using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public List<AudioClip> clips; //Clips to be played
    public bool ongoing; //true if sound is meant to be continious
    public bool instant; //true if sound is meant to play as gameobject appears 
    public float volume = 1.0f;
    public bool mute = false;

    public string type; //Used to find data from settings. Type and the name of the sound target have to match
    public string settings = "EventSystem";

    // Start is called before the first frame update
    void Start()
    {
        Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        Upkeep();
    }

    //Adds AudioSource and puts the first clip to it
    public void Invoke()
    {
        this.gameObject.AddComponent<AudioSource>();
        GetComponent<AudioSource>().clip = clips[0];
        if (ongoing)
        {
            GetComponent<AudioSource>().loop = true;
        }
        if(instant)
        {
            GetComponent<AudioSource>().Play();
        }
    }

    //Changes volume and mute according to public values
    public void Upkeep()
    {
        //Get sound targets from settings and adapt to them
        List<SoundTarget> soundTargets = GameObject.Find(settings).GetComponent<SoundSettings>().soundTargets;
        for(int i = 0; i < soundTargets.Count; i++)
        {
            if(soundTargets[i].name == type)
            {
                mute = soundTargets[i].mute;
                volume = soundTargets[i].volume;
            }
        }
        if (GetComponent<AudioSource>().mute != mute) GetComponent<AudioSource>().mute = mute;
        if (GetComponent<AudioSource>().volume != volume) GetComponent<AudioSource>().volume = volume;
    }

    //Changes public values
    public void UpdateStatus(bool newMute, float newVolume)
    {
        mute = newMute;
        volume = newVolume;
    }

    //Clip controlling
    public void ChangeClip(int index)
    {
        GetComponent<AudioSource>().clip = clips[index];
    }

    public void PlayClip()
    {
        GetComponent<AudioSource>().Play();
    }

    public void StopClip()
    {
        GetComponent<AudioSource>().Stop();
    }

    public void PauseClip()
    {
        GetComponent<AudioSource>().Pause();
    }
}
